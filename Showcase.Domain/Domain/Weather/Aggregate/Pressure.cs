namespace Showcase.Measurement.Domain.Weather.Aggregate
{
    public class Pressure
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }

        public Coordinates? Coordinates { get; set; }

        public static Pressure NewMeasurement(double value, DateTime timestamp, Coordinates coordinates)
        {
            return new Pressure
            {
                Id = Guid.NewGuid(),
                Timestamp = timestamp,
                Value = value,
                Coordinates = coordinates
            };
        }
    }
}