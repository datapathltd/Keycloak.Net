using System;
using System.Collections.Generic;
using System.Linq;
using Keycloak.Net.Models.Authorization;

namespace Keycloak.Net.Common.Converters
{
    public class PolicyTypeConverter : JsonEnumConverter<PolicyType>
    {
        private static readonly Dictionary<PolicyType, string> s_pairs = new Dictionary<PolicyType, string>
        {
            [PolicyType.Aggregated] = "aggregated",
            [PolicyType.Client] = "client",
            [PolicyType.Group] = "group",
            [PolicyType.Role] = "role",
            [PolicyType.Time] = "time",
            [PolicyType.User] = "user"
        };

        protected override string EntityString { get; } = nameof(PolicyType).ToLower();

        protected override string ConvertToString(PolicyType value) => s_pairs[value];

        protected override PolicyType ConvertFromString(string s)
        {
            var pair = s_pairs.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<PolicyType, string>>.Default.Equals(pair))
            {
                throw new ArgumentException($"Unknown {EntityString}: {s}");
            }

            return pair.Key;
        }
    }
}
