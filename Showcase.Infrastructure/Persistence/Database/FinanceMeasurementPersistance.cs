using Microsoft.EntityFrameworkCore;
using Showcase.Measurement.Domain.Finance;
using Showcase.Measurement.Domain.Finance.Aggregate;

namespace Showcase.Infrastructure.Persistence.Database
{
    public class FinanceMeasurementPersistance : IFinanceMeasurementPersistance
    {
        private readonly MeasurementDbContext measurementDbContext;

        public FinanceMeasurementPersistance(MeasurementDbContext measurementDbContext)
        {
            this.measurementDbContext = measurementDbContext;
        }

        public async Task<Stock?> GetStockAsync(string symbol, CancellationToken cancellationToken)
        {
            return await measurementDbContext.Stocks.SingleOrDefaultAsync(s => s.Symbol == symbol, cancellationToken);
        }

        public void Add(StockRate rate)
        {
            measurementDbContext.StockRates.Add(rate);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await measurementDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
