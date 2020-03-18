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
                Name = "create-resource",
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
                
                resource = new Resource()
                {
                    Attributes = new Dictionary<string, object>()
                    {
                        { "attribute-1", "value1" },
                        { "attribute-2", "value2" },
                        { "attribute-3", "value3" }
                    },
                    DisplayName = "Updated Resource",
                    Name = "updated-resource",
                    IconUri = "http://icon-uri",
                    OwnerManagedAccess = true,
                    Scopes = new[]{ new Scope
                    {
                        Name = "test"
                    }},
                    Type = "test-resource",
                    Uris = new[] { "http://uri" }
                };

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
    }
}
