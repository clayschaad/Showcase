using Showcase.Domain.Measurements.Finance;

namespace Showcase.Domain.Measurements
{
    public class FinanceMeasurementService : IFinanceMeasurementService
    {
        private readonly IFinanceMeasurement financeMeasurement;
        private readonly IMeasurementSender measurementSender;

        public FinanceMeasurementService(IFinanceMeasurement financeMeasurement, IMeasurementSender measurementSender)
        {
            this.financeMeasurement = financeMeasurement;
            this.measurementSender = measurementSender;
        }

        public async Task MeasureStockAsync(string symbol, DateTime date, CancellationToken cancellationToken)
        {
            var measurement = await financeMeasurement.GetStockMeasurementAsync(symbol, date, cancellationToken);

            var rate = StockRate.NewMeasurement(open: measurement.Open, close: measurement.Close, symbol: symbol, timestamp: measurement.LastRefresh);
            await measurementSender.SendMeasurement(rate, cancellationToken);
        }
    }
}
