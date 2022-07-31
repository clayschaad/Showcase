using Showcase.Measurement.Domain;
using Showcase.Measurement.Domain.Finance;
using Showcase.Measurement.Domain.Finance.Aggregate;

namespace Showcase.Measurement.Application.Finance
{
    public class FinanceMeasurementService : IFinanceMeasurementService
    {
        private readonly IFinanceMeasurement financeMeasurement;
        private readonly IMeasurementSender measurementSender;
        private readonly IFinanceMeasurementPersistance financeMeasurementPersistance;

        public FinanceMeasurementService(IFinanceMeasurement financeMeasurement, IMeasurementSender measurementSender, IFinanceMeasurementPersistance financeMeasurementPersistance)
        {
            this.financeMeasurement = financeMeasurement;
            this.measurementSender = measurementSender;
            this.financeMeasurementPersistance = financeMeasurementPersistance;
        }

        public async Task MeasureStockAsync(string symbol, DateTime date, CancellationToken cancellationToken)
        {
            var stockRecord = await financeMeasurement.GetStockMeasurementAsync(symbol, date, cancellationToken);
            await measurementSender.SendMeasurement(stockRecord, cancellationToken);
        }

        public async Task SaveStockAsync(StockRecord stockRecord, CancellationToken cancellationToken)
        {
            var stock = await financeMeasurementPersistance.GetStockAsync(stockRecord.Symbol, cancellationToken);
            if (stock == null)
            {
                stock = Stock.New(stockRecord.Symbol);
            }

            financeMeasurementPersistance.Add(StockRate.New(timestamp: stockRecord.LastRefresh, open: stockRecord.Open, close: stockRecord.Close, stock));

            await financeMeasurementPersistance.SaveChangesAsync(cancellationToken);
        }
    }
}
