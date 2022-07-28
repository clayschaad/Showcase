using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Showcase.Domain.Measurements.Weather;

namespace Showcase.Infrastructure.Messaging.RabbitMQ
{
    public class RabbitMqSending : IWeatherMeasurementSender
    {
        private readonly IConfiguration configuration;

        public RabbitMqSending(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendWeatherMeasurement<T>(T measurement, CancellationToken cancellationToken) where T : notnull
        {
            await Task.Run(() =>
            {
                var options = configuration.GetSection(MessagingOptions.SectionKey).Get<MessagingOptions>();
                var factory = new ConnectionFactory() { HostName = options.Hostname, Password = options.Password, UserName = options.Username, Port = options.Port };
                using var connection = factory.CreateConnection();
                var channel = connection.CreateModel();
                RabbitMqHelper.Publish(channel, options.Queue, measurement);
            });
        }
    }
}
