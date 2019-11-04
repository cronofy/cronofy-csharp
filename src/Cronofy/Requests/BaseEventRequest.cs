namespace Cronofy.Requests
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of an event request.
    /// </summary>
    public abstract class BaseEventRequest
    {
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
        /// Gets or sets the start time of the event. Represented as UTC regardless of timezone.
        /// </summary>
        /// <value>
        /// The start time of the event.
        /// </value>
        [JsonProperty("start")]
        [JsonConverter(typeof(EventTimeConverter))]
        public EventTime Start { get; set; }

        /// <summary>
        /// Gets or sets the end time of the event. Represented as UTC regardless of timezone.
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
        /// Gets or sets the URL of the event.
        /// </summary>
        /// <value>
        /// The URL of the event.
        /// </value>
        [JsonProperty("url")]
        [JsonConverter(typeof(NullableStringConverter))]
        public NullableString Url { get; set; }

        /// <summary>
        /// Gets or sets the reminders of the event.
        /// </summary>
        /// <value>
        /// The reminders of the event.
        /// </value>
        [JsonProperty("reminders")]
        public IEnumerable<RequestReminder> Reminders { get; set; }

        /// <summary>
        /// Gets or sets the transparency of the event.
        /// </summary>
        /// <value>
        /// The transparency of the event.
        /// </value>
        [JsonProperty("transparency")]
        public string Transparency { get; set; }

        /// <summary>
        /// Gets or sets the timezone ID of the event. Please note that start and end time must still be represented as UTC.
        /// </summary>
        /// <value>The time zone identifier.</value>
        [JsonProperty("tzid")]
        public string TimeZoneId { get; set; }

        /// <summary>
        /// Gets or sets the color of the event.
        /// </summary>
        /// <value>The color.</value>
        [JsonProperty("color")]
        public string Color { get; set; }

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

            /// <summary>
            /// Gets or sets the latitude of the location.
            /// </summary>
            /// <value>
            /// The latitude of the location.
            /// </value>
            [JsonProperty("lat")]
            public string Latitude { get; set; }

            /// <summary>
            /// Gets or sets the longitude of the location.
            /// </summary>
            /// <value>
            /// The longitude of the location.
            /// </value>
            [JsonProperty("long")]
            public string Longitude { get; set; }
        }

        /// <summary>
        /// Class for the serialization of the reminders for an upsert event
        /// request.
        /// </summary>
        public sealed class RequestReminder
        {
            /// <summary>
            /// Gets or sets the minutes offset of the reminder.
            /// </summary>
            /// <value>
            /// The minutes offset of the reminder.
            /// </value>
            [JsonProperty("minutes")]
            public int Minutes { get; set; }
        }
    }
}
