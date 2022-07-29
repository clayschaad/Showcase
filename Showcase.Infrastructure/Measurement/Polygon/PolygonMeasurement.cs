using Microsoft.Extensions.Configuration;
using Showcase.Domain.Measurements;
using Showcase.Domain.Measurements.Finance;
using System.Net.Http.Json;

namespace Showcase.Infrastructure.Measurement.Polygon
{
    public class PolygonMeasurement : IFinanceMeasurement
    {
        private readonly IConfiguration configuration;

        public PolygonMeasurement(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<StockMeasurement> GetStockMeasurementAsync(string symbol, DateTime date, CancellationToken cancellation)
        {
            var httpClient = new HttpClient();
            var options = configuration.GetSection(MeasurementOptions.SectionKey).Get<MeasurementOptions>();

            var stockMeasurement = await httpClient.GetFromJsonAsync<PolygonModel>($"https://api.polygon.io/v1/open-close/{symbol}/{date:yyyy-MM-dd}?adjusted=true&apiKey={options.PolygonApiKey}");
            if (stockMeasurement == null)
            {
                throw new MeasurementException("Cannot parse measurement result");
            }

            return new StockMeasurement(Open: stockMeasurement.Open, Close: stockMeasurement.Close, LastRefresh: stockMeasurement.From);
        }
    }
}
