namespace Cronofy.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of a read availability rule response.
    /// </summary>
    internal sealed class AvailabilityRuleResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier of the availability rule.
        /// </summary>
        /// <value>
        /// The unique identifier of the availability rule.
        /// </value>
        [JsonProperty("availability_rule_id")]
        public string AvailabilityRuleId { get; set; }

        /// <summary>
        /// Gets or sets the time zone for which the availability rule start and end times are represented in.
        /// </summary>
        /// <value>
        /// The time zone for which the availability rule start and end times are represented in.
        /// </value>
        [JsonProperty("tzid")]
        public string TimeZoneId { get; set; }

        /// <summary>
        /// Gets or sets the calendars that should impact the user's availability.
        /// </summary>
        /// <value>
        /// The calendars that should impact the user's availability.
        /// </value>
        [JsonProperty("calendar_ids")]
        public IEnumerable<string> CalendarIds { get; set; }

        /// <summary>
        /// Gets or sets the weekly recurring periods for the availability rule.
        /// </summary>
        /// <value>
        /// The weekly recurring periods for the availability rule.
        /// </value>
        [JsonProperty("weekly_periods")]
        public IEnumerable<WeeklyPeriod> WeeklyPeriods { get; set; }

        /// <summary>
        /// Converts the response to an <see cref="AvailabilityRule"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="AvailabilityRule"/>.
        /// </returns>
        public AvailabilityRule ToAvailabilityRule()
        {
            return new AvailabilityRule
            {
                AvailabilityRuleId = this.AvailabilityRuleId,
                TimeZoneId = this.TimeZoneId,
                CalendarIds = this.CalendarIds?.ToArray(),
                WeeklyPeriods = this.WeeklyPeriods.Select(weeklyPeriod => weeklyPeriod.ToWeeklyPeriod()).ToArray(),
            };
        }

        /// <summary>
        /// Class for the deserialization of a weekly period within an availability rule.
        /// </summary>
        internal sealed class WeeklyPeriod
        {
            /// <summary>
            /// Gets or sets the week day the period applies to.
            /// </summary>
            /// <value>
            /// The week day the period applies to.
            /// </value>
            [JsonProperty("day")]
            public string Day { get; set; }

            /// <summary>
            /// Gets or sets the time of day the period should start.
            /// </summary>
            /// <value>
            /// The time of day the period should start.
            /// </value>
            [JsonProperty("start_time")]
            public string StartTime { get; set; }

            /// <summary>
            /// Gets or sets the time of day the period should end.
            /// </summary>
            /// <value>
            /// The time of day the period should end.
            /// </value>
            [JsonProperty("end_time")]
            public string EndTime { get; set; }

            /// <summary>
            /// Converts the response to an <see cref="AvailabilityRule.WeeklyPeriod"/>.
            /// </summary>
            /// <returns>
            /// An <see cref="AvailabilityRule"/>.
            /// </returns>
            public AvailabilityRule.WeeklyPeriod ToWeeklyPeriod()
            {
                return new AvailabilityRule.WeeklyPeriod
                {
                    Day = ToDayOfWeek(this.Day),
                    StartTime = this.StartTime,
                    EndTime = this.EndTime,
                };
            }

            /// <summary>
            /// Converts a day string to a day of the week representation.
            /// </summary>
            /// <param name="day">The string representation of the day of the week.</param>
            /// <returns>The day of the week represented by the string.</returns>
            private static DayOfWeek ToDayOfWeek(string day)
            {
                switch (day)
                {
                    case "monday":
                        return DayOfWeek.Monday;
                    case "tuesday":
                        return DayOfWeek.Tuesday;
                    case "wednesday":
                        return DayOfWeek.Wednesday;
                    case "thursday":
                        return DayOfWeek.Thursday;
                    case "friday":
                        return DayOfWeek.Friday;
                    case "saturday":
                        return DayOfWeek.Saturday;
                    case "sunday":
                        return DayOfWeek.Sunday;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(day), "Unexpected day");
                }
            }
        }
    }
}
