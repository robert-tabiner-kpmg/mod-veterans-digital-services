using Common.ExternalFrameworks;
using Common.Frameworks;
using Forms.Infrastructure.Cache;
using Forms.Infrastructure.Cache.ConnectionFactory;
using Forms.Infrastructure.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forms.Infrastructure
{
    public static class DependencyInjectionExtensions
    {
        public static IRedisConnectionFactory AddCacheFramework(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
        {
            var connectionFactory = isDevelopment
                ? new LocalRedisConnectionFactory(configuration) as IRedisConnectionFactory
                : new CloudFoundryRedisConnectionFactory();
            
            services.AddSingleton(connectionFactory);
            services.AddScoped<ICacheFramework, RedisCacheFramework>();
            
            return connectionFactory;
        }
        public static void AddEmailFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailFrameworkOptions>(configuration.GetSection("Email"));
            services.AddScoped<IEmailFramework, NotifyEmailFramework>();
        }
        
    }
}