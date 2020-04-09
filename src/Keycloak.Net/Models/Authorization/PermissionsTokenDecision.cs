using Newtonsoft.Json;

namespace Keycloak.Net.Models.Authorization
{
    public class PermissionsTokenDecision
    {
        [JsonProperty("result")]
        public bool Result { get; set; }
    }
}
