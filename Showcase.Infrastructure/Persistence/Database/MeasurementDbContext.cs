using Microsoft.EntityFrameworkCore;
using Showcase.Domain.Measurements;
using Showcase.Domain.Measurements.Temperatures;

namespace Showcase.Infrastructure.Persistence.Database
{
    public class MeasurementDbContext : DbContext
    {
        public DbSet<Temperature> Temperatures { get; set; } = null!;

        public MeasurementDbContext(DbContextOptions<MeasurementDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Coordinates>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeasurementDbContext).Assembly);
        }
    }
}
