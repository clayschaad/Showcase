using Microsoft.EntityFrameworkCore;
using Showcase.Measurement.Domain.Weather;

namespace Showcase.Infrastructure.Persistence.Database
{
    public class DatabaseWeatherMeasurementPersistance : IWeatherMeasurementPersistance
    {
        private readonly MeasurementDbContext measurementDbContext;

        public DatabaseWeatherMeasurementPersistance(MeasurementDbContext measurementDbContext)
        {
            this.measurementDbContext = measurementDbContext;
        }

        public async Task<Temperature?> GetTemperatureAsync(Guid id, CancellationToken cancellationToken)
        {
            return await measurementDbContext.Temperatures.SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Temperature>> LoadTemperaturesAsync(CancellationToken cancellationToken)
        {
            return await measurementDbContext.Temperatures.OrderByDescending(t => t.Timestamp).ToListAsync(cancellationToken);
        }

        public async Task SaveTemperatureAsync(Temperature temperature, CancellationToken cancellationToken)
        {
            measurementDbContext.Temperatures.Add(temperature);
            await measurementDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Pressure>> LoadPressuresAsync(CancellationToken cancellationToken)
        {
            return await measurementDbContext.Pressures.OrderByDescending(t => t.Timestamp).ToListAsync(cancellationToken);
        }

        public async Task SavePressureAsync(Pressure pressure, CancellationToken cancellationToken)
        {
            measurementDbContext.Pressures.Add(pressure);
            await measurementDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
