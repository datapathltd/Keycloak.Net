using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Keycloak.Net.Models.Authorization
{
    public class Resource
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("attributes")]
        public IDictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("icon_uri")]
        public string IconUri { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ownerManagedAccess")]
        public bool OwnerManagedAccess { get; set; }

        [JsonProperty("scopes")]
        public IEnumerable<Scope> Scopes { get; set; } = Enumerable.Empty<Scope>();

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("uris")]
        public IEnumerable<string> Uris { get; set; } = Enumerable.Empty<string>();
    }
}
