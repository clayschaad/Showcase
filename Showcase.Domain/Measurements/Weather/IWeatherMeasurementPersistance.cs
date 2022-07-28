namespace Showcase.Domain.Measurements.Weather
{
    public interface IWeatherMeasurementPersistance
    {
        Task<IReadOnlyList<Temperature>> LoadTemperaturesAsync(CancellationToken cancellationToken);
        Task SaveTemperatureAsync(Temperature temperature, CancellationToken cancellationToken);
        Task<Temperature?> GetTemperatureAsync(Guid id, CancellationToken cancellationToken);

        Task<IReadOnlyList<Pressure>> LoadPressuresAsync(CancellationToken cancellationToken);
        Task SavePressureAsync(Pressure pressure, CancellationToken cancellationToken);
    }
}
