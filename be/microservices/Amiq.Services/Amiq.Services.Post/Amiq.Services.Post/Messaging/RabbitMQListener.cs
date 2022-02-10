using Amiq.Services.Friendship.Contracts.User;
using Amiq.Services.Friendship.DataAccessLayer;
using Amiq.Services.Post.Messaging.IntegrationEvents;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Amiq.Services.Post.Messaging
{
    public class RabbitMQListener : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;

        private readonly DaoUser _daoUser;

        public RabbitMQListener()
        {
            _connection = RabbitMQConnectionFactory.Factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout, true);
            _queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: _queueName,
                exchange: "trigger",
                routingKey: "");

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

            _daoUser = new DaoUser();
        }

        private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {

        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                string message = Encoding.UTF8.GetString(body.ToArray());

                _channel.BasicAck(ea.DeliveryTag, false);

                var integrationEvent = JsonSerializer.Deserialize<IntegrationEvent>(message);
                switch (integrationEvent.EventName)
                {
                    case nameof(UserModificationEvent):
                        UserModificationEvent @event = JsonSerializer.Deserialize<UserModificationEvent>(message);
                        DtoBasicUserInfo basicUserInfo = new DtoBasicUserInfo
                        {
                            UserId = @event.UserId,
                            Name = @event.Name,
                            Surname = @event.Surname,
                            AvatarPath = @event.AvatarPath
                        };
                        _daoUser.AddOrUpdate(basicUserInfo);
                        break;
                }

            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume(queue: _queueName, consumer: consumer);

            return Task.CompletedTask;
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
