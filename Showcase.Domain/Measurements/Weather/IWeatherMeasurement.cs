namespace Showcase.Domain.Measurements.Weather
{
    public interface IWeatherMeasurement
    {
        Task<WeatherMeasurement> GetWeatherMeasurementAsync(Coordinates coordinates, CancellationToken cancellation);
    }
}
