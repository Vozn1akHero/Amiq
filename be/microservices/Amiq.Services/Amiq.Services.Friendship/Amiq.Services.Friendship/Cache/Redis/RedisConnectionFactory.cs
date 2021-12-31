using StackExchange.Redis;

namespace Amiq.Services.Friendship.Cache.Redis
{
    public static class RedisConnectionFactory
    {
        static RedisConnectionFactory()
        {
            _connection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect("localhost:6379");
            });
        }

        private static Lazy<ConnectionMultiplexer> _connection;

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return _connection.Value;
            }
        }

        public static IDatabase Db => Connection.GetDatabase();
    }
}
