using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JrkWeather.Models.OpenWeatherMap.OUT
{
    public class GeoLocationResponseModel: IOwmResponseModel
    {
        public LocationInfo[] Data { get; set; }

        public class LocationInfo
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("lat")]
            public double Latitude { get; set; }

            [JsonPropertyName("lon")]
            public double Longitude { get; set; }

            [JsonPropertyName("country")]
            public string Country { get; set; }
        }
    }

}
