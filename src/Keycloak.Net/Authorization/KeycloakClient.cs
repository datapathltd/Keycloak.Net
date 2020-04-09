using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl.Http.Configuration;
using Keycloak.Net.Common.Converters;
using Keycloak.Net.Models.Authorization;
using Newtonsoft.Json;

namespace Keycloak.Net
{
    public partial class KeycloakClient
    {
        #region Resources

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

        public async Task<IEnumerable<Resource>> GetResourcesAsync(string realm, string clientId, int first = 0, int max = 20,
            string name = null, string owner = null, string scope = null, string type = null, string uri = null)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/resource")
                .SetQueryParam("deep", false)
                .SetQueryParam("first", first)
                .SetQueryParam("max", max)
                .SetQueryParam("name", name)
                .SetQueryParam("owner", owner)
                .SetQueryParam("scope", scope)
                .SetQueryParam("type", type)
                .SetQueryParam("uri", uri)
                .GetAsync()
                .ReceiveJson<IEnumerable<Resource>>()
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

        #endregion

        #region Permissions

        public async Task<string> CreatePermissionAsync(string realm, string clientId, Permission permission)
        {
            string permissionType = permission.Type.ToString().ToLower();
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/permission/{permissionType}")
                .PostJsonAsync(permission)
                .ReceiveJson<Permission>()
                .ConfigureAwait(false);

            return response.Id;
        }

        public async Task<Permission> GetPermissionAsync(string realm, string clientId, string permissionId, PermissionType type)
        {
            string permissionType = type.ToString().ToLower();
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/permission/{permissionType}/{permissionId}")
                .GetAsync()
                .ReceiveJson<Permission>()
                .ConfigureAwait(false);

            return response;
        }

        public async Task<IEnumerable<Permission>> GetPermissionsAsync(string realm, string clientId, int first = 0, int max = 20, 
            string name = null, string resource = null, string scope = null)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/permission")
                .SetQueryParam("first", first)
                .SetQueryParam("max", max)
                .SetQueryParam("name", name)
                .SetQueryParam("resource", resource)
                .SetQueryParam("scope", scope)
                .GetAsync()
                .ReceiveJson<IEnumerable<Permission>>()
                .ConfigureAwait(false);

            return response ?? Enumerable.Empty<Permission>();
        }

        public async Task<bool> UpdatePermissionAsync(string realm, string clientId, string permissionId, Permission permission)
        {
            string permissionType = permission.Type.ToString().ToLower();
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/permission/{permissionType}/{permissionId}")
                .PutJsonAsync(permission)
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePermissionAsync(string realm, string clientId, string permissionId, PermissionType type)
        {
            string permissionType = type.ToString().ToLower();
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/permission/{permissionType}/{permissionId}")
                .DeleteAsync()
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<PermissionResource>> GetPermissionResourcesAsync(string realm, string clientId, string permissionId)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/permission/{permissionId}/resources")
                .GetAsync()
                .ReceiveJson<IEnumerable<PermissionResource>>()
                .ConfigureAwait(false);

            return response ?? Enumerable.Empty<PermissionResource>();
        }

        public async Task<IEnumerable<PermissionScope>> GetPermissionScopesAsync(string realm, string clientId, string permissionId)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/permission/{permissionId}/scopes")
                .GetAsync()
                .ReceiveJson<IEnumerable<PermissionScope>>()
                .ConfigureAwait(false);

            return response ?? Enumerable.Empty<PermissionScope>();
        }

        public async Task<IEnumerable<PermissionAssociatedPolicy>> GetPermissionPoliciesAsync(string realm, string clientId, string permissionId)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/permission/{permissionId}/associatedPolicies")
                .GetAsync()
                .ReceiveJson<IEnumerable<PermissionAssociatedPolicy>>()
                .ConfigureAwait(false);

            return response ?? Enumerable.Empty<PermissionAssociatedPolicy>();
        }

        #endregion

        #region Policies

        public async Task<string> CreatePolicyAsync(string realm, string clientId, Policy policy)
        {
            string policyType = policy.Type.ToString().ToLower();
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy/{policyType}")
                .PostJsonAsync(policy)
                .ReceiveJson<Policy>()
                .ConfigureAwait(false);

            return response.Id;
        }

        public async Task<Policy> GetPolicyAsync(string realm, string clientId, string policyId)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy/{policyId}")
                .ConfigureRequest(settings =>
                {
                    settings.JsonSerializer = new NewtonsoftJsonSerializer(new JsonSerializerSettings
                    {
                        Converters = new JsonConverter[] { new PolicyWithConfigConverter() }
                    });
                })
                .GetAsync()
                .ReceiveJson<Policy>()
                .ConfigureAwait(false);

            return response;
        }

        public async Task<IEnumerable<Policy>> GetPoliciesAsync(string realm, string clientId, int first = 0, int max = 20,
            PolicyType? type = null, string name = null, string resource = null, string scope = null)
        {
            string policyType = type.HasValue
                ? $"{type.ToString().ToLower()}"
                : string.Empty;
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy{policyType}")
                .SetQueryParam("first", first)
                .SetQueryParam("max", max)
                .SetQueryParam("name", name)
                .SetQueryParam("permission", false)
                .SetQueryParam("resource", resource)
                .SetQueryParam("scope", scope)
                .ConfigureRequest(settings =>
                {
                    settings.JsonSerializer = new NewtonsoftJsonSerializer(new JsonSerializerSettings
                    {
                        Converters = new JsonConverter[] { new PolicyWithConfigConverter() }
                    });
                })
                .GetAsync()
                .ReceiveJson<IEnumerable<Policy>>()
                .ConfigureAwait(false);

            return response ?? Enumerable.Empty<Policy>();
        }

        public async Task<bool> UpdatePolicyAsync(string realm, string clientId, string policyId, Policy policy)
        {
            string policyType = policy.Type.ToString().ToLower();
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy/{policyType}/{policyId}")
                .PutJsonAsync(policy)
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePolicyAsync(string realm, string clientId, string policyId)
        {
            var response = await GetBaseUrl(realm)
                .AppendPathSegment($"/admin/realms/{realm}/clients/{clientId}/authz/resource-server/policy/{policyId}")
                .DeleteAsync()
                .ConfigureAwait(false);

            return response.IsSuccessStatusCode;
        }

        #endregion

        #region Permissions Token

        public async Task<IEnumerable<PermissionsTokenResource>> GetPermissionsTokenAsync(string realm, string client, string accessToken, string[] permissions = null)
        {
            var data = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "urn:ietf:params:oauth:grant-type:uma-ticket"),
                new KeyValuePair<string, string>("response_mode", "permissions"),
                new KeyValuePair<string, string>("audience", client)
            };

            var permissionsToAdd = (permissions ?? Enumerable.Empty<string>())
                .Select(permission => new KeyValuePair<string, string>("permission", permission));

            data.AddRange(permissionsToAdd);

            try
            {
                var response = await GetBaseUrl(realm)
                    .AppendPathSegment($"/realms/{realm}/protocol/openid-connect/token")
                    .WithOAuthBearerToken(accessToken)
                    .PostUrlEncodedAsync(data)
                    .ReceiveJson<IEnumerable<PermissionsTokenResource>>()
                    .ConfigureAwait(false);

                return response;
            }
            catch (FlurlHttpException ex) when (ex.Call.HttpStatus == HttpStatusCode.Forbidden)
            {
                return Enumerable.Empty<PermissionsTokenResource>();
            }
        }

        public async Task<bool> GetPermissionsDecisionAsync(string realm, string client, string accessToken, string[] permissions = null)
        {
            var data = new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("grant_type", "urn:ietf:params:oauth:grant-type:uma-ticket"),
                new KeyValuePair<string, object>("response_mode", "decision"),
                new KeyValuePair<string, object>("audience", client)
            };

            data.Add(new KeyValuePair<string, object>("permission", permissions));

            bool decision;

            try
            {
                var response = await GetBaseUrl(realm)
                    .AppendPathSegment($"/realms/{realm}/protocol/openid-connect/token")
                    .WithOAuthBearerToken(accessToken)
                    .PostUrlEncodedAsync(data)
                    .ReceiveJson<PermissionsTokenDecision>()
                    .ConfigureAwait(false);

                decision = response.Result;
            }
            catch (FlurlHttpException ex) when (ex.Call.HttpStatus == HttpStatusCode.Forbidden)
            {
                decision = false;
            }

            return decision;
        }

        #endregion
    }
}
