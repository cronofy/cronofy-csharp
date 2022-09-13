namespace Cronofy.Responses
{
    using System.Collections;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    internal sealed class ListAvailabilityRulesResponse
    {
        [JsonProperty("availability_rules")]
        public IEnumerable<AvailabilityRule> AvailabilityRules { get; set; }

        internal sealed class AvailabilityRuleResponse
        {
            [JsonProperty("availability_rule_id")]
            public string AvailabilityRuleId { get; set; }

            [JsonProperty("tzid")]
            public string TimeZoneId { get; set; }

            [JsonProperty("calendar_ids")]
            public IEnumerable<string> CalendarIds { get; set; }
            
            [JsonProperty("weekly_periods")]
            public IEnumerable<WeeklyPeriod> WeeklyPeriods { get; set; }

            internal sealed class WeeklyPeriod
            {
                [JsonProperty("day")]
                public string Day { get; set; } // could be an enum

                [JsonProperty("start_time")]
                public string StartTime { get; set; }

                [JsonProperty("end_time")]
                public string EndTime { get; set; }
            }

            // public AvailabilityRule ToAvailabilityRule() {}
        }       
    }
}
