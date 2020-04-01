using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Keycloak.Net.Models.Authorization
{
    public class Policy
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type")]
        public PolicyType Type { get; set; }

        [JsonProperty("logic")]
        public Logic Logic { get; set; }

        [JsonProperty("decisionStrategy")]
        public DecisionStrategy DecisionStrategy { get; set; }

        #region Time Policy Properties

        [JsonProperty("notBefore")]
        public DateTime NotBefore { get; set; }

        [JsonProperty("notOnOrAfter")]
        public DateTime NotOnOrAfter { get; set; }

        [JsonProperty("dayMonth")]
        public int DayMonth { get; set; }

        [JsonProperty("dayMonthEnd")]
        public int DayMonthEnd { get; set; }

        [JsonProperty("month")]
        public int Month { get; set; }

        [JsonProperty("monthEnd")]
        public int MonthEnd { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("yearEnd")]
        public int YearEnd { get; set; }

        [JsonProperty("hour")]
        public int Hour { get; set; }

        [JsonProperty("hourEnd")]
        public int HourEnd { get; set; }

        [JsonProperty("minute")]
        public int Minute { get; set; }

        [JsonProperty("minuteEnd")]
        public int MinuteEnd { get; set; }

        #endregion
        
        [JsonProperty("roles")]
        public IEnumerable<PolicyRole> Roles { get; set; } = Enumerable.Empty<PolicyRole>();

        [JsonProperty("clients")]
        public IEnumerable<string> Clients { get; set; } = Enumerable.Empty<string>();

        [JsonProperty("users")]
        public IEnumerable<string> Users { get; set; } = Enumerable.Empty<string>();

        [JsonProperty("groups")]
        public IEnumerable<PolicyGroup> Groups { get; set; } = Enumerable.Empty<PolicyGroup>();

        [JsonProperty("policies")]
        public IEnumerable<string> Policies { get; set; } = Enumerable.Empty<string>();
    }
}
