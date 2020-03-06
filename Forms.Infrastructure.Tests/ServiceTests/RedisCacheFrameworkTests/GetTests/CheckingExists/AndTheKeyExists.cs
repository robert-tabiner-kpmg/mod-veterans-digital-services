using Moq;
using StackExchange.Redis;

namespace Forms.Infrastructure.Tests.ServiceTests.RedisCacheFrameworkTests.GetTests.CheckingExists
{
    public abstract class AndTheKeyExists : WhenCallingGet
    {
        protected AndTheKeyExists()
        {
            MockRedisDatabase.Setup(x => x.KeyExistsAsync(CacheKey, CommandFlags.None)).ReturnsAsync(true);
        }
    }
}
