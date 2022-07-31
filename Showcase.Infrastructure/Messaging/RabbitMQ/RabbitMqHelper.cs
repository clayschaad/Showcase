using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Showcase.Infrastructure.Messaging.RabbitMQ
{
    public class RabbitMqHelper
    {
        public static void Init(IModel channel, string queue)
        {
            var ttl = new Dictionary<string, object>
            {
                { "x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare($"{queue}-exchange", ExchangeType.Topic, arguments: ttl);
            channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
            channel.BasicQos(0, 10, false);
        }

        public static void Publish<T>(IModel channel, string queue, T message) where T : notnull
        {
            var jsonOptions = new JsonSerializerOptions() { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(message, jsonOptions);
            var body = Encoding.UTF8.GetBytes(jsonString);
            channel.BasicPublish($"{queue}-exchange", typeof(T).Name, null, body);
        }

        public static void Consume<T>(IModel channel, string queue, Action<T> action) where T : notnull
        {
            channel.QueueBind(queue, $"{queue}-exchange", typeof(T).Name);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var obj = JsonSerializer.Deserialize<T>(message);
                action(obj!);
                channel.BasicAck(e.DeliveryTag, false);
            };

            channel.BasicConsume(queue, false, consumer);
        }
    }
}
