using System.Threading.Tasks;
using Flurl.Http;
using Keycloak.Net.Models.Authorization;

namespace Keycloak.Net
{
    public partial class KeycloakClient
    {
        public async Task<string> CreateResourceAsync(string realm, string clientId, Resource resource)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/resource")
                .PostJsonAsync(resource)
                .ReceiveJson<Resource>()
                .ConfigureAwait(false);

            return response.Id;
        }

        public async Task<Resource> GetResourceAsync(string realm, string clientId, string resourceId)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/resource/{resourceId}")
                .GetAsync()
                .ReceiveJson<Resource>()
                .ConfigureAwait(false);

            return response;
        }

        public async Task<bool> UpdateResourceAsync(string realm, string clientId, string resourceId, Resource resource)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/resource/{resourceId}")
                .PutJsonAsync(resource)
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteResourceAsync(string realm, string clientId, string resourceId)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/resource/{resourceId}")
                .DeleteAsync()
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }
    }
}
