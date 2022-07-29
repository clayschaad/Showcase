namespace Showcase.Domain.Measurements.Finance
{
    public interface IFinanceMeasurement
    {
        Task<StockMeasurement> GetStockMeasurementAsync(string symbol, DateTime date, CancellationToken cancellation);
    }
}
