using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace Logic.RabbitMq
{
    public class RabbitMqClient
    {
        private const string OrdersQueue = "Orders";
        private readonly IChannel _channel;
        private readonly IConnection _connection;

        public RabbitMqClient(string hostName = "localhost", string userName = "rabbitmq", string password = "rabbitmq")
        {
            var factory = new ConnectionFactory
            {
                HostName = hostName,
                UserName = userName,
                Password = password
            };

            _connection = factory.CreateConnectionAsync().Result;
            _channel = _connection.CreateChannelAsync().Result;
            _channel.QueueDeclareAsync(
                    queue: OrdersQueue,
                    durable: true,
                    exclusive: false,
                    autoDelete: false);
        }

        public Task PublishQueue(object content)
        {
            var strContent = JsonConvert.SerializeObject(content);

            if (string.IsNullOrWhiteSpace(strContent))
                throw new ArgumentException("O conteúdo não pode ser vazio ou nulo.", nameof(content));

            var contentAsBytes = Encoding.UTF8.GetBytes(strContent);
            return _channel.BasicPublishAsync(
               exchange: string.Empty,
               routingKey: OrdersQueue,
               body: contentAsBytes).AsTask();
        }

        public Task<BasicGetResult?>? ReceiveQueue()
        {
            return _channel.BasicGetAsync(OrdersQueue, autoAck: true);
        }

        public void Dispose()
        {
            _channel?.CloseAsync().Wait();
            _channel?.Dispose();
            _connection?.CloseAsync().Wait();
            _connection?.Dispose();
        }
    }
}
