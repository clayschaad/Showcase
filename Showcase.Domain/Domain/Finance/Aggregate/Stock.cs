namespace Showcase.Measurement.Domain.Finance.Aggregate
{
    public class Stock
    {
        public Guid Id { get; set; }
        public string Symbol { get; private set; } = null!;

        private Stock()
        {
            // EF
        }

        private Stock(string symbol)
        {
            Symbol = symbol;
        }

        public static Stock New(string symbol)
        {
            return new Stock(symbol);
        }
    }
}
