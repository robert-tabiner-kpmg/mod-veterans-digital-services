using System.Collections.Generic;
using Forms.Infrastructure.Cache;
using Forms.Infrastructure.Cache.ConnectionFactory;
using Moq;
using StackExchange.Redis;

namespace Forms.Infrastructure.Tests.ServiceTests.RedisCacheFrameworkTests
{
    public abstract class WhenTestingTheRedisCacheService
    {
        protected string CacheKey { get; set; } = "Cache_Key_123";
        protected ResourceToCache Resource { get; set; }
        protected RedisCacheFramework Service { get; set; }
        private Mock<IRedisConnectionFactory> MockConnectionFactory { get; set; }
        protected Mock<IDatabase> MockRedisDatabase { get; set; }

        protected WhenTestingTheRedisCacheService()
        {
            MockConnectionFactory = new Mock<IRedisConnectionFactory>();
            MockRedisDatabase = new Mock<IDatabase>();

            MockConnectionFactory.Setup(x => x.GetDatabase()).Returns(MockRedisDatabase.Object);

            Service = new RedisCacheFramework(MockConnectionFactory.Object);
        }

        public class ResourceToCache
        {
            public string Name { get; set; }
            public int Int { get; set; }
            public List<object> ObjectsList { get; set; }
        }
    }
}