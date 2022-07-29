using Showcase.Measurement.Domain.Weather;

namespace Showcase.Measurement.Application
{
    public interface IWeatherMeasurementService
    {
        Task MeasureWeatherAsync(Coordinates coordinates, CancellationToken cancellationToken);
        Task<IReadOnlyList<Temperature>> GetTemperaturesAsync(CancellationToken cancellationToken);
        Task<IReadOnlyList<Pressure>> GetPressuresAsync(CancellationToken cancellationToken);
    }
}
