using Showcase.Measurement.Domain.Weather.Aggregate;

namespace Showcase.Measurement.Application
{
    public interface IWeatherMeasurementService
    {
        Task MeasureWeatherAsync(double latitude, double longitude, CancellationToken cancellationToken);
        Task<IReadOnlyList<Temperature>> GetTemperaturesAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<Pressure>> GetPressuresAsync(CancellationToken cancellationToken);
    }
}
