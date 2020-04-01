using Keycloak.Net.Common.Converters;
using Newtonsoft.Json;

namespace Keycloak.Net.Models.Authorization
{
    [JsonConverter(typeof(DecisionStrategyConverter))]
    public enum DecisionStrategy
    {
        Affirmative, 
        Unanimous,
        Consensus
    }
}