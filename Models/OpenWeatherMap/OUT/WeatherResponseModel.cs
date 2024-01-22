using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrkWeather.Models.OpenWeatherMap.OUT
{
    public class WeatherResponseModel : IOwmResponseModel
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public Current current { get; set; }
        public Hourly[] hourly { get; set; }
        public Daily[] daily { get; set; }
    }
    

    public class Current
    {
        public int dt { get; set; }
        public double temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double wind_speed { get; set; }
        public int wind_deg { get; set; }
        public double wind_gust { get; set; }
        public Weather[] weather { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
    }

    public class Hourly
    {
        public int dt { get; set; }
        public double temp { get; set; }
        public Weather[] weather { get; set; }
    }
    
    public class Daily
    {
        public int dt { get; set; }
        public TempDailySummary temp { get; set; }
        public Weather[] weather { get; set; }
    }

    public class TempDailySummary
    {
        public float day { get; set; }
        public float min { get; set; }
        public float max { get; set; }
        public float night { get; set; }
        public float eve { get; set; }
        public float morn { get; set; }
    }
    

}
