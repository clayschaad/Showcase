using Microsoft.EntityFrameworkCore;
using Showcase.Domain.Measurements.Temperatures;

namespace Showcase.Infrastructure.Persistence.Database
{
    public class TemperaturePersistance : ITemperaturePersistance
    {
        private readonly MeasurementDbContext measurementDbContext;

        public TemperaturePersistance(MeasurementDbContext measurementDbContext)
        {
            this.measurementDbContext = measurementDbContext;
        }

        public async Task<Temperature?> GetTemperatureAsync(Guid id, CancellationToken cancellationToken)
        {
            return await measurementDbContext.Temperatures.SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task SaveTemperatureAsync(Temperature temperature, CancellationToken cancellationToken)
        {
            measurementDbContext.Temperatures.Add(temperature);
            await measurementDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Temperature>> LoadTemperaturesAsync(CancellationToken cancellationToken)
        {
            return await measurementDbContext.Temperatures.ToListAsync(cancellationToken);
        }
    }
}
