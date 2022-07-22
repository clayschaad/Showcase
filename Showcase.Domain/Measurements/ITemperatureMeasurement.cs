namespace Showcase.Domain.Measurements
{
    public interface ITemperatureMeasurement
    {
        Task<double> GetTemperatureAsync(CancellationToken cancellation);
    }
}
