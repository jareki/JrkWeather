namespace JrkWeather.Models
{
    public class Location
    {
        public Guid Id;
        public string Name;
        public Coordinates GeoPosition;

        public Location(string name, Coordinates geoPosition)
        {
            this.Name = name;
            this.GeoPosition = geoPosition;
        }

        public Location(Guid id, string name, double latitude, double longitude)
        {
            this.Id = id;
            this.Name =name;
            this.GeoPosition = new Coordinates(latitude, longitude);
        }
    }
}
