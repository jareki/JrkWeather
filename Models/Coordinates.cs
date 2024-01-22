namespace JrkWeather.Models
{
    public struct Coordinates
    {
        public double Latitude;
        public double Longitude;

        public Coordinates(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}
