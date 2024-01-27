using System.Text.Json;
using JrkWeather.Constants;
using JrkWeather.Models;
using JrkWeather.Models.OpenWeatherMap.OUT;

namespace JrkWeather.Services
{
    public class WeatherService
    {
        #region Fields

        private readonly LiteDbService _liteDbService;
        private readonly OpenWeatherApiService _openWeatherApiService;
        private readonly DeviceLocationService _deviceLocationService;
        private readonly SettingsService _settingsService;

        #endregion

        #region Constructors

        public WeatherService(
            LiteDbService liteDbService,
            OpenWeatherApiService openWeatherApiService,
            DeviceLocationService deviceLocationService,
            SettingsService settingsService)
        {
            this._liteDbService = liteDbService;
            this._openWeatherApiService = openWeatherApiService;
            this._deviceLocationService = deviceLocationService;
            _settingsService = settingsService;
        }

        #endregion

        private Forecast TranslateWeatherData(WeatherResponseModel responseModel)
        {
            var forecast = new Forecast();
            forecast.Location = _settingsService?.CurrentPlace;
            forecast.ErrorText = null;
            forecast.CurrentWeather = new WeatherItem(
                this.UnixTimestampToDateTime(responseModel.CurrentWeather.Timestamp),
                responseModel.CurrentWeather.Temp,
                responseModel.CurrentWeather.Weather.FirstOrDefault()?.Description,
                GetWeatherIcon(responseModel.CurrentWeather.Weather.FirstOrDefault()?.Id ?? 0));
            forecast.DailyForecast =
                responseModel.DailyForecast.Select(f =>
                    new WeatherSummaryItem(
                        this.UnixTimestampToDateTime(f.Timestamp),
                        f.Temp.Min,
                        f.Temp.Max,
                        f.Weather.FirstOrDefault()?.Description,
                        this.GetWeatherIcon(f.Weather.FirstOrDefault()?.Id ?? 0)));
            forecast.HourlyForecast =
                responseModel.HourlyForecast.Select(f =>
                    new WeatherItem(
                        this.UnixTimestampToDateTime(f.Timestamp),
                        f.Temp,
                        f.Weather.FirstOrDefault()?.Description,
                        this.GetWeatherIcon(f.Weather.FirstOrDefault()?.Id ?? 0)));
            return forecast;
        }

        private DateTime UnixTimestampToDateTime(long timestamp) =>
            DateTimeOffset.FromUnixTimeSeconds(timestamp).LocalDateTime;

        private string GetWeatherIcon(int code)
        {
            if (code == 701 || code == 711) return WeatherIcons.Mist;
            if (code == 721 || code == 741) return WeatherIcons.Fog;
            if (code >= 801 && code <= 804) return WeatherIcons.SunClouds;
            switch (code / 100)
            {
                case 2: return WeatherIcons.Lightning;
                case 3: return WeatherIcons.Rain;
                case 5: return WeatherIcons.HeavyRain;
                case 6: return WeatherIcons.Snow;
                default: return WeatherIcons.Sun;
            }
        }

        #region Public Methods

        public async Task<Forecast> GetForecastAsync(CancellationToken ct = default)
        {
            IOwmResponseModel? data = null;
            if (_settingsService.LastUpdateForecast.AddMinutes(_settingsService.UpdateForecastIntervalMinutes) < DateTime.UtcNow)
            {
                data = await _openWeatherApiService.GetWeatherForecastDataAsync(ct);
                if (data is ErrorResponseModel error)
                {
                    return new Forecast()
                    {
                        ErrorText = error.ErrorText
                    };
                }

                if (_settingsService.CurrentPlace != null)
                {
                    _liteDbService.StoreData(_settingsService.CurrentPlace.Id, JsonSerializer.Serialize(data as WeatherResponseModel));
                }
            }
            else
            {
                if (_settingsService.CurrentPlace != null)
                {
                    var response = _liteDbService.GetStoredData(_settingsService.CurrentPlace.Id);
                    data = JsonSerializer.Deserialize<WeatherResponseModel>(response?.Data ?? string.Empty);
                }
            }

            return this.TranslateWeatherData(data as WeatherResponseModel);

        }

        #endregion
    }
}
