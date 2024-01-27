namespace JrkWeather.Models
{
    public class Forecast
    {
        public Location? Location { get; set; }
        public WeatherItem? CurrentWeather { get; set; }
        public IEnumerable<WeatherItem>? HourlyForecast { get; set; }
        public IEnumerable<WeatherSummaryItem>? DailyForecast { get; set; }

        public string? ErrorText { get; set; }

    }
}
