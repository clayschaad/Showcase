using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Showcase.Infrastructure.Messaging.RabbitMQ
{
    public class RabbitMqPublisher : RabbitMqBase
    {
        public static void Publish<T>(IModel channel, string queue, T message) where T : notnull
        {
            //ExchangeDeclare(channel, queue);
            var jsonOptions = new JsonSerializerOptions() { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(message, jsonOptions);
            var body = Encoding.UTF8.GetBytes(jsonString);
            channel.BasicPublish($"{queue}-exchange", GetRoutingKey(message), null, body);
        }
    }
}
