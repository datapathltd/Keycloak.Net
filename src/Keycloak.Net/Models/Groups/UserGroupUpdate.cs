namespace Keycloak.Net.Models.Groups
{
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