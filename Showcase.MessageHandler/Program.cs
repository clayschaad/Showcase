using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using Showcase.Domain.Measurements.Weather;
using Showcase.Infrastructure;
using Showcase.Infrastructure.Messaging;
using Showcase.Infrastructure.Messaging.RabbitMQ;

namespace Showcase.MessageHandler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Message Handler (please ensure rabbitmq is up)...");

            var configuration = CreateConfiguration(args);
            using var host = CreateHost(args, configuration);
            using var serviceScope = CreateServiceScope(host);

            var options = configuration.GetSection(MessagingOptions.SectionKey).Get<MessagingOptions>();
            var factory = new ConnectionFactory() { HostName = options.Hostname, Password = options.Password, UserName = options.Username, Port = options.Port };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var weatherMeasurementPersistance = serviceScope.ServiceProvider.GetRequiredService<IWeatherMeasurementPersistance>();
                RabbitMqHelper.Init(channel, options.Queue);
                RabbitMqHelper.Consume<Temperature>(channel, options.Queue, (T) => weatherMeasurementPersistance.SaveTemperatureAsync(T, CancellationToken.None));
                RabbitMqHelper.Consume<Pressure>(channel, options.Queue, (T) => weatherMeasurementPersistance.SavePressureAsync(T, CancellationToken.None));

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            } 
        }

        private static IConfiguration CreateConfiguration(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            return configuration;
        }

        private static IHost CreateHost(string[] args, IConfiguration configuration)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddInfrastructureServices(configuration))
                .Build();

            return host;
        }

        private static IServiceScope CreateServiceScope(IHost host)
        {
            var serviceScope = host.Services.CreateScope();
            return serviceScope;
        }
    }
}


