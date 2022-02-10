using RabbitMQ.Client;

namespace Amiq.Services.Friendship.Amqp
{
    public static class RabbitMQConnectionFactory
    {
        static RabbitMQConnectionFactory()
        {
            //_factory = new ConnectionFactory() { HostName = "localhost" };
            //_factory = new ConnectionFactory() { HostName = "rabbitmq-clusterip-srv" };
            _factory = new ConnectionFactory() { HostName = "host.docker.internal" };
            _factory.Password = "123dimon";
            _factory.UserName = "sa";
        }

        public static ConnectionFactory _factory;

        public static ConnectionFactory Factory => _factory;
    }
}
