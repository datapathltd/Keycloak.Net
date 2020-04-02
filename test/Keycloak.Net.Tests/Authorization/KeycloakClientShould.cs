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
                DecisionStrategy = DecisionStrategy.Consensus,
                Description = "Create Permission",
                Logic = Logic.Positive,
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
                DecisionStrategy = DecisionStrategy.Consensus,
                Description = "Update Permission",
                Logic = Logic.Positive,
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
                permission.DecisionStrategy = DecisionStrategy.Unanimous;
                permission.Logic = Logic.Negative;

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
                DecisionStrategy = DecisionStrategy.Consensus,
                Description = "Delete Permission",
                Logic = Logic.Positive,
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
                DecisionStrategy = DecisionStrategy.Consensus,
                Description = "Get Permission Resources",
                Logic = Logic.Positive,
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

        [Theory]
        [InlineData("Insurance", "insurance")]
        public async Task CreateUpdateAndDeleteUserPolicyAsync(string realm, string clientId)
        {
            var guid = Guid.NewGuid();
            var policy = new Policy
            {
                Description = "Create User Policy",
                Logic = Logic.Positive,
                Name = $"create-user-policy-{guid}",
                Type = PolicyType.User,
                Users = new[] { "56afa4ab-3384-403b-a38d-c93a1c58aaf5" }
            };

            var clients = await _client.GetClientsAsync(realm);
            string clientsId = clients.FirstOrDefault(x => x.ClientId == clientId)?.Id;
            if (clientsId != null)
            {
                var policyId = await _client.CreatePolicyAsync(realm, clientsId, policy);
                Assert.NotNull(policyId);

                var createdPolicy = await _client.GetPolicyAsync(realm, clientsId, policyId);
                Assert.NotNull(createdPolicy);
                Assert.Equal(policy.Users.Count(), createdPolicy.Users.Count());

                createdPolicy.Name = $"update-user-policy-{guid}";
                createdPolicy.Description = "Update User Policy";

                var updateResponse = await _client.UpdatePolicyAsync(realm, clientsId, policyId, createdPolicy);
                Assert.True(updateResponse);

                var deleteResponse = await _client.DeletePolicyAsync(realm, clientsId, policyId);
                Assert.True(deleteResponse);
            }
        }

        [Theory]
        [InlineData("Insurance", "insurance")]
        public async Task CreateUpdateAndDeleteGroupPolicyAsync(string realm, string clientId)
        {
            var guid = Guid.NewGuid();
            var policy = new Policy
            {
                Description = "Create Group Policy",
                Logic = Logic.Positive,
                Name = $"create-group-policy-{guid}",
                Type = PolicyType.Group,
                Groups = new[] { 
                    new PolicyGroup
                    {
                        Id = "b3172a83-e7da-4d86-ae59-57aba035d431",
                        ExtendChildren = true
                    }
                }
            };

            var clients = await _client.GetClientsAsync(realm);
            string clientsId = clients.FirstOrDefault(x => x.ClientId == clientId)?.Id;
            if (clientsId != null)
            {
                var policyId = await _client.CreatePolicyAsync(realm, clientsId, policy);
                Assert.NotNull(policyId);

                var createdPolicy = await _client.GetPolicyAsync(realm, clientsId, policyId);
                Assert.NotNull(createdPolicy);
                Assert.Equal(policy.Groups.Count(), createdPolicy.Groups.Count());

                createdPolicy.Name = $"update-group-policy-{guid}";
                createdPolicy.Description = "Update Group Policy";

                var updateResponse = await _client.UpdatePolicyAsync(realm, clientsId, policyId, createdPolicy);
                Assert.True(updateResponse);

                var deleteResponse = await _client.DeletePolicyAsync(realm, clientsId, policyId);
                Assert.True(deleteResponse);
            }
        }

        [Theory]
        [InlineData("Insurance", "insurance")]
        public async Task CreateUpdateAndDeleteRolePolicyAsync(string realm, string clientId)
        {
            var guid = Guid.NewGuid();
            var policy = new Policy
            {
                Description = "Create Role Policy",
                Logic = Logic.Positive,
                Name = $"create-role-policy-{guid}",
                Type = PolicyType.Role,
                Roles = new[] {
                    new PolicyRole
                    {
                        Id = "5dc7655b-5a30-42c4-a341-929e88cb389d",
                        Required = true
                    }
                }
            };

            var clients = await _client.GetClientsAsync(realm);
            string clientsId = clients.FirstOrDefault(x => x.ClientId == clientId)?.Id;
            if (clientsId != null)
            {
                var policyId = await _client.CreatePolicyAsync(realm, clientsId, policy);
                Assert.NotNull(policyId);

                var createdPolicy = await _client.GetPolicyAsync(realm, clientsId, policyId);
                Assert.NotNull(createdPolicy);
                Assert.Equal(policy.Roles.Count(), createdPolicy.Roles.Count());

                createdPolicy.Name = $"update-role-policy-{guid}";
                createdPolicy.Description = "Update Role Policy";

                var updateResponse = await _client.UpdatePolicyAsync(realm, clientsId, policyId, createdPolicy);
                Assert.True(updateResponse);

                var deleteResponse = await _client.DeletePolicyAsync(realm, clientsId, policyId);
                Assert.True(deleteResponse);
            }
        }

        [Theory]
        [InlineData("Insurance", "insurance")]
        public async Task GetPoliciesAsync(string realm, string clientId)
        {
            var clients = await _client.GetClientsAsync(realm);
            string clientsId = clients.FirstOrDefault(x => x.ClientId == clientId)?.Id;
            if (clientsId != null)
            {
                var policies = await _client.GetPoliciesAsync(realm, clientsId);
                Assert.True(policies.Any());
            }
        }
    }
}
