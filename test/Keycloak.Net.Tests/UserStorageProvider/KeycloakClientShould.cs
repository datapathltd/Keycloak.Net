using System.Threading.Tasks;
using Xunit;

namespace Keycloak.Net.Tests
{
    public partial class KeycloakClientShould
    {
        [Theory]
        [InlineData("Insurance")]
        public async Task TriggerUserSynchronizationAsync(string realm)
        {
            string storageProviderId = "91249a33-172a-49c5-8e1c-8fca79497274";
            var result = await _client.TriggerUserSynchronizationAsync(realm, storageProviderId, UserSyncActions.Full);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Insurance")]
        public async Task TriggerLdapMapperSynchronizationAsync(string realm)
        {
            string storageProviderId = "91249a33-172a-49c5-8e1c-8fca79497274";
            string mapperId = "29feabf2-e7cd-449d-ad96-a0eb68080850";
            var result = await _client.TriggerLdapMapperSynchronizationAsync(realm, storageProviderId, mapperId, LdapMapperSyncActions.FedToKeycloak);
            Assert.NotNull(result);
        }
    }
}