using Keycloak.Net.Common.Converters;
using Newtonsoft.Json;

namespace Keycloak.Net.Models.Authorization
{
    [JsonConverter(typeof(PermissionLogicConverter))]
    public enum PermissionLogic
    {
        Positive,
        Negative
    }
}