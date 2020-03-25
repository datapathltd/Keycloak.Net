using System.Collections.Generic;

namespace Keycloak.Net
{
    public abstract class OAuthCredentials
    {
        public abstract List<KeyValuePair<string, string>> GetRequestBody();
    }

    public class PasswordGrantCredentials : OAuthCredentials
    {
        private readonly string _username;
        private readonly string _password;

        public PasswordGrantCredentials(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public override List<KeyValuePair<string, string>> GetRequestBody()
        {
            return new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", _username),
                new KeyValuePair<string, string>("password", _password),
                new KeyValuePair<string, string>("client_id", "admin-cli")
            };
        }
    }

    public class ClientCredentialsGrantCredentials : OAuthCredentials
    {
        private readonly string _clientId;
        private readonly string _clientSecret;

        public ClientCredentialsGrantCredentials(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public override List<KeyValuePair<string, string>> GetRequestBody()
        {
            return new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("client_secret", _clientSecret)
            };
        }
    }
}
