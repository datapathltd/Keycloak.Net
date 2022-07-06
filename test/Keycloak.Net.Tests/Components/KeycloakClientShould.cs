using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Keycloak.Net.Models.Components;
using Xunit;

namespace Keycloak.Net.Tests
{
    public partial class KeycloakClientShould
    {
        [Theory]
        [InlineData("networkmanager")]
        public async Task GetComponentsAsync(string realm)
        {
            var result = await _client.GetComponentsAsync("master", realm);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Insurance")]
        public async Task GetComponentAsync(string realm)
        {
            var components = await _client.GetComponentsAsync("master", realm);
            string componentId = components.FirstOrDefault()?.Id;
            if (componentId != null)
            {
                var result = await _client.GetComponentAsync(realm, componentId);
                Assert.NotNull(result);
            }
        }

        [Theory(Skip = "Modifies data")]
        [InlineData("Insurance")]
        public async Task CreateUserStorageProviderComponent(string realm)
        {
            var testInputComponent = new Component
            {

                Name = "Test LDAP Component",
                ParentId = "242f4e18-4ba4-40f2-b152-1b2a2231c187",
                ProviderId = "ldap",
                ProviderType = "org.keycloak.storage.UserStorageProvider",
                Config = new Dictionary<string, string[]>
                {
                    { "enabled", new [] { "true" } },
                    { "priority", new [] { "0" } },
                    { "importEnabled", new [] { "true" } },
                    { "editMode", new [] { "READ_ONLY" } },
                    { "syncRegistrations", new [] { "false" } },
                    { "vendor", new [] { "ad" } },
                    { "usernameLDAPAttribute", new [] { "sAMAccountName" } },
                    { "uuidLDAPAttribute", new [] { "objectGUID" } },
                    { "userObjectClasses", new [] { "person, organizationalPerson, user" } },
                    { "connectionUrl", new [] { "ldap://dc.corporate.local" } },
                    { "usersDn", new [] { "OU=users,DC=corporate,DC=local" } },
                    { "searchScope", new [] { "2" } },
                    { "bindType", new [] { "simple" } },
                    { "bindDn", new [] { "CN=keycloak,CN=Users,DC=corporate, DC=local" } },
                    { "bindCredential", new [] { "testpassword" } },
                    { "pagination", new [] { "true" } },
                    { "connectionPooling", new [] { "true" } },
                    { "allowKerberosAuthentication", new [] { "true" } },
                    { "kerberosRealm", new [] { "CORPORATE.LOCAL" } },
                    { "serverPrincipal", new [] { "HTTP/server.corporate.local@CORPORATE.LOCAL" } },
                    { "keyTab", new [] { "/etc/keycloak.keytab" } },
                    { "useKerberosForPasswordAuthentication", new [] { "true" } },
                    { "fullSyncPeriod", new [] { "865400" } },
                    { "changedSyncPeriod", new [] { "84000" } },
                    { "batchSizeForSync", new [] { "84000" } }
                }
            };

            var result = await _client.CreateComponentAsync(realm, testInputComponent);

            Assert.NotEmpty(result);
        }

        [Theory(Skip = "Modifies data")]
        [InlineData("Insurance")]
        public async Task DeleteUserStorageProviderComponent(string realm)
        {
            var result = await _client.DeleteComponentAsync(realm, "72354592-23f8-487c-b345-6e749cfb9b61");
            Assert.True(result);
        }
    }
}