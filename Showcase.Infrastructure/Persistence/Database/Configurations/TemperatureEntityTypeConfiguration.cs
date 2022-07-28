﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Showcase.Domain.Measurements.Weather;

namespace Showcase.Infrastructure.Persistence.Database
{
    public class TemperatureEntityTypeConfiguration : IEntityTypeConfiguration<Temperature>
    {
        public void Configure(EntityTypeBuilder<Temperature> builder)
        {
            builder.OwnsOne(e => e.Coordinates, CoordinatesEntityTypeConfiguration.Configure);
        }
    }
}
