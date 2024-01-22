using System.Text.Json;
using JrkWeather.Constants;
using JrkWeather.Enums;
using JrkWeather.Models;
using Location = JrkWeather.Models.Location;

namespace JrkWeather.Services
{
    public class SettingsService
    {
        #region Fields

        public string ApiKey
        {
            get => Preferences.Get(nameof(ApiKey), SecretsConstants.DefaultAPIKey);
            set => Preferences.Set(nameof(ApiKey), value);
        }

        public string ApiEndPoint
        {
            get => Preferences.Get(nameof(ApiEndPoint), SecretsConstants.DefaultWeatherAPIEndPoint);
            set => Preferences.Set(nameof(ApiEndPoint), value);
        }
        
        public int UpdateForecastIntervalMinutes
        {
            get => Preferences.Get(nameof(UpdateForecastIntervalMinutes), DefaultConstants.UpdateIntervalMinutes);
            set => Preferences.Set(nameof(UpdateForecastIntervalMinutes), value);
        }
        
        public DateTime LastUpdateForecast
        {
            get => Preferences.Get(nameof(LastUpdateForecast), DateTime.MinValue);
            set => Preferences.Set(nameof(LastUpdateForecast), value);
        }

        /// <summary>
        /// unit system for making api requests and visualizing data
        /// </summary>
        public UnitSystem UnitSystem
        {
            get
            {
                int val = Preferences.Get(nameof(UnitSystem), (int)DefaultConstants.UnitSystem);
                if (Enum.IsDefined(typeof(UnitSystem), val))
                {
                    return (UnitSystem)val;
                }
                else
                {
                    return UnitSystem.Metric;
                }
            }
            set => Preferences.Set(nameof(UnitSystem), (int)value);
        }

        /// <summary>
        /// saved location from weatherservice to making api requests
        /// </summary>
        public Location? CurrentPlace
        {
            get
            {
                string val = Preferences.Get(nameof(CurrentPlace), string.Empty);
                return JsonSerializer.Deserialize<Location>(val);
            }
            set
            {
                string val = JsonSerializer.Serialize(value);
                Preferences.Set(nameof(CurrentPlace), val);
            }
        }

        /// <summary>
        /// saved last device's geolocation for tracking movements
        /// </summary>
        public Coordinates LastLocation
        {
            get
            {
                string val = Preferences.Get(nameof(LastLocation), string.Empty);
                return JsonSerializer.Deserialize<Coordinates>(val);
            }
            set
            {
                string val = JsonSerializer.Serialize(value);
                Preferences.Set(nameof(LastLocation), val);
            }
        }

        #endregion
    }
}
