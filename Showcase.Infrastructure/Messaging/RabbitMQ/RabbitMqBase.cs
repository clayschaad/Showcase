using RabbitMQ.Client;

namespace Showcase.Infrastructure.Messaging.RabbitMQ
{
    public class RabbitMqBase
    {
        public static void ExchangeDeclare(IModel channel, string queue)
        {
            var ttl = new Dictionary<string, object>
            {
                { "x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare($"{queue}-exchange", ExchangeType.Topic, arguments: ttl);
        }

        public static string GetRoutingKey<T>(T message) where T : notnull
        {
            var routingKey = typeof(T).Name;
            return routingKey;
        }
    }
}
