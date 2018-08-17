namespace Cronofy.Requests
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialzation of a Sequenced availability request.
    /// </summary>
    public sealed class SequencedAvailabilityRequest
    {
        /// <summary>
        /// Gets or sets the sequence.
        /// </summary>
        /// <value>The sequence.</value>
        [JsonProperty("sequence")]
        public IEnumerable<SequenceRequest> Sequence { get; set; }

        /// <summary>
        /// Gets or sets the available periods for the request.
        /// </summary>
        /// <value>
        /// The available periods for the request.
        /// </value>
        [JsonProperty("available_periods")]
        public IEnumerable<AvailabilityRequest.AvailablePeriod> AvailablePeriods { get; set; }

        /// <summary>
        /// Class for the serialization of a sequence step.
        /// </summary>
        public sealed class SequenceRequest
        {
            /// <summary>
            /// Gets or sets the ordinal.
            /// </summary>
            /// <value>The ordinal.</value>
            [JsonProperty("ordinal")]
            public int? Ordinal { get; set; }

            /// <summary>
            /// Gets or sets the sequence identifier.
            /// </summary>
            /// <value>The sequence identifier.</value>
            [JsonProperty("sequence_id")]
            public string SequenceId { get; set; }

            /// <summary>
            /// Gets or sets the event details for the request.
            /// </summary>
            /// <value>
            /// The event details for the request.
            /// </value>
            [JsonProperty("event")]
            public UpsertEventRequest Event { get; set; }

            /// <summary>
            /// Gets or sets the participant groups for the request.
            /// </summary>
            /// <value>
            /// The participant groups for the request.
            /// </value>
            [JsonProperty("participants")]
            public IEnumerable<AvailabilityRequest.ParticipantGroup> Participants { get; set; }

            /// <summary>
            /// Gets or sets the participant groups for the request.
            /// </summary>
            /// <value>
            /// The participant groups for the request.
            /// </value>
            [JsonProperty("required_duration")]
            public AvailabilityRequest.Duration RequiredDuration { get; set; }

            /// <summary>
            /// Gets or sets the start interval for the request.
            /// </summary>
            /// <value>
            /// The start interval for the request.
            /// </value>
            [JsonProperty("start_interval")]
            public AvailabilityRequest.Duration StartInterval { get; set; }

            /// <summary>
            /// Gets or sets the available periods for the request.
            /// </summary>
            /// <value>
            /// The available periods for the request.
            /// </value>
            [JsonProperty("available_periods")]
            public IEnumerable<AvailabilityRequest.AvailablePeriod> AvailablePeriods { get; set; }

            /// <summary>
            /// Gets or sets the buffer.
            /// </summary>
            /// <value>The buffer.</value>
            [JsonProperty("buffer")]
            public AvailabilityRequest.Buffers Buffer { get; set; }
        }
    }
}
