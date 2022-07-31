using Showcase.Measurement.Domain.Finance.Aggregate;

namespace Showcase.Measurement.Domain.Finance
{
    public interface IFinanceMeasurementPersistance
    {
        Task<Stock?> GetStockAsync(string symbol, CancellationToken cancellationToken);
        void Add(StockRate rate);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
