using Microsoft.EntityFrameworkCore;
using Showcase.Measurement.Domain.Weather;
using Showcase.Measurement.Domain.Weather.Aggregate;

namespace Showcase.Infrastructure.Persistence.Database
{
    public class DatabaseWeatherMeasurementPersistance : IWeatherMeasurementPersistance
    {
        private readonly MeasurementDbContext measurementDbContext;

        public DatabaseWeatherMeasurementPersistance(MeasurementDbContext measurementDbContext)
        {
            this.measurementDbContext = measurementDbContext;
        }

        public async Task<Location?> GetLocationAsync(double latitued, double longitued, CancellationToken cancellationToken)
        {
            return await measurementDbContext.Locations.SingleOrDefaultAsync(w => w.Coordinates.Latitude == latitued && w.Coordinates.Longitude == longitued, cancellationToken);
        }

        public void Add(Pressure pressure)
        {
            measurementDbContext.Pressures.Add(pressure);
        }

        public void Add(Temperature temperature)
        {
            measurementDbContext.Temperatures.Add(temperature);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await measurementDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
