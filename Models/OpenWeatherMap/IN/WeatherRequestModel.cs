using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JrkWeather.Enums;

namespace JrkWeather.Models.OpenWeatherMap.IN
{
    public class WeatherRequestModel: IOwmRequestModel
    {
        #region Fields

        public Coordinates Position { get; set; }

        public string ApiKey { get; set; }

        public UnitSystem UnitSystem { get; set; }

        #endregion

        public string CreateGetParamUriString()
        {
            return $"lat={Position.Latitude},lon={Position.Longitude},units={UnitSystem.ToOwmString()},ApiKey={ApiKey}";
        }
    }
}
