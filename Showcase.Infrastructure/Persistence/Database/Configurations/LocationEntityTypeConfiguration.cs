using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Showcase.Measurement.Domain.Weather.Aggregate;

namespace Showcase.Infrastructure.Persistence.Database
{
    public class LocationEntityTypeConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.Id);
            builder.OwnsOne(l => l.Coordinates, CoordinatesEntityTypeConfiguration.Configure);
        }
    }
}
