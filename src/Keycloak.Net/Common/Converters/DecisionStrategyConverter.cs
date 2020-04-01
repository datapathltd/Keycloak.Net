using System;
using System.Collections.Generic;
using System.Linq;
using Keycloak.Net.Models.Authorization;

namespace Keycloak.Net.Common.Converters
{
    public class DecisionStrategyConverter : JsonEnumConverter<DecisionStrategy>
    {
        private static readonly Dictionary<DecisionStrategy, string> s_pairs = new Dictionary<DecisionStrategy, string>
        {
            [DecisionStrategy.Affirmative] = "AFFIRMATIVE",
            [DecisionStrategy.Consensus] = "CONSENSUS",
            [DecisionStrategy.Unanimous] = "UNANIMOUS"
        };

        protected override string EntityString { get; } = nameof(DecisionStrategy).ToLower();

        protected override string ConvertToString(DecisionStrategy value) => s_pairs[value];

        protected override DecisionStrategy ConvertFromString(string s)
        {
            var pair = s_pairs.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<DecisionStrategy, string>>.Default.Equals(pair))
            {
                throw new ArgumentException($"Unknown {EntityString}: {s}");
            }

            return pair.Key;
        }
    }
}
