using Microsoft.EntityFrameworkCore;
using Showcase.Domain.Measurements.Temperatures;

namespace Showcase.Infrastructure.Persistence.Database
{
    public class MeasurementDbContext : DbContext
    {
        public DbSet<Temperature> Temperatures { get; set; } = null!;

        //public string DbPath { get; } = null!;

        // For design time
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlite(@"Data Source=measurement.db");

        //public MeasurementDbContext()
        //{
        //}

        public MeasurementDbContext(DbContextOptions<MeasurementDbContext> options) : base(options)
        { }
    }
}
