using Showcase.Domain.Measurements.Weather;

namespace Showcase.Domain.Measurements
{
    public interface IWeatherMeasurementService
    {
        Task MeasureWeatherAsync(Coordinates coordinates, CancellationToken cancellationToken);
        Task<IReadOnlyList<Temperature>> GetTemperaturesAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<Pressure>> GetPressuresAsync(CancellationToken cancellationToken);
    }
}
