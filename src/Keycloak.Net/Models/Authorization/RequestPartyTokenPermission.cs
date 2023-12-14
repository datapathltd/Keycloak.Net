using Newtonsoft.Json;

namespace Keycloak.Net.Models.Authorization
{
    public class RequestPartyTokenPermission
    {
        [JsonProperty("upgraded")]
        public bool Upgraded { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("refresh_expires_in")]
        public int RefreshExpiresIn { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("not-before-policy")]
        public int NotBeforePolicy { get; set; }
    }  
}
