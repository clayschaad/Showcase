using Showcase.Measurement.Domain.Weather.Aggregate;

namespace Showcase.Measurement.Domain.Weather
{
    public interface IWeatherMeasurementPersistance
    {
        Task<Location?> GetLoctionAsync(double latitued, double longitued, CancellationToken cancellationToken);
        void Add(Pressure pressure);
        void Add(Temperature temperature);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
