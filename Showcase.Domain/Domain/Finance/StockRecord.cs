namespace Showcase.Measurement.Domain.Finance
{
    public record StockRecord(string Symbol, double Open, double Close, DateTime LastRefresh);
}
