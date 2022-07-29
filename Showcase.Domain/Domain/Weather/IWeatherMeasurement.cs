namespace Showcase.Measurement.Domain.Weather
{
    public interface IWeatherMeasurement
    {
        Task<WeatherMeasurement> GetWeatherMeasurementAsync(Coordinates coordinates, CancellationToken cancellation);
    }
}
