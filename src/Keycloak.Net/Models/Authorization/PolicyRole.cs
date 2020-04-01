using Newtonsoft.Json;

namespace Keycloak.Net.Models.Authorization
{
    public class PolicyRole
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("required")]
        public bool Required { get; set; }
    }
}
