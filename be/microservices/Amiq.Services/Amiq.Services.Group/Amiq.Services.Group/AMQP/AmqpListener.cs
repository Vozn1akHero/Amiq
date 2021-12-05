using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Amiq.Services.Group.Amqp
{
    public class AmqpListener
    {
        public void Listen(string queue)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            // otwarcie połączenia
            using (var connection = factory.CreateConnection())
            // utworzenie kanału komunikacji
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queue,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                };
                channel.BasicConsume(queue: queue,
                    autoAck: true,
                    consumer: consumer);
            }
        }
    }
}
