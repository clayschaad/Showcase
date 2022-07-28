using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Showcase.Domain;
using Showcase.Domain.Measurements;
using Showcase.Domain.Measurements.Weather;
using Showcase.Infrastructure.Measurement;
using Showcase.Infrastructure.Messaging.RabbitMQ;
using Showcase.Infrastructure.Persistence.Database;

namespace Showcase.Infrastructure
{
    public static class ServiceProviderConfig
    {
        public static IServiceCollection AddDatabaseInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IWeatherMeasurementService), typeof(WeatherMeasurementService));
            services.AddTransient(typeof(IWeatherMeasurement), typeof(OpenWeatherMapMeasurement));
            services.AddTransient(typeof(IWeatherMeasurementPersistance), typeof(DatabaseWeatherMeasurementPersistance));
            services.AddTransient(typeof(IWeatherMeasurementSender), typeof(RabbitMqSending));

            var databaseOptions = configuration
                .GetSection(DatabaseOptions.SectionKey)
                .Get<DatabaseOptions>();

            services.AddDbContext<MeasurementDbContext>(
                opt => opt.UseSqlite($"Data Source={databaseOptions.DbPath}"));

            return services;
        }
    }
}
