﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Showcase.Domain.Measurements.Weather;

namespace Showcase.Infrastructure.Persistence.Database
{
    public class PressureEntityTypeConfiguration : IEntityTypeConfiguration<Pressure>
    {
        public void Configure(EntityTypeBuilder<Pressure> builder)
        {
            builder.OwnsOne(e => e.Coordinates, CoordinatesEntityTypeConfiguration.Configure);
        }
    }
}