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
        /// Gets or sets a value indicating whether reminders are only set on
        /// event creation.
        /// </summary>
        /// <value>
        /// Whether reminders are only set on event creation.
        /// </value>
        [JsonProperty("reminders_create_only")]
        public bool? RemindersCreateOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this event is private on
        /// event creation.
        /// </summary>
        /// <value>
        /// The event is private only set on event creation.
        /// </value>
        [JsonProperty("event_private")]
        public bool? EventPrivate { get; set; }

        /// <summary>
        /// Gets or sets the conferencing configuration for the event.
        /// </summary>
        /// <value>
        /// The conferencing configuration for the event.
        /// </value>
        [JsonProperty("conferencing")]
        public RequestConferencing Conferencing { get; set; }

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

        /// <summary>
        /// Class for the serialization of event conferencing.
        /// </summary>
        public sealed class RequestConferencing
        {
            /// <summary>
            /// Gets or sets the conferencing profile ID.
            /// </summary>
            /// <value>
            /// The conferencing profile ID.
            /// </value>
            [JsonProperty("profile_id")]
            public string ProfileId { get; set; }

            /// <summary>
            /// Gets or sets the conferencing provider's user-facing name.
            /// Only valid when using a <see cref="ProfileId"/> equal to <c>"explicit"</c>.
            /// </summary>
            /// <value>
            /// The conferencing provider's user-facing name.
            /// </value>
            [JsonProperty("provider_description")]
            public string ProviderDescription { get; set; }

            /// <summary>
            /// Gets or sets the conferencing join URL.
            /// Only valid when using a <see cref="ProfileId"/> equal to <c>"explicit"</c>.
            /// </summary>
            /// <value>
            /// The conferencing join URL.
            /// </value>
            [JsonProperty("join_url")]
            public string JoinUrl { get; set; }
        }
    }
}
