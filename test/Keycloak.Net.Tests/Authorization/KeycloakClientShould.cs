using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keycloak.Net.Models.Authorization;
using Xunit;

namespace Keycloak.Net.Tests
{
    public partial class KeycloakClientShould
    {
        [Theory]
        [InlineData("Insurance", "insurance")]
        public async Task CreateResourceAsync(string realm, string clientId)
        {
            var resource = new Resource()
            {
                Attributes = new Dictionary<string, object>()
                {
                    { "attribute-1", "value1" },
                    { "attribute-2", "value2" }
                },
                DisplayName = "Create Resource",
                Name = $"create-resource-{Guid.NewGuid()}",
                IconUri = "http://icon-uri",
                OwnerManagedAccess = true,
                Scopes = new []{ new Scope
                {
                    Name = "test"
                }},
                Type = "test-resource",
                Uris = new [] { "http://uri" }
            };

            var clients = await _client.GetClientsAsync(realm);
            string clientsId = clients.FirstOrDefault(x => x.ClientId == clientId)?.Id;
            if (clientsId != null)
            {
                var resourceId = await _client.CreateResourceAsync(realm, clientsId, resource);
                Assert.NotNull(resourceId);

                var createdResource = await _client.GetResourceAsync(realm, clientsId, resourceId);
                Assert.NotNull(createdResource);
            }
        }

        [Theory]
        [InlineData("Insurance", "insurance")]
        public async Task UpdateResourceAsync(string realm, string clientId)
        {
            var resource = new Resource()
            {
                Attributes = new Dictionary<string, object>()
                {
                    { "attribute-1", "value1" },
                    { "attribute-2", "value2" }
                },
                DisplayName = "Update Resource",
                Name = "update-resource",
                IconUri = "http://icon-uri",
                OwnerManagedAccess = true,
                Scopes = new[]{ new Scope
                {
                    Name = "test"
                }},
                Type = "test-resource",
                Uris = new[] { "http://uri" }
            };

            var clients = await _client.GetClientsAsync(realm);
            string clientsId = clients.FirstOrDefault(x => x.ClientId == clientId)?.Id;
            if (clientsId != null)
            {
                var resourceId = await _client.CreateResourceAsync(realm, clientsId, resource);
                
                resource.Attributes.Add("attribute-3", "value3");
                resource.DisplayName = "Updated Resource";
                resource.Name = "updated-resource";

                var response = await _client.UpdateResourceAsync(realm, clientsId, resourceId, resource);
                Assert.True(response);
            }
        }

        [Theory]
        [InlineData("Insurance", "insurance")]
        public async Task DeleteResourceAsync(string realm, string clientId)
        {
            var resource = new Resource()
            {
                Attributes = new Dictionary<string, object>()
                {
                    { "attribute-1", "value1" },
                    { "attribute-2", "value2" }
                },
                DisplayName = "Delete Resource",
                Name = "delete-resource",
                IconUri = "http://icon-uri",
                OwnerManagedAccess = true,
                Scopes = new[]{ new Scope
                {
                    Name = "test"
                }},
                Type = "test-resource",
                Uris = new[] { "http://uri" }
            };

            var clients = await _client.GetClientsAsync(realm);
            string clientsId = clients.FirstOrDefault(x => x.ClientId == clientId)?.Id;
            if (clientsId != null)
            {
                var resourceId = await _client.CreateResourceAsync(realm, clientsId, resource);
                Assert.NotNull(resourceId);

                var response = await _client.DeleteResourceAsync(realm, clientsId, resourceId);
                Assert.True(response);
            }
        }

        [Theory]
        [InlineData("Insurance", "insurance")]
        public async Task GetResourcesAsync(string realm, string clientId)
        {
            var clients = await _client.GetClientsAsync(realm);
            string clientsId = clients.FirstOrDefault(x => x.ClientId == clientId)?.Id;
            if (clientsId != null)
            {
                var resources = await _client.GetResourcesAsync(realm, clientsId);
                Assert.True(resources.Any());
            }
        }

        [Theory]
        [InlineData("Insurance", "insurance")]
        public async Task CreatePermissionAsync(string realm, string clientId)
        {
            var resourceId = Guid.NewGuid().ToString();
            var resource = new Resource()
            {
                DisplayName = "Create Permission Resource",
                Id = resourceId,
                Name = $"create-permission-resource-{resourceId}"
            };

            var permission = new Permission
            {
                DecisionStrategy = PermissionDecisionStrategy.Consensus,
                Description = "Create Permission",
                Logic = PermissionLogic.Positive,
                Name = $"create-permission-{Guid.NewGuid()}",
                Policies = Enumerable.Empty<string>(),
                Resources = new[] { resource.Id },
                Scopes = Enumerable.Empty<string>(),
                Type = PermissionType.Resource
            };

            var clients = await _client.GetClientsAsync(realm);
            string clientsId = clients.FirstOrDefault(x => x.ClientId == clientId)?.Id;
            if (clientsId != null)
            {
                resourceId = await _client.CreateResourceAsync(realm, clientsId, resource);
                Assert.NotNull(resourceId);

                var permissionId = await _client.CreatePermissionAsync(realm, clientsId, permission);
                Assert.NotNull(permissionId);

                var createdPermission = await _client.GetPermissionAsync(realm, clientsId, permissionId, permission.Type);
                Assert.NotNull(createdPermission);
            }
        }

        [Theory]
        [InlineData("Insurance", "insurance")]
        public async Task UpdatePermissionAsync(string realm, string clientId)
        {
            var resourceId = Guid.NewGuid().ToString();
            var resource = new Resource()
            {
                DisplayName = "Update Permission Resource",
                Id = resourceId,
                Name = $"update-permission-resource-{resourceId}"
            };

            var permission = new Permission
            {
                DecisionStrategy = PermissionDecisionStrategy.Consensus,
                Description = "Update Permission",
                Logic = PermissionLogic.Positive,
                Name = $"update-permission-{Guid.NewGuid()}",
                Policies = Enumerable.Empty<string>(),
                Resources = new[] { resource.Id },
                Scopes = Enumerable.Empty<string>(),
                Type = PermissionType.Resource
            };

            var clients = await _client.GetClientsAsync(realm);
            string clientsId = clients.FirstOrDefault(x => x.ClientId == clientId)?.Id;
            if (clientsId != null)
            {
                resourceId = await _client.CreateResourceAsync(realm, clientsId, resource);
                Assert.NotNull(resourceId);

                var permissionId = await _client.CreatePermissionAsync(realm, clientsId, permission);
                Assert.NotNull(permissionId);

                permission.Description = "Updated Permission";
                permission.DecisionStrategy = PermissionDecisionStrategy.Unanimous;
                permission.Logic = PermissionLogic.Negative;

                var response = await _client.UpdatePermissionAsync(realm, clientsId, permissionId, permission);
                Assert.True(response);
            }
        }

        [Theory]
        [InlineData("Insurance", "insurance")]
        public async Task DeletePermissionAsync(string realm, string clientId)
        {
            var resourceId = Guid.NewGuid().ToString();
            var resource = new Resource()
            {
                DisplayName = "Delete Permission Resource",
                Id = resourceId,
                Name = $"delete-permission-resource-{resourceId}"
            };

            var permission = new Permission
            {
                DecisionStrategy = PermissionDecisionStrategy.Consensus,
                Description = "Delete Permission",
                Logic = PermissionLogic.Positive,
                Name = $"delete-permission-{Guid.NewGuid()}",
                Policies = Enumerable.Empty<string>(),
                Resources = new[] { resource.Id },
                Scopes = Enumerable.Empty<string>(),
                Type = PermissionType.Resource
            };

            var clients = await _client.GetClientsAsync(realm);
            string clientsId = clients.FirstOrDefault(x => x.ClientId == clientId)?.Id;
            if (clientsId != null)
            {
                resourceId = await _client.CreateResourceAsync(realm, clientsId, resource);
                Assert.NotNull(resourceId);

                var permissionId = await _client.CreatePermissionAsync(realm, clientsId, permission);
                Assert.NotNull(permissionId);

                var response = await _client.DeletePermissionAsync(realm, clientsId, permissionId, permission.Type);
                Assert.True(response);
            }
        }

        [Theory]
        [InlineData("Insurance", "insurance")]
        public async Task GetPermissionsAsync(string realm, string clientId)
        {
            var clients = await _client.GetClientsAsync(realm);
            string clientsId = clients.FirstOrDefault(x => x.ClientId == clientId)?.Id;
            if (clientsId != null)
            {
                var permissions = await _client.GetPermissionsAsync(realm, clientsId);
                Assert.True(permissions.Any());
            }
        }

        [Theory]
        [InlineData("Insurance", "insurance")]
        public async Task GetPermissionResourcesAsync(string realm, string clientId)
        {
            var resourceId = Guid.NewGuid().ToString();
            var resource = new Resource()
            {
                DisplayName = "Get Permission Resources",
                Id = resourceId,
                Name = $"get-permission-resources-{resourceId}"
            };

            var permission = new Permission
            {
                DecisionStrategy = PermissionDecisionStrategy.Consensus,
                Description = "Get Permission Resources",
                Logic = PermissionLogic.Positive,
                Name = $"get-permission-resources-{Guid.NewGuid()}",
                Policies = Enumerable.Empty<string>(),
                Resources = new[] { resource.Id },
                Scopes = Enumerable.Empty<string>(),
                Type = PermissionType.Resource
            };

            var clients = await _client.GetClientsAsync(realm);
            string clientsId = clients.FirstOrDefault(x => x.ClientId == clientId)?.Id;
            if (clientsId != null)
            {
                resourceId = await _client.CreateResourceAsync(realm, clientsId, resource);
                Assert.NotNull(resourceId);

                var permissionId = await _client.CreatePermissionAsync(realm, clientsId, permission);
                Assert.NotNull(permissionId);

                var permissionResources = await _client.GetPermissionResourcesAsync(realm, clientsId, permissionId);
                Assert.NotNull(permissionResources.SingleOrDefault());
            }
        }
    }
}
