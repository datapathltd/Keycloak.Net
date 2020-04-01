using Keycloak.Net.Common.Converters;
using Newtonsoft.Json;

namespace Keycloak.Net.Models.Authorization
{
    [JsonConverter(typeof(LogicConverter))]
    public enum Logic
    {
        Positive,
        Negative
    }
}