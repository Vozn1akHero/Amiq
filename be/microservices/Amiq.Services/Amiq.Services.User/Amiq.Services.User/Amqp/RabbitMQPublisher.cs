﻿using Amiq.Services.User.Amqp.IntegrationEvents;
using Amiq.Services.User.Contracts.User;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Amiq.Services.User.Amqp
{
    public sealed class RabbitMQPublisher : IDisposable
    {
        private static IConnection _connection;
        private static IModel _channel;

        static RabbitMQPublisher()
        {
            _connection = RabbitMQConnectionFactory.Factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        public static void Publish<T>(T @event) where T : IntegrationEvent
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
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
            RabbitMQPublisher._channel.Close();
            RabbitMQPublisher._connection.Close();
        }
    }
}
