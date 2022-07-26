using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Showcase.Domain;
using Showcase.Domain.Measurements;
using Showcase.Domain.Measurements.Temperatures;
using Showcase.Infrastructure.Measurement;
using Showcase.Infrastructure.Persistence.Database;

namespace Showcase.Infrastructure
{
    public static class ServiceProviderConfig
    {
        public static IServiceCollection AddMemoryInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(ITemperatureService), typeof(TemperatureService));
            services.AddTransient(typeof(ITemperatureMeasurement), typeof(TemperatureMeasurement));
            services.AddTransient(typeof(ITemperaturePersistance), typeof(Persistence.Memory.TemperaturePersistance));

            return services;
        }

        public static IServiceCollection AddDatabaseInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(ITemperatureService), typeof(TemperatureService));
            services.AddTransient(typeof(ITemperatureMeasurement), typeof(TemperatureMeasurement));
            services.AddTransient(typeof(ITemperaturePersistance), typeof(TemperaturePersistance));

            var databaseOptions = configuration
                .GetSection(DatabaseOptions.SectionKey)
                .Get<DatabaseOptions>();

            services.AddDbContext<MeasurementDbContext>(
                opt => opt.UseSqlite($"Data Source={databaseOptions.DbPath}"));

            return services;
        }
    }
}
