using System.Threading.Tasks;

namespace Forms.Infrastructure.Tests.ServiceTests.RedisCacheFrameworkTests.SaveTests
{
    public class WhenCallingSaveOnCache : WhenTestingTheRedisCacheService
    {
        protected async Task Act()
        {
            await Service.Save(CacheKey, Resource);
        }
    }
}
