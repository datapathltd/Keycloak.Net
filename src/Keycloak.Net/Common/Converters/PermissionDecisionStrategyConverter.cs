using System;
using System.Collections.Generic;
using System.Linq;
using Keycloak.Net.Models.Authorization;

namespace Keycloak.Net.Common.Converters
{
    public class PermissionDecisionStrategyConverter : JsonEnumConverter<PermissionDecisionStrategy>
    {
        private static readonly Dictionary<PermissionDecisionStrategy, string> s_pairs = new Dictionary<PermissionDecisionStrategy, string>
        {
            [PermissionDecisionStrategy.Affirmative] = "AFFIRMATIVE",
            [PermissionDecisionStrategy.Consensus] = "CONSENSUS",
            [PermissionDecisionStrategy.Unanimous] = "UNANIMOUS"
        };

        protected override string EntityString { get; } = nameof(PermissionDecisionStrategy).ToLower();

        protected override string ConvertToString(PermissionDecisionStrategy value) => s_pairs[value];

        protected override PermissionDecisionStrategy ConvertFromString(string s)
        {
            var pair = s_pairs.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<PermissionDecisionStrategy, string>>.Default.Equals(pair))
            {
                throw new ArgumentException($"Unknown {EntityString}: {s}");
            }

            return pair.Key;
        }
    }
}
