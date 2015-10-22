namespace Cronofy.Requests
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of an upsert event request.
    /// </summary>
    public sealed class UpsertEventRequest
    {
        /// <summary>
        /// Gets or sets the OAuth application's ID for the event.
        /// </summary>
        /// <value>
        /// The OAuth application's ID for the event.
        /// </value>
        [JsonProperty("event_id")]
        public string EventId { get; set; }

        /// <summary>
        /// Gets or sets the event's summary.
        /// </summary>
        /// <value>
        /// The event's summary.
        /// </value>
        [JsonProperty("summary")]
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the event's description.
        /// </summary>
        /// <value>
        /// The event's description.
        /// </value>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the start time of the event.
        /// </summary>
        /// <value>
        /// The start time of the event.
        /// </value>
        [JsonProperty("start")]
        [JsonConverter(typeof(EventTimeConverter))]
        public EventTime Start { get; set; }

        /// <summary>
        /// Gets or sets the end time of the event.
        /// </summary>
        /// <value>
        /// The end time of the event.
        /// </value>
        [JsonProperty("end")]
        [JsonConverter(typeof(EventTimeConverter))]
        public EventTime End { get; set; }

        /// <summary>
        /// Gets or sets the location of the event.
        /// </summary>
        /// <value>
        /// The location of the event.
        /// </value>
        [JsonProperty("location")]
        public RequestLocation Location { get; set; }

        /// <summary>
        /// Class for the serialization of the location for an upsert event
        /// request.
        /// </summary>
        public sealed class RequestLocation
        {
            /// <summary>
            /// Gets or sets the description of the location.
            /// </summary>
            /// <value>
            /// The description of the location.
            /// </value>
            [JsonProperty("description")]
            public string Description { get; set; }
        }
    }
}
