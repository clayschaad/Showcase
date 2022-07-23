namespace Showcase.Domain.Measurements.Temperatures
{
    public interface ITemperatureMeasurement
    {
        Task<double> GetTemperatureAsync(Coordinates coordinates, CancellationToken cancellation);
    }
}
