using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Showcase.Infrastructure.Messaging.RabbitMQ
{
    public class RabbitMqConsumer
    {
        public static void Consume<T>(IModel channel, string queue, Action<T> action)
        {
            channel.ExchangeDeclare($"{queue}-exchange", ExchangeType.Direct);
            channel.QueueDeclare(queue, durable: true, exclusive: false, autoDelete: false, arguments: null);

            channel.QueueBind(queue, $"{queue}-exchange", nameof(T));
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var obj = JsonSerializer.Deserialize<T>(message);
                action(obj);
            };

            channel.BasicConsume(queue, true, consumer);
        }
    }
}
