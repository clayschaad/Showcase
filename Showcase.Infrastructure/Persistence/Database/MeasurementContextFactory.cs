using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Showcase.Infrastructure.Persistence.Database
{
    public class MeasurementContextFactory : IDesignTimeDbContextFactory<MeasurementDbContext>
    {
        /// <summary>
        /// Gets called when 'dotnet ef migrations/database' is executed
        /// </summary>
        public MeasurementDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MeasurementDbContext>();
            optionsBuilder.UseSqlite(@"Data Source=../measurement.db");

            return new MeasurementDbContext(optionsBuilder.Options);
        }
    }
}
