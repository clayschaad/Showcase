using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Showcase.Infrastructure.Messaging.RabbitMQ
{
    public class RabbitMqPublisher
    {
        public static void Publish<T>(IModel channel, string queue, T message)
        {
            var ttl = new Dictionary<string, object>
            {
                { "x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare($"{queue}-exchange", ExchangeType.Direct, arguments: ttl);

            var jsonOptions = new JsonSerializerOptions() { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(message, jsonOptions);
            var body = Encoding.UTF8.GetBytes(jsonString);
            channel.BasicPublish($"{queue}-exchange", nameof(T), null, body);
        }
    }
}
