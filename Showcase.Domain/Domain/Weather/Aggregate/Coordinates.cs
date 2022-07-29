namespace Showcase.Measurement.Domain.Weather.Aggregate
{
    public record Coordinates(double Latitude, double Longitude)
    {
        public static Coordinates New(double latitude, double longitude)
        {
            return new Coordinates(Latitude: latitude, Longitude: longitude);
        }
    }
}
