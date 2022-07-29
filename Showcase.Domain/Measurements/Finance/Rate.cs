namespace Showcase.Domain.Measurements.Finance
{
    public class Rate
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }

        public string Symbol { get; set; }

        public static Rate NewMeasurement(double open, double close, DateTime timestamp, string symbol)
        {
            return new Rate
            {
                Id = Guid.NewGuid(),
                Timestamp = timestamp,
                Open = open,
                Close = close,
                Symbol = symbol
            };
        }
    }
}
