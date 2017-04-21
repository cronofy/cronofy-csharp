namespace Cronofy.Requests
{
    using System.Collections.Generic;
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
        /// Gets or sets the OAuth application's ID for an external event.
        /// </summary>
        /// <value>
        /// The OAuth application's ID for an external event.
        /// </value>
        [JsonProperty("event_uid")]
        public string EventUid { get; set; }

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
        /// Gets or sets the attendees of the event.
        /// </summary>
        /// <value>
        /// The attendees of the event.
        /// </value>
        [JsonProperty("attendees")]
        public RequestAttendees Attendees { get; set; }

        /// <summary>
        /// Gets or sets the transparency of the event.
        /// </summary>
        /// <value>
        /// The transparency of the event.
        /// </value>
        [JsonProperty("transparency")]
        public string Transparency { get; set; }

        /// <summary>
        /// Gets or sets the timezone ID of the event.
        /// </summary>
        /// <value>The time zone identifier.</value>
        [JsonProperty("tzid")]
        public string TimeZoneId { get; set; }

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

        /// <summary>
        /// Class for the serialization of the attendees for an upsert event
        /// request.
        /// </summary>
        public sealed class RequestAttendees
        {
            /// <summary>
            /// Gets or sets the attendees to invite to the event.
            /// </summary>
            /// <value>
            /// The attendees of the event to invite.
            /// </value>
            [JsonProperty("invite")]
            public IEnumerable<RequestAttendee> Invite { get; set; }

            /// <summary>
            /// Gets or sets the attendees to remove from event.
            /// </summary>
            /// <value>
            /// The attendees of the event to remove.
            /// </value>
            [JsonProperty("remove")]
            public IEnumerable<RequestAttendee> Remove { get; set; }
        }

        /// <summary>
        /// Class for the serialization of an attendee for an upsert event
        /// request.
        /// </summary>
        public sealed class RequestAttendee
        {
            /// <summary>
            /// Gets or sets the attendee's email.
            /// </summary>
            /// <value>
            /// The attendee's email.
            /// </value>
            [JsonProperty("email")]
            public string Email { get; set; }

            /// <summary>
            /// Gets or sets the attendee's display name.
            /// </summary>
            /// <value>
            /// The attendee's display name.
            /// </value>
            [JsonProperty("display_name")]
            public string DisplayName { get; set; }
        }
    }
}
