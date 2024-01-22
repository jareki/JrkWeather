using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JrkWeather.Enums;

namespace JrkWeather.Models.OpenWeatherMap.IN
{
    public class GeoPlaceRequestModel : IOwmRequestModel
    {
        #region Fields

        public Coordinates Position { get; set; }

        public string ApiKey { get; set; }

        public int Limit { get; set; }

        #endregion

        public string CreateGetParamUriString()
        {
            return $"lat={Position.Latitude},lon={Position.Longitude},limit={Limit},appid={ApiKey}";
        }
    }
}
