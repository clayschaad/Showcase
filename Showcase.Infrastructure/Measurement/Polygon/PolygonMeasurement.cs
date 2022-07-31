using Microsoft.Extensions.Configuration;
using Showcase.Measurement.Domain;
using Showcase.Measurement.Domain.Finance;
using System.Text.Json;

namespace Showcase.Infrastructure.Measurement.Polygon
{
    public class PolygonMeasurement : IFinanceMeasurement
    {
        private readonly IConfiguration configuration;

        public PolygonMeasurement(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<StockRecord> GetStockMeasurementAsync(string symbol, DateTime date, CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();
            var options = configuration.GetSection(MeasurementOptions.SectionKey).Get<MeasurementOptions>();

            var requestUrl = $"https://api.polygon.io/v1/open-close/{symbol}/{date:yyyy-MM-dd}?adjusted=true&apiKey={options.PolygonApiKey}";
            var response = await httpClient.GetAsync(requestUrl, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw new MeasurementException($"Cannot parse measurement result: {response.ReasonPhrase}");
            }

            var jsonString = await response.Content.ReadAsStringAsync(cancellationToken);
            var stockMeasurement = JsonSerializer.Deserialize<PolygonModel>(jsonString);
            return new StockRecord(Open: stockMeasurement!.Open, Close: stockMeasurement.Close, LastRefresh: stockMeasurement.From);
        }
    }
}
