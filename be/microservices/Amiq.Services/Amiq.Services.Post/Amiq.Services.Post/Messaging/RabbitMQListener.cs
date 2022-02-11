﻿using Amiq.Services.Post.Messaging.IntegrationEvents;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Amiq.Services.Post.DataAccessLayer.Models.Models;
using Amiq.Services.Post.Contracts.User;

namespace Amiq.Services.Post.Messaging
{
    public class RabbitMQListener : BackgroundService
    {
        private IConnection _connection;
        private IModel _channel;
        private string _queueName;
        private AmiqPostContext _amiqPostContext;

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

            _amiqPostContext = new AmiqPostContext();
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
                        {
                            var @event = JsonSerializer.Deserialize<UserModificationEvent>(message);
                            var dtoBasicUserInfo = new DtoBasicUserInfo
                            {
                                UserId = @event.UserId,
                                Name = @event.Name,
                                Surname = @event.Surname,
                                AvatarPath = @event.AvatarPath
                            };
                            var user = _amiqPostContext.Users.Find(dtoBasicUserInfo.UserId);
                            if (user == null)
                            {
                                _amiqPostContext.Users.Add(new User
                                {
                                    UserId = dtoBasicUserInfo.UserId,
                                    Name = dtoBasicUserInfo.Name,
                                    Surname = dtoBasicUserInfo.Surname,
                                    AvatarPath = dtoBasicUserInfo.AvatarPath
                                });
                            }
                            else
                            {
                                user.Name = dtoBasicUserInfo.Name;
                                user.Surname = dtoBasicUserInfo.Surname;
                                user.AvatarPath = dtoBasicUserInfo.AvatarPath;
                            }

                            break;
                        }
                }
                _amiqPostContext.SaveChanges();
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
