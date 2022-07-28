using Showcase.Domain.Measurements;
using Showcase.Domain.Measurements.Weather;

namespace Showcase.Domain
{
    public interface IWeatherMeasurementService
    {
        Task MeasureWeatherAsync(Coordinates coordinates, CancellationToken cancellationToken);
        Task<IReadOnlyList<Temperature>> GetTemperaturesAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<Pressure>> GetPressuresAsync(CancellationToken cancellationToken);
    }
}
