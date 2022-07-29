namespace Showcase.Domain.Measurements
{
    public interface IFinanceMeasurementService
    {
        Task MeasureStockAsync(string symbol, CancellationToken cancellationToken);
    }
}
