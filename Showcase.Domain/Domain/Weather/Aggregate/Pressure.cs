namespace Showcase.Measurement.Domain.Weather.Aggregate
{
    public class Pressure
    {
        public Guid Id { get; }
        public double Value { get; private set; }
        public DateTime Timestamp { get; private set; }
        public Location Location { get; private set; } = null!;

        private Pressure()
        {
            // EF needed
        }

        private Pressure(Guid id, DateTime timestamp, double value, Location location)
        {
            Id = id;
            Timestamp = timestamp;
            Value = value;
            Location = location;
        }

        public static Pressure New(DateTime timestamp, double value, Location location)
        {
            return new Pressure(Guid.NewGuid(), timestamp, value, location);
        }
    }
}