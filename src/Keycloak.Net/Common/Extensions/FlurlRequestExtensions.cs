using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace Keycloak.Net.Common.Extensions
{
    public static class FlurlRequestExtensions
    {
        private static async Task<string> GetAccessTokenAsync(string url, string realm, OAuthCredentials credentials)
        {
            var result = await url
                .AppendPathSegment($"/auth/realms/{realm}/protocol/openid-connect/token")
                .WithHeader("Accept", "application/json")
                .PostUrlEncodedAsync(credentials.GetRequestBody())
                .ReceiveJson();

            string accessToken = result
                .access_token.ToString();

            return accessToken;
        }

        private static string GetAccessToken(string url, string realm, string userName, string password) 
            => GetAccessTokenAsync(url, realm, new PasswordGrantCredentials(userName, password)).GetAwaiter().GetResult();

        public static IFlurlRequest WithAuthentication(this IFlurlRequest request, Func<string> getToken, string url, string realm, string userName, string password)
        {
            if (getToken != null)
            {
                string token = getToken();
                return request.WithOAuthBearerToken(token);
            }

            return request.WithOAuthBearerToken(GetAccessToken(url, realm, userName, password));
        }

        public static IFlurlRequest WithAuthentication(this IFlurlRequest request, string url, string realm, OAuthCredentials credentials)
        {
            return request.WithOAuthBearerToken(GetAccessTokenAsync(url, realm, credentials).GetAwaiter().GetResult());
        }
    }
}
