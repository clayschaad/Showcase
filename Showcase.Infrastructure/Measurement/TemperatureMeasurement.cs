using Showcase.Domain.Measurements;

namespace Showcase.Infrastructure.Measurement
{
    internal class TemperatureMeasurement : ITemperatureMeasurement
    {
        public async Task<double> GetTemperatureAsync(CancellationToken cancellation)
        {
            throw new NotImplementedException();
        }
    }
}
