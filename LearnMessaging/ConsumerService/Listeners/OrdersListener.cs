using Logic.RabbitMq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ConsumerService.Listeners
{
    public class OrdersListener(RabbitMqClient mqClient)
    {
        private readonly RabbitMqClient _mqClient = mqClient ?? throw new ArgumentNullException(nameof(mqClient));

        public void ProcessQueues()
        {
            var consumer = new AsyncEventingBasicConsumer(_mqClient._channel);
            consumer.ReceivedAsync += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"queue: {message}");
                await _mqClient._channel.BasicAckAsync(deliveryTag: eventArgs.DeliveryTag, multiple: false);
            };

            _mqClient._channel.BasicConsumeAsync(
                queue: RabbitMqClient.OrdersQueue,
                autoAck: false,
                consumer: consumer
            ).Wait();

            while (true)
            {
                Thread.Sleep(10);
            }
        }
    }
}
