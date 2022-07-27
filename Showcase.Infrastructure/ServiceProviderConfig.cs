using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Showcase.Domain;
using Showcase.Domain.Measurements;
using Showcase.Domain.Measurements.Temperatures;
using Showcase.Infrastructure.Measurement;
using Showcase.Infrastructure.Messaging.RabbitMQ;
using Showcase.Infrastructure.Persistence.Database;

namespace Showcase.Infrastructure
{
    public static class ServiceProviderConfig
    {
        public static IServiceCollection AddDatabaseInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(ITemperatureService), typeof(TemperatureService));
            services.AddTransient(typeof(ITemperatureMeasurement), typeof(OpenWeatherMapMeasurement));
            services.AddTransient(typeof(ITemperaturePersistance), typeof(DatabaseTemperaturePersistance));
            services.AddTransient(typeof(ITemperatureSending), typeof(RabbitMqSending));

            var databaseOptions = configuration
                .GetSection(DatabaseOptions.SectionKey)
                .Get<DatabaseOptions>();

            services.AddDbContext<MeasurementDbContext>(
                opt => opt.UseSqlite($"Data Source={databaseOptions.DbPath}"));

            return services;
        }
    }
}
