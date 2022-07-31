namespace Showcase.Measurement.Domain.Weather.Aggregate
{
    public class Temperature
    {
        public Guid Id { get; }
        public double Value { get; private set; }
        public DateTime Timestamp { get; private set; }
        public Location Location { get; private set; } = null!;

        private Temperature()
        {
            // EF needed
        }

        private Temperature(Guid id, DateTime timestamp, double value, Location location)
        {
            Id = id;
            Timestamp = timestamp;
            Value = value;
            Location = location;
        }

        public static Temperature New(DateTime timestamp, double value, Location location)
        {
            return new Temperature(Guid.NewGuid(), timestamp, value, location);
        }
    }
}