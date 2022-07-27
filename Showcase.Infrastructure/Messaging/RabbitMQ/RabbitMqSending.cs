using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Showcase.Domain.Measurements.Temperatures;

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
                using var connection = factory.CreateConnection();
                var channel = connection.CreateModel();
                RabbitMqPublisher.Publish(channel, options.Queue, temperature);
            });
        }
    }
}
