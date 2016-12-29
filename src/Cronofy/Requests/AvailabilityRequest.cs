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
        }
    }
}