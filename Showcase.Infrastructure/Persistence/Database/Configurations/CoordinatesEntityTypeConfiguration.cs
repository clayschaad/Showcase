﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Showcase.Domain.Measurements;

namespace Showcase.Infrastructure.Persistence.Database
{
    public static class CoordinatesEntityTypeConfiguration
    {
        public static void Configure<T>(OwnedNavigationBuilder<T, Coordinates> builder) where T : class
        {
            builder.Property(m => m.Latitude);
            builder.Property(m => m.Longitude);
        }
    }
}
