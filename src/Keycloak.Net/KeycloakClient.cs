using System;
using Flurl;
using Flurl.Http;
using Flurl.Http.Configuration;
using Keycloak.Net.Common.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Keycloak.Net
{
    public partial class KeycloakClient
    {

        private static readonly ISerializer s_serializer = new NewtonsoftJsonSerializer(new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
        });

        static KeycloakClient()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            };
        }

        private readonly Url _url;
        private readonly string _userName;
        private readonly string _password;
        private readonly Func<string> _getToken;
        private readonly OAuthCredentials _credentials;

        private KeycloakClient(string url)
        {
            _url = url;
        }

        public KeycloakClient(string url, string userName, string password)
            : this(url)
        {
            _userName = userName;
            _password = password;
        }

        public KeycloakClient(string url, Func<string> getToken)
            : this(url)
        {
            _getToken = getToken;
        }

        public KeycloakClient(string url, OAuthCredentials credentials)
            : this(url)
        {
            _credentials = credentials;
        }

        private IFlurlRequest GetBaseUrl(string authenticationRealm)
        {
            var url = new Url(_url)
                .AppendPathSegment("/auth")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer);

            return _credentials != null
                ? url.WithAuthentication(_url, authenticationRealm, _credentials)
                : url.WithAuthentication(_getToken, _url, authenticationRealm, _userName, _password);
        }

        private static IFlurlRequest GetCustomBaseUrl(Url url)
        {
            return new Url(url)
                .AppendPathSegment("/auth")
                .ConfigureRequest(settings => settings.JsonSerializer = s_serializer);
        }

    }
}
