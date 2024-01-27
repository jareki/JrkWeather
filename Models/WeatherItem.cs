using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JrkWeather.Models
{
    public class WeatherItem
    {
        public DateTime DateTime;
        public double Temp;
        public string Description;
        public string Icon;

        public WeatherItem(DateTime dateTime, double temp, string description, string icon)
        {
            DateTime = dateTime;
            Temp = temp;
            Description = description;
            Icon = icon;
        }
    }
}
