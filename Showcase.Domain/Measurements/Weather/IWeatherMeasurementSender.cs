namespace Showcase.Domain.Measurements.Weather
{
    public interface IWeatherMeasurementSender
    {
        Task SendWeatherMeasurement<T>(T measurement, CancellationToken cancellationToken) where T : notnull;
    }
}
