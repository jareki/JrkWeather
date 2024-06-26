﻿using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using JrkWeather.Constants;
using JrkWeather.Models;
using JrkWeather.Models.OpenWeatherMap.IN;
using JrkWeather.Models.OpenWeatherMap.OUT;

namespace JrkWeather.Services
{
    public class OpenWeatherApiService
    {
        #region Fields
        
        private readonly Lazy<HttpClient> _httpClient = new Lazy<HttpClient>(() =>
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return httpClient;
        });

        private readonly SettingsService _settingsService;
        
        #endregion

        #region Constructors

        public OpenWeatherApiService(
            SettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        #endregion

        #region Private Methods

        private async Task<IOwmResponseModel?> ExecuteAsync<TRequestModel,TResponseModel>(
            string endpoint, 
            TRequestModel requestModel, 
            CancellationToken ct = default) 
            where TRequestModel: IOwmRequestModel, new()
        where TResponseModel: IOwmResponseModel, new()
        {
            var message = new HttpRequestMessage()
            {
                RequestUri = new Uri(
                    new Uri(this._settingsService.ApiEndPoint, UriKind.Absolute), 
                    $"{endpoint}?{requestModel.CreateGetParamUriString()}"),
                Method = HttpMethod.Get
            };
            var content = JsonSerializer.Serialize(requestModel);
            message.Content = new StringContent(content, Encoding.UTF8);

            try
            {
                var response = await this._httpClient.Value.SendAsync(message, ct);
                if (response.IsSuccessStatusCode)
                {
                    var result =
                        JsonSerializer.Deserialize<TResponseModel>(await response.Content.ReadAsStringAsync(ct));
                    return result;
                }
                else
                {
                    return this.HandleRequestErrors(response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return new ErrorResponseModel(ex.Message);
            }
        }

        private ErrorResponseModel HandleRequestErrors(HttpStatusCode code)
        {
            if (code == HttpStatusCode.BadRequest)
            {
                return new ErrorResponseModel("400, BadRequest");
            }

            if (code == HttpStatusCode.Unauthorized)
            {
                return new ErrorResponseModel("401, Invalid API token");
            }

            if (code == HttpStatusCode.NotFound)
            {
                return new ErrorResponseModel("404, Data not found");
            }

            if (code == HttpStatusCode.TooManyRequests)
            {
                return new ErrorResponseModel("429, Requests count is exceeded");
            }

            if ((int)code >= 500 && (int)code < 600)
            {
                return new ErrorResponseModel($"{code}, Unknown error");
            }

            return new ErrorResponseModel(code.ToString());
        }

        #endregion

        #region Public Methods

        public async Task<IOwmResponseModel?> GetWeatherForecastDataAsync(CancellationToken ct = default)
        {
            if (_settingsService.CurrentPlace == null)
            {
                return null;
            }

            var requestModel = new WeatherRequestModel()
            {
                ApiKey = _settingsService.ApiKey,
                Position = _settingsService.CurrentPlace.GeoPosition,
                UnitSystem = _settingsService.UnitSystem
            };

            return await this.ExecuteAsync<WeatherRequestModel, WeatherResponseModel>("data/3.0/onecall", requestModel, ct);
        }

        public async Task<IOwmResponseModel?> GetLocationDataAsync(CancellationToken ct = default)
        {
            var requestModel = new GeoLocationRequestModel()
            {
                ApiKey = _settingsService.ApiKey,
                Position = _settingsService.LastLocation,
                Limit = DefaultConstants.LocationsCount
            };

            return await this.ExecuteAsync<GeoLocationRequestModel, GeoLocationResponseModel>("geo/1.0/reverse", requestModel, ct);
        }

        #endregion

    }
}
