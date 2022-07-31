using Showcase.Measurement.Domain.Finance;

namespace Showcase.Measurement.Application
{
    public interface IFinanceMeasurementService
    {
        Task MeasureStockAsync(string symbol, DateTime date, CancellationToken cancellationToken);
        Task SaveStockAsync(StockRecord stockRecord, CancellationToken cancellationToken);
    }
}
