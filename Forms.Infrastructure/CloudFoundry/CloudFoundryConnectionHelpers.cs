using System;
using System.Linq;
using System.Text.Json;
using Forms.Infrastructure.CloudFoundry.Models;
using StackExchange.Redis;

namespace Forms.Infrastructure.CloudFoundry
{
    public static class CloudFoundryConnectionHelpers
    {
        public static ConnectionMultiplexer CreateRedisConnection()
        {
            // Grab the Environment Variable from Cloud Foundry and parse the services
            var paasServices = Environment.GetEnvironmentVariable("VCAP_SERVICES");
            var paasServicesAsJson = JsonSerializer.Deserialize<PaasServices>(paasServices);

            var credentials = paasServicesAsJson.Redis.First().Credentials;

            var configOptions = new ConfigurationOptions
            {
                EndPoints = {{credentials.Host, credentials.Port}},
                Password = credentials.Password,
                Ssl = credentials.TlsEnabled
            };
                
            return ConnectionMultiplexer.Connect(configOptions);
        }
    }
}