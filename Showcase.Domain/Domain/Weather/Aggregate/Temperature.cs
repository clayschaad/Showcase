namespace Showcase.Measurement.Domain.Weather.Aggregate
{
    public class Temperature
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }

        public Coordinates? Coordinates { get; set; }

        public static Temperature NewMeasurement(double value, DateTime timestamp, Coordinates coordinates)
        {
            return new Temperature
            {
                Id = Guid.NewGuid(),
                Timestamp = timestamp,
                Value = value,
                Coordinates = coordinates
            };
        }
    }
}