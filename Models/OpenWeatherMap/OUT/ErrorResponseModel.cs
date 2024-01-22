namespace JrkWeather.Models.OpenWeatherMap.OUT
{
    public class ErrorResponseModel: IOwmResponseModel
    {
        public string ErrorText { get; set; }

        public ErrorResponseModel(string errorText = null)
        {
            this.ErrorText = errorText;
        }
    }
}
