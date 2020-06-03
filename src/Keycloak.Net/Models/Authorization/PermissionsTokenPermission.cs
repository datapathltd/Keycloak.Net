using System.Collections.Generic;
using Newtonsoft.Json;

namespace Keycloak.Net.Models.Authorization
{
    public class PermissionsTokenPermission
    {
        [JsonProperty("rsid")]
        public string ResourceId { get; set; }

        [JsonProperty("rsname")]
        public string ResourceName { get; set; }

        [JsonProperty("scopes")]
        public IEnumerable<string> Scopes { get; set; }
    }
}
