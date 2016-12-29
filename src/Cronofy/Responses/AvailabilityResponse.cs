namespace Cronofy.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of an availability response.
    /// </summary>
    internal sealed class AvailabilityResponse
    {
        /// <summary>
        /// Gets or sets the available periods of the response.
        /// </summary>
        /// <value>
        /// The available periods of the response.
        /// </value>
        [JsonProperty("available_periods")]
        public IEnumerable<AvailablePeriodResponse> AvailablePeriods { get; set; }

        /// <summary>
        /// Class for the deserialization of an available period response.
        /// </summary>
        internal sealed class AvailablePeriodResponse
        {
            /// <summary>
            /// Gets or sets the start time of the period.
            /// </summary>
            /// <value>
            /// The start time of the period.
            /// </value>
            [JsonProperty("start")]
            [JsonConverter(typeof(TimestampConverter))]
            public DateTime Start { get; set; }

            /// <summary>
            /// Gets or sets the end time of the period.
            /// </summary>
            /// <value>
            /// The end time of the period.
            /// </value>
            [JsonProperty("end")]
            [JsonConverter(typeof(TimestampConverter))]
            public DateTime End { get; set; }

            /// <summary>
            /// Gets or sets the participants for the period.
            /// </summary>
            /// <value>
            /// The participants for the period.
            /// </value>
            [JsonProperty("participants")]
            public ParticipantResponse[] Participants { get; set; }

            /// <summary>
            /// Converts the response to an <see cref="AvailablePeriod"/>.
            /// </summary>
            /// <returns>
            /// An <see cref="AvailablePeriod"/>.
            /// </returns>
            public AvailablePeriod ToAvailablePeriod()
            {
                return new AvailablePeriod
                {
                    Start = this.Start,
                    End = this.End,
                    Participants = this.Participants.Select(p => p.ToParticipant()).ToArray(),
                };
            }

            /// <summary>
            /// Class for the deserialization of the participants within an
            /// available period response.
            /// </summary>
            internal sealed class ParticipantResponse
            {
                /// <summary>
                /// Gets or sets the subject for the participant.
                /// </summary>
                /// <value>
                /// The subject for the participant.
                /// </value>
                [JsonProperty("sub")]
                public string Sub { get; set; }

                /// <summary>
                /// Returns the participant as an
                /// <see cref="AvailablePeriod.Participant"/>.
                /// </summary>
                /// <returns>
                /// The participant as an
                /// <see cref="AvailablePeriod.Participant"/>.
                /// </returns>
                public AvailablePeriod.Participant ToParticipant()
                {
                    return new AvailablePeriod.Participant
                    {
                        Sub = this.Sub,
                    };
                }
            }
        }
    }
}