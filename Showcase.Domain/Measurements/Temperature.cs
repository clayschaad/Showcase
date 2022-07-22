namespace Showcase.Domain.Measurements
{
    public class Temperature
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }

        public static Temperature NewMeasurement(double value, DateTime timestamp)
        {
            return new Temperature
            {
                Id = Guid.NewGuid(),
                Timestamp = timestamp,
                Value = value
            };
        }
    }
}