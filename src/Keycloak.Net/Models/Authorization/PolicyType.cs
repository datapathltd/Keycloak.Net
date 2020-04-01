using Keycloak.Net.Common.Converters;
using Newtonsoft.Json;

namespace Keycloak.Net.Models.Authorization
{
    [JsonConverter(typeof(PolicyTypeConverter))]
    public enum PolicyType
    {
        Aggregated,
        Client,
        Group,
        Role,
        Time,
        User
    }
}