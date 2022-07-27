using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Showcase.Domain.Measurements.Temperatures;
using Showcase.Infrastructure;
using Showcase.Infrastructure.Messaging;
using System.Text;
using System.Text.Json;

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

            var temperaturePersistance = serviceScope.ServiceProvider.GetRequiredService<ITemperaturePersistance>();

            var options = configuration.GetSection(MessagingOptions.SectionKey).Get<MessagingOptions>();
            var factory = new ConnectionFactory() { HostName = options.Hostname, Password = options.Password, UserName = options.Username, Port = options.Port };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: options.Queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var temperature = JsonSerializer.Deserialize<Temperature>(message);
                    temperaturePersistance.SaveTemperatureAsync(temperature, CancellationToken.None);
                };

                channel.BasicConsume(queue: options.Queue, autoAck: true, consumer: consumer);

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
                    services.AddDatabaseInfrastructureServices(configuration))
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


