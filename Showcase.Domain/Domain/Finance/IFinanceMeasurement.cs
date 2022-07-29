namespace Showcase.Measurement.Domain.Finance
{
    public interface IFinanceMeasurement
    {
        Task<StockMeasurement> GetStockMeasurementAsync(string symbol, DateTime date, CancellationToken cancellation);
    }
}
