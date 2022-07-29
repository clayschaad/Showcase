using Showcase.Measurement.Domain.Weather.Aggregate;

namespace Showcase.Measurement.Domain.Weather
{
    public interface IWeatherMeasurement
    {
        Task<WeatherRecord> GetWeatherMeasurementAsync(Coordinates coordinates, CancellationToken cancellation);
    }
}
