using System;
using System.Collections.Generic;
using System.Linq;
using Keycloak.Net.Models.Authorization;

namespace Keycloak.Net.Common.Converters
{
    public class PermissionTypeConverter : JsonEnumConverter<PermissionType>
    {
        private static readonly Dictionary<PermissionType, string> s_pairs = new Dictionary<PermissionType, string>
        {
            [PermissionType.Resource] = "resource",
            [PermissionType.Scope] = "scope"
        };

        protected override string EntityString { get; } = nameof(PermissionType).ToLower();

        protected override string ConvertToString(PermissionType value) => s_pairs[value];

        protected override PermissionType ConvertFromString(string s)
        {
            var pair = s_pairs.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<PermissionType, string>>.Default.Equals(pair))
            {
                throw new ArgumentException($"Unknown {EntityString}: {s}");
            }

            return pair.Key;
        }
    }
}
