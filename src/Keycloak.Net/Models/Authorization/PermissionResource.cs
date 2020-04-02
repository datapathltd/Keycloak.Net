﻿using Newtonsoft.Json;

namespace Keycloak.Net.Models.Authorization
{
    public class PermissionResource
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
