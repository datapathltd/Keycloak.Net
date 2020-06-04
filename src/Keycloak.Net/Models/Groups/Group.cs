using Newtonsoft.Json;

namespace Keycloak.Net.Models.Groups
{
    public class Group
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("path")]
        public string Path { get; set; }
    }

    public class UserGroupUpdate
    {
        [JsonProperty("groupId")]
        public string GroupId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("realm")]
        public string Realm { get; set; }
    }
}
