using System.Threading.Tasks;

namespace Forms.Infrastructure.Tests.ServiceTests.RedisCacheFrameworkTests.GetTests
{
    public class WhenCallingGet : WhenTestingTheRedisCacheService
    {
        protected ResourceToCache Result { get; set; }

        protected async Task Act()
        {
            Result = await Service.Get<ResourceToCache>(CacheKey);
        }
    }
}
