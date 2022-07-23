namespace Showcase.Domain.Measurements.Temperatures
{
    public interface ITemperaturePersistance
    {
        Task SaveTemperatureAsync(Temperature temperature, CancellationToken cancellation);
        Task<Temperature?> GetTemperatureAsync(Guid id, CancellationToken cancellation);
    }
}
