namespace Showcase.Domain.Measurements.Temperatures
{
    public interface ITemperatureSending
    {
        Task SendTemperatureAsync(Temperature temperature, CancellationToken cancellationToken);
    }
}
