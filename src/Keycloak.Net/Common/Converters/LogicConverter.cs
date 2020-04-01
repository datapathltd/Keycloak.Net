using System;
using System.Collections.Generic;
using System.Linq;
using Keycloak.Net.Models.Authorization;

namespace Keycloak.Net.Common.Converters
{
    public class LogicConverter : JsonEnumConverter<Logic>
    {
        private static readonly Dictionary<Logic, string> s_pairs = new Dictionary<Logic, string>
        {
            [Logic.Negative] = "NEGATIVE",
            [Logic.Positive] = "POSITIVE"
        };

        protected override string EntityString { get; } = nameof(Logic).ToLower();

        protected override string ConvertToString(Logic value) => s_pairs[value];

        protected override Logic ConvertFromString(string s)
        {
            var pair = s_pairs.FirstOrDefault(kvp => kvp.Value.Equals(s, StringComparison.OrdinalIgnoreCase));
            // ReSharper disable once SuspiciousTypeConversion.Global
            if (EqualityComparer<KeyValuePair<Logic, string>>.Default.Equals(pair))
            {
                throw new ArgumentException($"Unknown {EntityString}: {s}");
            }

            return pair.Key;
        }
    }
}
