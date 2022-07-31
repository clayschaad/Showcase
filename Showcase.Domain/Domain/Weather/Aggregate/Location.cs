namespace Showcase.Measurement.Domain.Weather.Aggregate
{
    public class Location
    {
        public Guid Id { get; private set; }
        public Coordinates Coordinates { get; } = null!;

        private Location()
        {
            // EF needed
        }

        private Location(Guid id, Coordinates coordinates)
        {
            Id = id;
            Coordinates = coordinates;
        }

        public static Location New(double latitude, double longitude)
        {
            return new Location(Guid.NewGuid(), Coordinates.New(latitude: latitude, longitude: longitude));
        }
    }
}
