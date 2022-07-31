using Showcase.Measurement.Domain.Weather;

namespace Showcase.Measurement.Application
{
    public interface IWeatherMeasurementService
    {
        Task MeasureWeatherAsync(double latitude, double longitude, CancellationToken cancellationToken);
        Task SaveWeatherAsync(WeatherRecord weatherRecord, CancellationToken cancellationToken);
    }
}
