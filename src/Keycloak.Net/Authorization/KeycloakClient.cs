﻿using System.Collections.Generic;
using System.Linq;
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
    }
}
