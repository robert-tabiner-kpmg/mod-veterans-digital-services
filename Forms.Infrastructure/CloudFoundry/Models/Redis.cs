using System.Text.Json.Serialization;

namespace Forms.Infrastructure.CloudFoundry.Models
{
    public class Redis
    {
        [JsonPropertyName("credentials")]
        public Credentials Credentials { get; set; }
    }
}