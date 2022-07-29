using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Showcase.Infrastructure.Measurement.OpenWeatherMap;
using Showcase.Infrastructure.Measurement.Polygon;
using Showcase.Infrastructure.Messaging.RabbitMQ;
using Showcase.Infrastructure.Persistence.Database;
using Showcase.Measurement.Application;
using Showcase.Measurement.Application.Finance;
using Showcase.Measurement.Application.Weather;
using Showcase.Measurement.Domain;
using Showcase.Measurement.Domain.Finance;
using Showcase.Measurement.Domain.Weather;

namespace Showcase.Infrastructure
{
    public static class ServiceProviderConfig
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddWeatherServices(configuration);
            services.AddStockServices(configuration);

            services.AddTransient(typeof(IMeasurementSender), typeof(RabbitMqSending));

            var databaseOptions = configuration
                .GetSection(DatabaseOptions.SectionKey)
                .Get<DatabaseOptions>();

            services.AddDbContext<MeasurementDbContext>(
                opt => opt.UseSqlite($"Data Source={databaseOptions.DbPath}"));

            return services;
        }

        private static IServiceCollection AddWeatherServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IWeatherMeasurementService), typeof(WeatherMeasurementService));
            services.AddTransient(typeof(IWeatherMeasurement), typeof(OpenWeatherMapMeasurement));
            services.AddTransient(typeof(IWeatherMeasurementPersistance), typeof(DatabaseWeatherMeasurementPersistance));

            return services;
        }

        private static IServiceCollection AddStockServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IFinanceMeasurementService), typeof(FinanceMeasurementService));
            services.AddTransient(typeof(IFinanceMeasurement), typeof(PolygonMeasurement));

            return services;
        }
    }
}
