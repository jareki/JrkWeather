using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrkWeather.Models
{
    public class WeatherSummaryItem
    {
        public DateTime DateTime;
        public double TempMin;
        public double TempMax;
        public string Description;
        public string Icon;

        public WeatherSummaryItem(DateTime dateTime, double tempMin, double tempMax, string description, string icon)
        {
            DateTime = dateTime;
            TempMin = tempMin;
            TempMax = tempMax;
            Description = description;
            Icon = icon;
        }
    }
}
