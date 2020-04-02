using System;
using System.Collections.Generic;
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

        [JsonProperty("notBefore")]
        public DateTime? NotBefore { get; set; }

        [JsonProperty("notOnOrAfter")]
        public DateTime? NotOnOrAfter { get; set; }

        [JsonProperty("dayMonth")]
        public int? DayMonth { get; set; }

        [JsonProperty("dayMonthEnd")]
        public int? DayMonthEnd { get; set; }

        [JsonProperty("month")]
        public int? Month { get; set; }

        [JsonProperty("monthEnd")]
        public int? MonthEnd { get; set; }

        [JsonProperty("year")]
        public int? Year { get; set; }

        [JsonProperty("yearEnd")]
        public int? YearEnd { get; set; }

        [JsonProperty("hour")]
        public int? Hour { get; set; }

        [JsonProperty("hourEnd")]
        public int? HourEnd { get; set; }

        [JsonProperty("minute")]
        public int? Minute { get; set; }

        [JsonProperty("minuteEnd")]
        public int? MinuteEnd { get; set; }

        [JsonProperty("roles")]
        public IEnumerable<PolicyRole> Roles { get; set; }

        [JsonProperty("clients")]
        public IEnumerable<string> Clients { get; set; }

        [JsonProperty("users")]
        public IEnumerable<string> Users { get; set; }

        [JsonProperty("groups")]
        public IEnumerable<PolicyGroup> Groups { get; set; }

        [JsonProperty("policies")]
        public IEnumerable<string> Policies { get; set; }

        #region ConfigProperties

        [JsonProperty("config.notBefore")]
        private DateTime? ConfigNotBefore { set => NotBefore = value; }

        [JsonProperty("config.notOnOrAfter")]
        private DateTime? ConfigNotOnOrAfter { set => NotOnOrAfter = value; }

        [JsonProperty("config.dayMonth")]
        private int? ConfigDayMonth { set => DayMonth = value; }

        [JsonProperty("config.dayMonthEnd")]
        private int? ConfigDayMonthEnd { set => DayMonthEnd = value; }

        [JsonProperty("config.month")]
        private int? ConfigMonth { set => Month = value; }

        [JsonProperty("config.monthEnd")]
        private int? ConfigMonthEnd { set => MonthEnd = value; }

        [JsonProperty("config.year")]
        private int? ConfigYear { set => Year = value; }

        [JsonProperty("config.yearEnd")]
        private int? ConfigYearEnd { set => YearEnd = value; }

        [JsonProperty("config.hour")]
        private int? ConfigHour { set => Hour = value; }

        [JsonProperty("config.hourEnd")]
        private int? ConfigHourEnd { set => HourEnd = value; }

        [JsonProperty("config.minute")]
        private int? ConfigMinute { set => Minute = value; }

        [JsonProperty("config.minuteEnd")]
        private int? ConfigMinuteEnd { set => MinuteEnd = value; }

        [JsonProperty("config.roles")]
        private string ConfigRoles
        {
            set => Roles = JsonConvert.DeserializeObject<IEnumerable<PolicyRole>>(value);
        }

        [JsonProperty("config.clients")]
        private string ConfigClients
        {
            set => Clients = JsonConvert.DeserializeObject<IEnumerable<string>>(value);
        }

        [JsonProperty("config.users")]
        private string ConfigUsers
        {
            set => Users = JsonConvert.DeserializeObject<IEnumerable<string>>(value);
        }

        [JsonProperty("config.groups")]
        private string ConfigGroups
        {
            set => Groups = JsonConvert.DeserializeObject<IEnumerable<PolicyGroup>>(value);
        }

        [JsonProperty("config.policies")]
        private string ConfigPolicies
        {
            set => Policies = JsonConvert.DeserializeObject<IEnumerable<string>>(value);
        }

        #endregion
    }
}
