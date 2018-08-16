namespace Cronofy.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of a read events response.
    /// </summary>
    internal sealed class ReadEventsResponse : IPagedResultsResponse<Event>
    {
        /// <summary>
        /// Gets or sets the array of events.
        /// </summary>
        /// <value>
        /// The array of events.
        /// </value>
        [JsonProperty("events")]
        public EventResponse[] Events { get; set; }

        /// <summary>
        /// Gets or sets the paging information.
        /// </summary>
        /// <value>
        /// The paging information.
        /// </value>
        [JsonProperty("pages")]
        public PagesResponse Pages { get; set; }

        /// <inheritdoc/>
        public IEnumerable<Event> GetResults()
        {
            return this.Events.Select(e => e.ToEvent());
        }

        /// <summary>
        /// Class for the deserialization of an event.
        /// </summary>
        internal sealed class EventResponse
        {
            /// <summary>
            /// Gets or sets the ID of the calendar the event is within.
            /// </summary>
            /// <value>
            /// The ID of the calendar the event is within.
            /// </value>
            [JsonProperty("calendar_id")]
            public string CalendarId { get; set; }

            /// <summary>
            /// Gets or sets the OAuth application's ID for the event, if it is
            /// an event the OAuth application is managing.
            /// </summary>
            /// <value>
            /// The OAuth application's ID for the event, <c>null</c> if the
            /// OAuth application is not managing this event.
            /// </value>
            [JsonProperty("event_id")]
            public string EventId { get; set; }

            /// <summary>
            /// Gets or sets the UID of the event.
            /// </summary>
            /// <value>
            /// The UID of the event.
            /// </value>
            [JsonProperty("event_uid")]
            public string EventUid { get; set; }

            /// <summary>
            /// Gets or sets the Google event ID of the event.
            /// </summary>
            /// <value>
            /// The Google event ID of the event.
            /// </value>
            [JsonProperty("google_event_id")]
            public string GoogleEventId { get; set; }

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
            public ResponseLocation Location { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this event has been
            /// deleted.
            /// </summary>
            /// <value>
            /// <c>true</c> if the event has been deleted; otherwise,
            /// <c>false</c>.
            /// </value>
            [JsonProperty("deleted")]
            public bool Deleted { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this event is a
            /// recurring event.
            /// </summary>
            /// <value>
            /// <c>true</c> if the event is a recurring event; otherwise,
            /// <c>false</c>.
            /// </value>
            [JsonProperty("recurring")]
            public bool Recurring { get; set; }

            /// <summary>
            /// Gets or sets the account's participation status.
            /// </summary>
            /// <value>
            /// The account's participation status.
            /// </value>
            [JsonProperty("participation_status")]
            public string ParticipationStatus { get; set; }

            /// <summary>
            /// Gets or sets the transparency of the event.
            /// </summary>
            /// <value>
            /// The transparency of the event.
            /// </value>
            [JsonProperty("transparency")]
            public string Transparency { get; set; }

            /// <summary>
            /// Gets or sets the status of the event.
            /// </summary>
            /// <value>
            /// The status of the event.
            /// </value>
            [JsonProperty("event_status")]
            public string EventStatus { get; set; }

            /// <summary>
            /// Gets or sets the categories assigned to the event.
            /// </summary>
            /// <value>
            /// The categories assigned to the event.
            /// </value>
            [JsonProperty("categories")]
            public string[] Categories { get; set; }

            /// <summary>
            /// Gets or sets the time the event was created.
            /// </summary>
            /// <value>
            /// The time the event was created.
            /// </value>
            [JsonProperty("created")]
            [JsonConverter(typeof(TimestampConverter))]
            public DateTime Created { get; set; }

            /// <summary>
            /// Gets or sets the time the event was last updated.
            /// </summary>
            /// <value>
            /// The time the event was last updated.
            /// </value>
            [JsonProperty("updated")]
            [JsonConverter(typeof(TimestampConverter))]
            public DateTime Updated { get; set; }

            /// <summary>
            /// Gets or sets the event's attendees.
            /// </summary>
            /// <value>
            /// The event's attendees.
            /// </value>
            [JsonProperty("attendees")]
            public AttendeeResponse[] Attendees { get; set; }

            /// <summary>
            /// Gets or sets the event's organizer.
            /// </summary>
            /// <value>
            /// The event's organizer.
            /// </value>
            [JsonProperty("organizer")]
            public OrganizerResponse Organizer { get; set; }

            /// <summary>
            /// Gets or sets the permissable options for this event.
            /// </summary>
            /// <value>
            /// The event's options.
            /// </value>
            [JsonProperty("options")]
            public EventOptions Options { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this event is private.
            /// </summary>
            /// <value>
            /// The event is private only.
            /// </value>
            [JsonProperty("event_private")]
            public bool EventPrivate { get; set; }

            /// <summary>
            /// Gets or sets the event's meeting URL.
            /// </summary>
            /// <value>
            /// The event's meeting URL.
            /// </value>
            [JsonProperty("meeting_url")]
            public string MeetingUrl { get; set; }

            /// <summary>
            /// Converts the response into a <see cref="Cronofy.Event"/>.
            /// </summary>
            /// <returns>
            /// A <see cref="Cronofy.Event"/> based upon the response.
            /// </returns>
            public Event ToEvent()
            {
                var evt = new Event
                {
                    CalendarId = this.CalendarId,
                    EventId = this.EventId,
                    EventUid = this.EventUid,
                    GoogleEventId = this.GoogleEventId,
                    Summary = this.Summary,
                    Description = this.Description,
                    Start = this.Start,
                    End = this.End,
                    Deleted = this.Deleted,
                    ParticipationStatus = this.ParticipationStatus,
                    Transparency = this.Transparency,
                    EventStatus = this.EventStatus,
                    Categories = this.Categories,
                    Created = this.Created,
                    Updated = this.Updated,
                    Recurring = this.Recurring,
                    EventPrivate = this.EventPrivate,
                    MeetingUrl = this.MeetingUrl,
                };

                if (this.Location != null)
                {
                    evt.Location = this.Location.ToLocation();
                }

                if (this.Attendees != null)
                {
                    evt.Attendees = this.Attendees.Select(a => a.ToAttendee()).ToArray();
                }

                if (this.Options != null)
                {
                    evt.Options = this.Options.ToOptions();
                }

                if (this.Organizer != null)
                {
                    evt.Organizer = this.Organizer.ToOrganizer();
                }

                return evt;
            }

            /// <summary>
            /// Class for the deserialization of the location for a read event
            /// response.
            /// </summary>
            internal sealed class ResponseLocation
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

                /// <summary>
                /// Converts the response into a <see cref="Cronofy.Location"/>.
                /// </summary>
                /// <returns>
                /// A <see cref="Cronofy.Location"/> based upon the response.
                /// </returns>
                public Location ToLocation()
                {
                    return new Location(this.Description, this.Latitude, this.Longitude);
                }
            }

            /// <summary>
            /// Class for the deserialization of the options for a read event
            /// response.
            /// </summary>
            internal sealed class EventOptions
            {
                /// <summary>
                /// Gets or sets a value indicating whether this event can be
                /// deleted.
                /// </summary>
                /// <value>
                /// <c>true</c> if the event can be deleted; otherwise,
                /// <c>false</c>.
                /// </value>
                [JsonProperty("delete")]
                public bool Delete { get; set; }

                /// <summary>
                /// Gets or sets a value indicating whether this event can be
                /// updated.
                /// </summary>
                /// <value>
                /// <c>true</c> if the event can be updated; otherwise,
                /// <c>false</c>.
                /// </value>
                [JsonProperty("update")]
                public bool Update { get; set; }

                /// <summary>
                /// Converts the response into a
                /// <see cref="Cronofy.EventOptions"/>.
                /// </summary>
                /// <returns>
                /// A <see cref="Cronofy.EventOptions"/> based upon the
                /// response.
                /// </returns>
                public Cronofy.EventOptions ToOptions()
                {
                    return new Cronofy.EventOptions
                    {
                        Delete = this.Delete,
                        Update = this.Update
                    };
                }
            }

            /// <summary>
            /// Class for the deserialization of an attendee for a read event
            /// response.
            /// </summary>
            internal sealed class AttendeeResponse
            {
                /// <summary>
                /// Gets or sets the email of the attendee.
                /// </summary>
                /// <value>
                /// The email of the attendee.
                /// </value>
                [JsonProperty("email")]
                public string Email { get; set; }

                /// <summary>
                /// Gets or sets the display name of the attendee.
                /// </summary>
                /// <value>
                /// The display name of the attendee.
                /// </value>
                [JsonProperty("display_name")]
                public string DisplayName { get; set; }

                /// <summary>
                /// Gets or sets the status of the attendee.
                /// </summary>
                /// <value>
                /// The status of the attendee.
                /// </value>
                [JsonProperty("status")]
                public string Status { get; set; }

                /// <summary>
                /// Converts the response into a <see cref="Cronofy.Attendee"/>.
                /// </summary>
                /// <returns>
                /// A <see cref="Cronofy.Attendee"/> based upon the response.
                /// </returns>
                public Attendee ToAttendee()
                {
                    return new Attendee
                    {
                        Email = this.Email,
                        DisplayName = this.DisplayName,
                        Status = this.Status,
                    };
                }
            }

            /// <summary>
            /// Class for the deserialization of an organizer for a read event
            /// response.
            /// </summary>
            internal sealed class OrganizerResponse
            {
                /// <summary>
                /// Gets or sets the email of the organizer.
                /// </summary>
                /// <value>
                /// The email of the organizer.
                /// </value>
                [JsonProperty("email")]
                public string Email { get; set; }

                /// <summary>
                /// Gets or sets the display name of the organizer.
                /// </summary>
                /// <value>
                /// The display name of the organizer.
                /// </value>
                [JsonProperty("display_name")]
                public string DisplayName { get; set; }

                /// <summary>
                /// Converts the response into a <see cref="Cronofy.Organizer"/>.
                /// </summary>
                /// <returns>
                /// A <see cref="Cronofy.Organizer"/> based upon the response.
                /// </returns>
                public Organizer ToOrganizer()
                {
                    return new Organizer
                    {
                        Email = this.Email,
                        DisplayName = this.DisplayName,
                    };
                }
            }
        }
    }
}
