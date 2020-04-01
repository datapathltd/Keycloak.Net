using Newtonsoft.Json;

namespace Keycloak.Net.Models.Authorization
{
    public class PolicyGroup
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("extendChildren")]
        public bool ExtendChildren { get; set; }
    }
}