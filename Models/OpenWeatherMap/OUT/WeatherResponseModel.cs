using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JrkWeather.Models.OpenWeatherMap.OUT
{
    public class WeatherResponseModel : IOwmResponseModel
    {
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        [JsonPropertyName("current")]
        public Current CurrentWeather { get; set; }

        [JsonPropertyName("hourly")]
        public Hourly[] HourlyForecast { get; set; }

        [JsonPropertyName("daily")]
        public Daily[] DailyForecast { get; set; }
        public class Current
        {
            [JsonPropertyName("dt")]
            public int Timestamp { get; set; }

            [JsonPropertyName("temp")]
            public double Temp { get; set; }

            [JsonPropertyName("pressure")]
            public int Pressure { get; set; }

            [JsonPropertyName("humidity")]
            public int Humidity { get; set; }

            [JsonPropertyName("wind_speed")]
            public double WindSpeed { get; set; }

            [JsonPropertyName("wind_deg")]
            public int WindDirection { get; set; }

            [JsonPropertyName("wind_gust")]
            public double WindGust { get; set; }

            [JsonPropertyName("weather")]
            public Weather[] Weather { get; set; }
        }

        public class Weather
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("main")]
            public string Main { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }
        }

        public class Hourly
        {
            [JsonPropertyName("dt")]
            public int Timestamp { get; set; }

            [JsonPropertyName("temp")]
            public double Temp { get; set; }

            [JsonPropertyName("weather")]
            public Weather[] Weather { get; set; }
        }

        public class Daily
        {
            [JsonPropertyName("dt")]
            public int Timestamp { get; set; }

            [JsonPropertyName("temp")]
            public TempDailySummary Temp { get; set; }

            [JsonPropertyName("weather")]
            public Weather[] Weather { get; set; }
        }

        public class TempDailySummary
        {
            [JsonPropertyName("min")]
            public float Min { get; set; }

            [JsonPropertyName("max")]
            public float Max { get; set; }
        }
    }
}
