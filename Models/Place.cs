namespace JrkWeather.Models
{
    public class Place
    {
        public Guid Id;
        public string Name;
        public Coordinates GeoPosition;

        public Place(string name, Coordinates geoPosition)
        {
            this.Name = name;
            this.GeoPosition = geoPosition;
        }

        public Place(Guid id, string name, double latitude, double longitude)
        {
            this.Id = id;
            this.Name =name;
            this.GeoPosition = new Coordinates(latitude, longitude);
        }
    }
}
