namespace Showcase.Measurement.Domain.Weather.Aggregate
{
    public class Location
    {
        public Guid Id { get; private set; }
        public Coordinates Coordinates { get; } = null!;
        public string? City { get; private set; }
        public string? Country { get; private set; }

        private Location()
        {
            // EF needed
        }

        private Location(Guid id, Coordinates coordinates, string city, string country)
        {
            Id = id;
            Coordinates = coordinates;
            City = city;
            Country = country;
        }

        public static Location New(double latitude, double longitude, string city, string country)
        {
            return new Location(Guid.NewGuid(), Coordinates.New(latitude: latitude, longitude: longitude), city: city, country: country);
        }
    }
}
