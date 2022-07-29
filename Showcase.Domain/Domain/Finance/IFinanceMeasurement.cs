namespace Showcase.Measurement.Domain.Finance
{
    public interface IFinanceMeasurement
    {
        Task<StockRecord> GetStockMeasurementAsync(string symbol, DateTime date, CancellationToken cancellation);
    }
}
