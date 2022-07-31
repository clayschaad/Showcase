namespace Showcase.Measurement.Domain.Finance.Aggregate
{
    public class StockRate
    {
        public Guid Id { get; set; }
        public DateTime Timestamp { get; private set; }
        public double Open { get; private set; }
        public double Close { get; private set; }
        public Stock Stock { get; private set; } = null!;

        private StockRate()
        {
            // EF
        }

        private StockRate(Guid id, DateTime timestamp, double open, double close, Stock stock)
        {
            Id = id;
            Open = open;
            Close = close;
            Timestamp = timestamp;
            Stock = stock;
        }

        public static StockRate New(DateTime timestamp, double open, double close, Stock stock)
        {
            return new StockRate(id: Guid.NewGuid(), timestamp: timestamp, open: open, close: close, stock: stock);
        }
    }
}
