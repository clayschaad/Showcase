using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Showcase.Measurement.Domain.Weather.Aggregate;

namespace Showcase.Infrastructure.Persistence.Database
{
    public class MeasurementDbContext : DbContext
    {
        public DbSet<Location> Locations { get; set; } = null!;
        public DbSet<Temperature> Temperatures { get; set; } = null!;
        public DbSet<Pressure> Pressures { get; set; } = null!;

        public MeasurementDbContext(DbContextOptions<MeasurementDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Coordinates>();

            ApplyDefaultEntityConfiguration(modelBuilder.Model);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeasurementDbContext).Assembly);
        }

        private static void ApplyDefaultEntityConfiguration(IMutableModel model)
        {
            foreach (var entity in model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName());

                ApplyDefaultForeignKeyConfiguration(entity);
            }
        }

        private static void ApplyDefaultForeignKeyConfiguration(IMutableEntityType entity)
        {
            var foreignKeys = entity.GetForeignKeys();
            foreach (var foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
