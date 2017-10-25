namespace Cronofy.Requests
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of an upsert event request.
    /// </summary>
    public sealed class UpsertEventRequest : BaseEventRequest
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
        /// Gets or sets the attendees of the event.
        /// </summary>
        /// <value>
        /// The attendees of the event.
        /// </value>
        [JsonProperty("attendees")]
        public RequestAttendees Attendees { get; set; }

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
