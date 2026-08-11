using Newtonsoft.Json;

namespace Keycloak.Net.Models.Authorization
{
    public class PolicyEvaluationRequest
    {
        [JsonProperty("resources")]
        public PermissionResource[] Resources { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("roleIds")]
        public string[] RoleIds { get; set; }

        [JsonProperty("entitlements")]
        public bool Entitlements { get; set; }
    }
}