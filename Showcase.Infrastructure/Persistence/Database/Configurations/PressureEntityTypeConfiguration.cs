using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Showcase.Measurement.Domain.Weather.Aggregate;

namespace Showcase.Infrastructure.Persistence.Database
{
    public class PressureEntityTypeConfiguration : IEntityTypeConfiguration<Pressure>
    {
        public void Configure(EntityTypeBuilder<Pressure> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Location);
        }
    }
}
