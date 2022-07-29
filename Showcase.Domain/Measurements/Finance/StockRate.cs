namespace Showcase.Domain.Measurements.Finance
{
    public class StockRate
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }

        public string Symbol { get; set; } = null!;

        public static StockRate NewMeasurement(double open, double close, DateTime timestamp, string symbol)
        {
            return new StockRate
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
