using Microsoft.EntityFrameworkCore;
using Showcase.Measurement.Domain.Weather;

namespace Showcase.Infrastructure.Persistence.Database
{
    public class MeasurementDbContext : DbContext
    {
        public DbSet<Temperature> Temperatures { get; set; } = null!;
        public DbSet<Pressure> Pressures { get; set; } = null!;

        public MeasurementDbContext(DbContextOptions<MeasurementDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Coordinates>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeasurementDbContext).Assembly);
        }
    }
}
