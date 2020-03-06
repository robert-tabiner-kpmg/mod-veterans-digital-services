using StackExchange.Redis;

namespace Forms.Infrastructure.Cache.ConnectionFactory
{
    public interface IRedisConnectionFactory
    {
        IDatabase GetDatabase();
    }
}