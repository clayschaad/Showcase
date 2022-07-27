using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Showcase.Domain.Measurements.Temperatures;
using Showcase.Infrastructure.Messaging;
using System.Text;
using System.Text.Json;

namespace Showcase.MessageHandler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var options = configuration.GetSection(MessagingOptions.SectionKey).Get<MessagingOptions>();
            var factory = new ConnectionFactory() { HostName = options.Hostname, Password = options.Password, UserName = options.Username, Port = options.Port };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: options.Queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);

                    var temperature = JsonSerializer.Deserialize<Temperature>(message);
                };

                channel.BasicConsume(queue: options.Queue,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            } 
        }
    }
}


