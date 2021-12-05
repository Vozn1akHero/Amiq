using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Amiq.Services.Group.Amqp
{
    public class AmqpSender
    {
        public void Send(string msg, string queue)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            // otwarcie połączenia
            using (var connection = factory.CreateConnection())
            {
                // utworzenie kanału komunikacji
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queue,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                    channel.BasicPublish(exchange: "",
                        routingKey: queue,
                        basicProperties: null,
                        body: Encoding.UTF8.GetBytes(msg));
                }
            }
        }
    }
}
