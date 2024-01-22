namespace JrkWeather.Services
{
    public class WeatherService
    {
        #region Fields

        private readonly LiteDbService _liteDbService;
        private readonly OpenWeatherApiService _openWeatherApiService;

        #endregion

        #region Constructors

        public WeatherService(
            LiteDbService liteDbService,
            OpenWeatherApiService openWeatherApiService)
        {
            this._liteDbService = liteDbService;
            this._openWeatherApiService = openWeatherApiService;
        }

        #endregion
    }
}
