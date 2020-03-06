using System.Text.Json.Serialization;

namespace Forms.Infrastructure.CloudFoundry.Models
{
    public class Credentials
    {
        [JsonPropertyName("host")]        
        public string Host { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("port")]
        public int Port { get; set; }
        [JsonPropertyName("tls_enabled")]
        public bool TlsEnabled { get; set; }
    }
}