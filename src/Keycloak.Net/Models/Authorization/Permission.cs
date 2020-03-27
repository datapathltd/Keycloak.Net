using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Keycloak.Net.Models.Authorization
{
    public class Permission
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type")]
        public PermissionType Type { get; set; }

        [JsonProperty("logic")]
        public PermissionLogic Logic { get; set; }

        [JsonProperty("decisionStrategy")]
        public PermissionDecisionStrategy DecisionStrategy { get; set; }

        [JsonProperty("resources")]
        public IEnumerable<string> Resources { get; set; } = Enumerable.Empty<string>();

        [JsonProperty("scopes")]
        public IEnumerable<string> Scopes { get; set; } = Enumerable.Empty<string>();

        [JsonProperty("policies")]
        public IEnumerable<string> Policies { get; set; } = Enumerable.Empty<string>();
    }
}
