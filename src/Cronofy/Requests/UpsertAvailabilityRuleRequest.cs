namespace Cronofy.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of an upsert availability rule request.
    /// </summary>
    public sealed class UpsertAvailabilityRuleRequest
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
        /// Creates an upsert request from an existing availability rule.
        /// </summary>
        /// <param name="availabilityRule">Availability rule to create this upsert request from.</param>
        /// <returns>An upsert request for the given rule.</returns>
        public static UpsertAvailabilityRuleRequest FromAvailabilityRule(AvailabilityRule availabilityRule)
        {
            return new UpsertAvailabilityRuleRequest
            {
                AvailabilityRuleId = availabilityRule.AvailabilityRuleId,
                TimeZoneId = availabilityRule.TimeZoneId,
                CalendarIds = availabilityRule.CalendarIds?.ToArray(),
                WeeklyPeriods = availabilityRule.WeeklyPeriods.Select(WeeklyPeriod.FromWeeklyPeriod).ToArray(),
            };
        }

        /// <summary>
        /// Class for the serialization of a weekly period within an availability rule.
        /// </summary>
        public sealed class WeeklyPeriod
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
            /// Creates an upset weekly period request from a given weekly period.
            /// </summary>
            /// <param name="weeklyPeriod">The weekly period to copy from.</param>
            /// <returns>The weekly period upsert request.</returns>
            public static WeeklyPeriod FromWeeklyPeriod(AvailabilityRule.WeeklyPeriod weeklyPeriod)
            {
                return new WeeklyPeriod
                {
                    Day = ToDayString(weeklyPeriod.Day),
                    StartTime = weeklyPeriod.StartTime,
                    EndTime = weeklyPeriod.EndTime,
                };
            }

            /// <summary>
            /// Converts a day of the week to its string representation.
            /// </summary>
            /// <param name="day">The day of the week.</param>
            /// <returns>The string representation of the day of the week.</returns>
            private static string ToDayString(DayOfWeek day)
            {
                switch (day)
                {
                    case DayOfWeek.Monday:
                        return "monday";
                    case DayOfWeek.Tuesday:
                        return "tuesday";
                    case DayOfWeek.Wednesday:
                        return "wednesday";
                    case DayOfWeek.Thursday:
                        return "thursday";
                    case DayOfWeek.Friday:
                        return "friday";
                    case DayOfWeek.Saturday:
                        return "saturday";
                    case DayOfWeek.Sunday:
                        return "sunday";
                    default:
                        throw new ArgumentOutOfRangeException(nameof(day), "Unexpected day");
                }
            }
        }
    }
}
