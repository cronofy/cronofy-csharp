namespace Cronofy.Requests
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of an availability request.
    /// </summary>
    public sealed class AvailabilityRequest
    {
        /// <summary>
        /// Gets or sets the participant groups for the request.
        /// </summary>
        /// <value>
        /// The participant groups for the request.
        /// </value>
        [JsonProperty("participants")]
        public IEnumerable<ParticipantGroup> Participants { get; set; }

        /// <summary>
        /// Gets or sets the participant groups for the request.
        /// </summary>
        /// <value>
        /// The participant groups for the request.
        /// </value>
        [JsonProperty("required_duration")]
        public Duration RequiredDuration { get; set; }

        /// <summary>
        /// Gets or sets the available periods for the request.
        /// </summary>
        /// <value>
        /// The available periods for the request.
        /// </value>
        [JsonProperty("available_periods")]
        public IEnumerable<AvailablePeriod> AvailablePeriods { get; set; }

        /// <summary>
        /// Gets or sets the start interval for the request.
        /// </summary>
        /// <value>
        /// The start interval for the request.
        /// </value>
        [JsonProperty("start_interval")]
        public Duration StartInterval { get; set; }

        /// <summary>
        /// Gets or sets the max results for the request.
        /// </summary>
        /// <value>
        /// The maximum number of available periods or slots for the request.
        /// </value>
        [JsonProperty("max_results")]
        public int? MaxResults { get; set; }

        /// <summary>
        /// Gets or sets the buffer.
        /// </summary>
        /// <value>The buffer.</value>
        [JsonProperty("buffer")]
        public Buffers Buffer { get; set; }

        /// <summary>
        /// Class for serialization of a buffer.
        /// </summary>
        public sealed class Buffers
        {
            /// <summary>
            /// Gets or sets the before buffer.
            /// </summary>
            /// <value>The before.</value>
            [JsonProperty("before")]
            public BufferDefintion Before { get; set; }

            /// <summary>
            /// Gets or sets the after buffer.
            /// </summary>
            /// <value>The after.</value>
            [JsonProperty("after")]
            public BufferDefintion After { get; set; }
        }

        /// <summary>
        /// Class for seralization of a buffer.
        /// </summary>
        public sealed class BufferDefintion
        {
            /// <summary>
            /// Gets or sets the minimum duration.
            /// </summary>
            /// <value>The minimum.</value>
            [JsonProperty("minimum")]
            public Duration Minimum { get; set; }

            /// <summary>
            /// Gets or sets the maximum duration.
            /// </summary>
            /// <value>The maximum.</value>
            [JsonProperty("maximum")]
            public Duration Maximum { get; set; }
        }

        /// <summary>
        /// Class for the serialization of a duration.
        /// </summary>
        public sealed class AvailablePeriod
        {
            /// <summary>
            /// Gets or sets the start time of the period.
            /// </summary>
            /// <value>
            /// The start time of the period.
            /// </value>
            [JsonProperty("start")]
            [JsonConverter(typeof(TimestampConverter))]
            public DateTimeOffset Start { get; set; }

            /// <summary>
            /// Gets or sets the end time of the period.
            /// </summary>
            /// <value>
            /// The end time of the period.
            /// </value>
            [JsonProperty("end")]
            [JsonConverter(typeof(TimestampConverter))]
            public DateTimeOffset End { get; set; }
        }

        /// <summary>
        /// Class for the serialization of a duration.
        /// </summary>
        public sealed class Duration
        {
            /// <summary>
            /// Gets or sets the total minutes of the duration.
            /// </summary>
            /// <value>
            /// The total minutes of the duration.
            /// </value>
            [JsonProperty("minutes")]
            public int Minutes { get; set; }
        }

        /// <summary>
        /// Class for the serialization of a participant group.
        /// </summary>
        public sealed class ParticipantGroup
        {
            /// <summary>
            /// Gets or sets the members of the group.
            /// </summary>
            /// <value>
            /// The members of the group.
            /// </value>
            [JsonProperty("members")]
            public IEnumerable<Member> Members { get; set; }

            /// <summary>
            /// Gets or sets the required property of the group.
            /// </summary>
            /// <value>
            /// The required property of the group.
            /// </value>
            [JsonProperty("required")]
            public object Required { get; set; }
        }

        /// <summary>
        /// Class for the serialization of a member.
        /// </summary>
        public sealed class Member
        {
            /// <summary>
            /// Gets or sets the subject the member relates to.
            /// </summary>
            /// <value>
            /// The subject the member relates to.
            /// </value>
            [JsonProperty("sub")]
            public string Sub { get; set; }

            /// <summary>
            /// Gets or sets the available periods for the member.
            /// </summary>
            /// <value>
            /// The available periods for the request.
            /// </value>
            [JsonProperty("available_periods")]
            public IEnumerable<AvailablePeriod> AvailablePeriods { get; set; }

            /// <summary>
            /// Gets or sets the calendar IDs for the member.
            /// </summary>
            /// <value>
            /// The calendar IDs for the request.
            /// </value>
            [JsonProperty("calendar_ids")]
            public IEnumerable<string> CalendarIds { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether Managed Availability is taken into account for this member.
            /// </summary>
            /// <value>
            /// Whether Managed Availability is taken into account for this member.
            /// </value>
            [JsonProperty("managed_availability")]
            public bool? ManagedAvailability { get; set; }

            /// <summary>
            /// Gets or sets the Availability Rules, stored against the member, which will be considered.
            /// A <c>null</c> value will mean all Availability Rules will be used, when <see cref="ManagedAvailability" /> is <c>true</c>.
            /// An empty list will mean that none of the Account's Availability Rules will be used.
            /// </summary>
            /// <value>
            /// The Availability Rules to be considered for the member.
            /// </value>
            [JsonProperty("availability_rule_ids")]
            public IEnumerable<string> AvailabilityRuleIds { get; set; }
        }
    }
}
