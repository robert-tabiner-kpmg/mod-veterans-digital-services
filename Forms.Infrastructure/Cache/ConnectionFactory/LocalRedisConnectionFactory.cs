using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Forms.Infrastructure.Cache.ConnectionFactory
{
    public class LocalRedisConnectionFactory : IRedisConnectionFactory
    {
        private ConnectionMultiplexer _redisConnectionMultiplexer;
        private readonly IConfiguration _configuration;
        
        public LocalRedisConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public IDatabase GetDatabase()
        {
            if (_redisConnectionMultiplexer == null)
            {
                _redisConnectionMultiplexer = ConnectionMultiplexer.Connect(_configuration["Redis:Uri"]);
            }

            return _redisConnectionMultiplexer.GetDatabase();
        }
    }

}