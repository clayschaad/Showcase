using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Showcase.Domain.Measurements.Temperatures;
using System.Text;
using System.Text.Json;

namespace Showcase.Infrastructure.Messaging.RabbitMQ
{
    public class RabbitMqSending : ITemperatureSending
    {
        private readonly IConfiguration configuration;

        public RabbitMqSending(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendTemperatureAsync(Temperature temperature, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
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

                    var jsonOptions = new JsonSerializerOptions() { WriteIndented = true };
                    var jsonString = JsonSerializer.Serialize(temperature, jsonOptions);
                    var body = Encoding.UTF8.GetBytes(jsonString);

                    channel.BasicPublish(exchange: "",
                                         routingKey: options.Queue,
                                         basicProperties: null,
                                         body: body);
                }
            });
        }
    }
}
