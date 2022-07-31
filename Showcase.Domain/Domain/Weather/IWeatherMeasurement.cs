namespace Showcase.Measurement.Domain.Weather
{
    public interface IWeatherMeasurement
    {
        Task<WeatherRecord> GetWeatherMeasurementAsync(double latitude, double longitude, CancellationToken cancellation);
    }
}
