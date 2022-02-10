using Amiq.Services.Post.Messaging.IntegrationEvents;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Amiq.Services.Post.Messaging
{
    public sealed class RabbitMQPublisher : IDisposable
    {
        private static IConnection _connection;
        private static IModel _channel;
        private static JsonSerializerOptions SerializeOptions { get; } = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        static RabbitMQPublisher()
        {
            _connection = RabbitMQConnectionFactory.Factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout, true);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        public static void Publish<T>(T @event) where T : IntegrationEvent
        {

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event, SerializeOptions));
            //string message = Encoding.UTF8.GetString(body.ToArray());
            _channel.BasicPublish(exchange: "trigger",
               routingKey: "",
               basicProperties: null,
               body: body);
        }

        private static void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {

        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
