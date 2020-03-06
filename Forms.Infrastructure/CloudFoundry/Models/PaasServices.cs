using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Forms.Infrastructure.CloudFoundry.Models
{
    public class PaasServices
    {
        [JsonPropertyName("redis")]
        public List<Redis> Redis { get; set; }
    }
}