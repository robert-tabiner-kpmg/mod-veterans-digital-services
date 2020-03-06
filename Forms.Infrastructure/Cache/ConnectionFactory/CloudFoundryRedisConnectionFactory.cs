using Forms.Infrastructure.CloudFoundry;
using StackExchange.Redis;

namespace Forms.Infrastructure.Cache.ConnectionFactory
{
    public class CloudFoundryRedisConnectionFactory : IRedisConnectionFactory
    {
        private ConnectionMultiplexer _redisConnectionMultiplexer;

        public IDatabase GetDatabase()
        {
            if (_redisConnectionMultiplexer == null)
            {
                _redisConnectionMultiplexer = CloudFoundryConnectionHelpers.CreateRedisConnection();
            }

            return _redisConnectionMultiplexer.GetDatabase();
        }
    }
}