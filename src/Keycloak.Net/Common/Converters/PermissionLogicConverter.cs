using System;
using System.Collections.Generic;
using System.Linq;
using Keycloak.Net.Models.Authorization;

namespace Keycloak.Net.Common.Converters
{
    public class PermissionLogicConverter : JsonEnumConverter<PermissionLogic>
    {
        private static readonly Dictionary<PermissionLogic, string> s_pairs = new Dictionary<PermissionLogic, string>
        {
            [PermissionLogic.Negative] = "NEGATIVE",
            [PermissionLogic.Positive] = "POSITIVE"
        };

        protected override string EntityString { get; } = nameof(PermissionLogic).ToLower();

        protected override string ConvertToString(PermissionLogic value) => s_pairs[value];

        protected override PermissionLogic ConvertFromString(string s)
        {
            var pair = s_pairs.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<PermissionLogic, string>>.Default.Equals(pair))
            {
                throw new ArgumentException($"Unknown {EntityString}: {s}");
            }

            return pair.Key;
        }
    }
}
