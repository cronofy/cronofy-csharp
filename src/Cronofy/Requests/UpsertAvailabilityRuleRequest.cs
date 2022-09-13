namespace Cronofy.Requests
{
    using System.Collections;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of an availability rule upsert request.
    /// </summary>
    public sealed class UpsertAvailabilityRuleRequest
    {
        [JsonProperty("availability_rule_id")]
        public string AvailabilityRuleId { get; set; }

        [JsonProperty("tzid")]
        public string TimeZoneId { get; set; }

        [JsonProperty("calendar_ids")]
        public IEnumerable<string> CalendarIds { get; set; }
        
        [JsonProperty("weekly_periods")]
        public IEnumerable<WeeklyPeriod> WeeklyPeriods { get; set; }

        public sealed class WeeklyPeriod
        {
            [JsonProperty("day")]
            public string Day { get; set; }

            [JsonProperty("start_time")]
            public string StartTime { get; set; }

            [JsonProperty("end_time")]
            public string EndTime { get; set; }
        }
    }
}
