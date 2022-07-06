using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Keycloak.Net.Models.Components
{
    public class Component
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("providerId")]
        public string ProviderId { get; set; }
        [JsonProperty("providerType")]
        public string ProviderType { get; set; }
        [JsonProperty("parentId")]
        public string ParentId { get; set; }
        [JsonProperty("subType")]
        public string SubType { get; set; }
        [JsonProperty("config")]
        public Dictionary<string, string[]> Config { get; set; }

        public string GetKeyIfExists(string key)
        {
            var exists = Config.TryGetValue(key, out var value);
            // The Config dictionary values returned by Keycloak always contain string arrays with a single value
            // so extract the first member of the array when the key exists.
            return exists ? value[0] : null;
        }

        public T GetKeyIfExists<T>(string key, Func<string, T> transform)
        {
            var value = GetKeyIfExists(key);
            return value == null ? default : transform.Invoke(value);
        }

        public void SetKey(string key, object value)
        {
            if (value == null) return;

            if (value is bool)
            {
                // Keycloak requires booleans to be represented as lowercase "true" or "false"
                Config[key] = new[] { value.ToString().ToLower() };
            }
            else
            {
                Config[key] = new[] { value.ToString() };
            }

        }
    }
}