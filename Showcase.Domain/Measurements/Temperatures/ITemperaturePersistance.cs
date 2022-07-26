namespace Showcase.Domain.Measurements.Temperatures
{
    public interface ITemperaturePersistance
    {
        Task<IReadOnlyList<Temperature>> LoadTemperaturesAsync(CancellationToken cancellationToken);
        Task SaveTemperatureAsync(Temperature temperature, CancellationToken cancellationToken);
        Task<Temperature?> GetTemperatureAsync(Guid id, CancellationToken cancellationToken);
    }
}
