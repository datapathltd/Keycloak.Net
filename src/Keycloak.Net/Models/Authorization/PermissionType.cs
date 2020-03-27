using Keycloak.Net.Common.Converters;
using Newtonsoft.Json;

namespace Keycloak.Net.Models.Authorization
{
    [JsonConverter(typeof(PermissionTypeConverter))]
    public enum PermissionType
    {
        Resource,
        Scope
    }
}