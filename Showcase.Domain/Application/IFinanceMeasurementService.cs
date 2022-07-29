namespace Showcase.Measurement.Application
{
    public interface IFinanceMeasurementService
    {
        Task MeasureStockAsync(string symbol, DateTime date, CancellationToken cancellationToken);
    }
}
