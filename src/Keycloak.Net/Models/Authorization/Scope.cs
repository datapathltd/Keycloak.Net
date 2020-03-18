using Newtonsoft.Json;

namespace Keycloak.Net.Models.Authorization
{
    public class Scope
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("iconUri")]
        public string IconUri { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}