using Keycloak.Net.Common.Converters;
using Newtonsoft.Json;

namespace Keycloak.Net.Models.Authorization
{
    [JsonConverter(typeof(PermissionDecisionStrategyConverter))]
    public enum PermissionDecisionStrategy
    {
        Affirmative, 
        Unanimous,
        Consensus
    }
}