namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cronofy.Requests;

    /// <summary>
    /// Builder class for
    /// <see cref="ICronofyAccountClient.UpsertEvent(string, IBuilder{UpsertEventRequest})"/>
    /// method calls.
    /// </summary>
    public sealed class UpsertEventRequestBuilder : IBuilder<UpsertEventRequest>
    {
        /// <summary>
        /// The OAuth application's ID for the event.
        /// </summary>
        private string eventId;

        /// <summary>
        /// The event's summary.
        /// </summary>
        private string summary;

        /// <summary>
        /// The event's description.
        /// </summary>
        private string description;

        /// <summary>
        /// The start time of the event.
        /// </summary>
        private DateTimeOffset? startTime;

        /// <summary>
        /// The start date of the event.
        /// </summary>
        private Date? startDate;

        /// <summary>
        /// The end time of the event.
        /// </summary>
        private DateTimeOffset? endTime;

        /// <summary>
        /// The end date of the event.
        /// </summary>
        private Date? endDate;

        /// <summary>
        /// The description of the event's location.
        /// </summary>
        private string locationDescription;

        /// <summary>
        /// The latitude of the event's location.
        /// </summary>
        private string locationLatitude;

        /// <summary>
        /// The longitude of the event's location.
        /// </summary>
        private string locationLongitude;

        /// <summary>
        /// The time zone ID of the event's start time.
        /// </summary>
        private string startTimeZoneId;

        /// <summary>
        /// The time zone ID of the event's end time.
        /// </summary>
        private string endTimeZoneId;

        /// <summary>
        /// The time zone of the event.
        /// </summary>
        private string timeZoneId;

        /// <summary>
        /// The reminders for the event.
        /// </summary>
        private int[] reminders;

        /// <summary>
        /// Whether reminders are only set on event creation.
        /// </summary>
        private bool? remindersCreateOnly;

        /// <summary>
        /// Whether the event is private or not.
        /// </summary>
        private bool? eventPrivate;

        /// <summary>
        /// The OAuth application's ID for the external event.
        /// </summary>
        private string eventUid;

        /// <summary>
        /// The URL of the event.
        /// </summary>
        private NullableString url;

        /// <summary>
        /// The transparency of the event.
        /// </summary>
        private string transparency;

        /// <summary>
        /// The color of the event.
        /// </summary>
        private string color;

        /// <summary>
        /// The removed attendees of the event.
        /// </summary>
        private ICollection<UpsertEventRequest.RequestAttendee> removedAttendees;

        /// <summary>
        /// The added attendees of the event.
        /// </summary>
        private ICollection<UpsertEventRequest.RequestAttendee> addedAttendees;

        /// <summary>
        /// The conferencing for the event.
        /// </summary>
        private UpsertEventRequest.RequestConferencing conferencing;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.UpsertEventRequestBuilder"/> class.
        /// </summary>
        public UpsertEventRequestBuilder()
        {
            this.startTimeZoneId = TimeZoneIdentifiers.Default;
            this.endTimeZoneId = TimeZoneIdentifiers.Default;
            this.addedAttendees = new List<UpsertEventRequest.RequestAttendee>();
            this.removedAttendees = new List<UpsertEventRequest.RequestAttendee>();
        }

        /// <summary>
        /// Sets the OAuth application's ID for the event.
        /// </summary>
        /// <param name="eventId">
        /// The OAuth application's ID for the event, must not be empty.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="eventId"/> is empty.
        /// </exception>
        public UpsertEventRequestBuilder EventId(string eventId)
        {
            Preconditions.NotEmpty("eventId", eventId);

            this.eventId = eventId;
            return this;
        }

        /// <summary>
        /// Sets the OAuth application's ID for an external event.
        /// </summary>
        /// <param name="eventUid">
        /// The OAuth application's ID for an external event, must not be empty.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="eventUid"/> is empty.
        /// </exception>
        public UpsertEventRequestBuilder EventUid(string eventUid)
        {
            Preconditions.NotEmpty("eventUid", eventUid);

            this.eventUid = eventUid;
            return this;
        }

        /// <summary>
        /// Sets the summary for the event.
        /// </summary>
        /// <param name="summary">
        /// The summary for the event, must not be empty.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="summary"/> is empty.
        /// </exception>
        public UpsertEventRequestBuilder Summary(string summary)
        {
            Preconditions.NotEmpty("summary", summary);

            this.summary = summary;
            return this;
        }

        /// <summary>
        /// Sets the description for the event.
        /// </summary>
        /// <param name="description">
        /// The description for the event.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public UpsertEventRequestBuilder Description(string description)
        {
            this.description = description;
            return this;
        }

        /// <summary>
        /// Sets the start time of the event using the given offset from UTC.
        /// </summary>
        /// <param name="start">
        /// The start time of the event.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public UpsertEventRequestBuilder Start(DateTimeOffset start)
        {
            this.startTime = start;
            this.startDate = null;
            return this;
        }

        /// <summary>
        /// Sets the start time of the event in UTC.
        /// </summary>
        /// <param name="year">
        /// The year of the start time.
        /// </param>
        /// <param name="month">
        /// The month of the start time.
        /// </param>
        /// <param name="day">
        /// The day of the start time.
        /// </param>
        /// <param name="hour">
        /// The hour of the start time.
        /// </param>
        /// <param name="minute">
        /// The minute of the start time.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the provided parameters do no generate a valid time.
        /// </exception>
        public UpsertEventRequestBuilder Start(int year, int month, int day, int hour, int minute)
        {
            var dateTimeOffset = new DateTimeOffset(year, month, day, hour, minute, 0, TimeSpan.Zero);
            return this.Start(dateTimeOffset);
        }

        /// <summary>
        /// Sets the start time of the event.
        /// </summary>
        /// <param name="start">
        /// The start time of the event.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public UpsertEventRequestBuilder Start(Date start)
        {
            this.startDate = start;
            this.startTime = null;
            return this;
        }

        /// <summary>
        /// Sets the start time of the event.
        /// </summary>
        /// <param name="year">
        /// The year of the start time.
        /// </param>
        /// <param name="month">
        /// The month of the start time.
        /// </param>
        /// <param name="day">
        /// The day of the start time.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the provided parameters do no generate a valid date.
        /// </exception>
        public UpsertEventRequestBuilder Start(int year, int month, int day)
        {
            var date = new Date(year, month, day);
            return this.Start(date);
        }

        /// <summary>
        /// Sets the end time of the event using the given offset from UTC.
        /// </summary>
        /// <param name="end">
        /// The end time of the event.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public UpsertEventRequestBuilder End(DateTimeOffset end)
        {
            this.endTime = end;
            this.endDate = null;
            return this;
        }

        /// <summary>
        /// Sets the end time of the event in UTC.
        /// </summary>
        /// <param name="year">
        /// The year of the end time.
        /// </param>
        /// <param name="month">
        /// The month of the end time.
        /// </param>
        /// <param name="day">
        /// The day of the end time.
        /// </param>
        /// <param name="hour">
        /// The hour of the end time.
        /// </param>
        /// <param name="minute">
        /// The minute of the end time.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the provided parameters do no generate a valid time.
        /// </exception>
        public UpsertEventRequestBuilder End(int year, int month, int day, int hour, int minute)
        {
            var dateTimeOffset = new DateTimeOffset(year, month, day, hour, minute, 0, TimeSpan.Zero);
            return this.End(dateTimeOffset);
        }

        /// <summary>
        /// Sets the end time of the event.
        /// </summary>
        /// <param name="end">
        /// The end time of the event.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public UpsertEventRequestBuilder End(Date end)
        {
            this.endDate = end;
            this.endTime = null;
            return this;
        }

        /// <summary>
        /// Sets the end time of the event.
        /// </summary>
        /// <param name="year">
        /// The year of the end time.
        /// </param>
        /// <param name="month">
        /// The month of the end time.
        /// </param>
        /// <param name="day">
        /// The day of the end time.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the provided parameters do no generate a valid date.
        /// </exception>
        public UpsertEventRequestBuilder End(int year, int month, int day)
        {
            var date = new Date(year, month, day);
            return this.End(date);
        }

        /// <summary>
        /// Sets the description of the event's location.
        /// </summary>
        /// <param name="description">
        /// The description of the event's location.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public UpsertEventRequestBuilder Location(string description)
        {
            this.locationDescription = description;
            return this;
        }

        /// <summary>
        /// Sets the description of the event's location.
        /// </summary>
        /// <param name="description">
        /// The description of the event's location.
        /// </param>
        /// <param name="latitude">
        /// The latitude of the event's location.
        /// </param>
        /// <param name="longitude">
        /// The longitude of the event's location.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public UpsertEventRequestBuilder Location(string description, string latitude, string longitude)
        {
            this.locationDescription = description;
            this.locationLatitude = latitude;
            this.locationLongitude = longitude;
            return this;
        }

        /// <summary>
        /// Sets the time zone identifier for the start and end times of the
        /// event. This is used as a visual hint by calendar clients and does
        /// not impact the Start and End of the event.
        /// </summary>
        /// <param name="timeZoneId">
        /// Time zone identifier, must not be empty.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="timeZoneId"/> is empty.
        /// </exception>
        public UpsertEventRequestBuilder TimeZoneId(string timeZoneId)
        {
            Preconditions.NotEmpty("timeZoneId", timeZoneId);

            this.startTimeZoneId = timeZoneId;
            this.endTimeZoneId = timeZoneId;
            this.timeZoneId = timeZoneId;

            return this;
        }

        /// <summary>
        /// Sets the reminders for the event.
        /// </summary>
        /// <param name="reminders">
        /// The times that reminders should be triggered, must not be
        /// <c>null</c>.
        /// <para>
        /// Each value is a number of minutes before the event start that a
        /// reminder should be triggered.
        /// </para>
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="reminders"/> is null.
        /// </exception>
        public UpsertEventRequestBuilder Reminders(int[] reminders)
        {
            Preconditions.NotNull("reminders", reminders);

            this.reminders = reminders;

            return this;
        }

        /// <summary>
        /// Sets whether reminders are only set on event creation.
        /// </summary>
        /// <param name="remindersCreateOnly">
        /// Whether reminders are only set on event creation.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public UpsertEventRequestBuilder RemindersCreateOnly(bool remindersCreateOnly)
        {
            this.remindersCreateOnly = remindersCreateOnly;

            return this;
        }

        /// <summary>
        /// Sets the event as private or public.
        /// </summary>
        /// <param name="eventPrivate">
        /// Whether the event is private or not.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public UpsertEventRequestBuilder EventPrivate(bool eventPrivate)
        {
            this.eventPrivate = eventPrivate;

            return this;
        }

        /// <summary>
        /// Sets the time zone identifier for the start time of the event.
        /// </summary>
        /// <param name="timeZoneId">
        /// Time zone identifier, must not be empty.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="timeZoneId"/> is empty.
        /// </exception>
        public UpsertEventRequestBuilder StartTimeZoneId(string timeZoneId)
        {
            Preconditions.NotEmpty("timeZoneId", timeZoneId);

            this.startTimeZoneId = timeZoneId;

            return this;
        }

        /// <summary>
        /// Sets the time zone identifier for the end time of the event.
        /// </summary>
        /// <param name="timeZoneId">
        /// Time zone identifier, must not be empty.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="timeZoneId"/> is empty.
        /// </exception>
        public UpsertEventRequestBuilder EndTimeZoneId(string timeZoneId)
        {
            Preconditions.NotEmpty("timeZoneId", timeZoneId);

            this.endTimeZoneId = timeZoneId;

            return this;
        }

        /// <summary>
        /// Sets the URL of the event.
        /// </summary>
        /// <param name="url">
        /// URL, must not be empty.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="url"/> is empty.
        /// </exception>
        public UpsertEventRequestBuilder Url(string url)
        {
            this.url = new NullableString(url);

            return this;
        }

        /// <summary>
        /// Sets the transparency of the event.
        /// </summary>
        /// <param name="transparency">
        /// Transparency, must not be empty.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="transparency"/> is not
        /// transparent or opaque.
        /// </exception>
        public UpsertEventRequestBuilder Transparency(string transparency)
        {
            Preconditions.True(new[] { "transparent", "opaque" }.Contains(transparency), "Transparency must be `transparent` or `opaque`");

            this.transparency = transparency;

            return this;
        }

        /// <summary>
        /// Sets the color of the event.
        /// </summary>
        /// <param name="color">
        /// The color of the event.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public UpsertEventRequestBuilder Color(string color)
        {
            this.color = color;

            return this;
        }

        /// <summary>
        /// Adds the attendee to the event.
        /// </summary>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <param name="email">The Email of the attendee.</param>
        /// <param name="displayName">The Display name of the attendee.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="email"/> is empty.
        /// </exception>
        public UpsertEventRequestBuilder AddAttendee(string email, string displayName = null)
        {
            Preconditions.NotEmpty("email", email);

            this.addedAttendees.Add(new UpsertEventRequest.RequestAttendee
            {
                Email = email,
                DisplayName = displayName,
            });

            return this;
        }

        /// <summary>
        /// Removes the attendee from the event.
        /// </summary>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <param name="email">The email of the attendee.</param>
        /// <param name="displayName">The display name of the attendee.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="email"/> is empty.
        /// </exception>
        public UpsertEventRequestBuilder RemoveAttendee(string email, string displayName = null)
        {
            Preconditions.NotEmpty("email", email);

            this.removedAttendees.Add(new UpsertEventRequest.RequestAttendee
            {
                Email = email,
                DisplayName = displayName,
            });
            return this;
        }

        /// <summary>
        /// Adds conferencing to the event.
        /// </summary>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <param name="profileId">The profile ID of the required conferencing. Either an explicit ID, or one of our documented built-in values.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="profileId"/> is empty.
        /// </exception>
        public UpsertEventRequestBuilder Conferencing(string profileId)
        {
            Preconditions.NotEmpty(nameof(profileId), profileId);

            this.conferencing = new UpsertEventRequest.RequestConferencing
            {
                ProfileId = profileId,
            };
            return this;
        }

        /// <summary>
        /// Adds explicit (not provisioned by Cronofy) conferencing to the event.
        /// </summary>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <param name="providerDescription">The user-facing provider name of the conferencing.</param>
        /// <param name="joinUrl">The URL to join the conferencing meeting.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="providerDescription"/> or <paramref name="joinUrl"/> are empty.
        /// </exception>
        public UpsertEventRequestBuilder ExplicitConferencing(string providerDescription, string joinUrl)
        {
            Preconditions.NotEmpty(nameof(providerDescription), providerDescription);
            Preconditions.NotEmpty(nameof(joinUrl), joinUrl);

            this.conferencing = new UpsertEventRequest.RequestConferencing
            {
                ProfileId = "explicit",
                ProviderDescription = providerDescription,
                JoinUrl = joinUrl,
            };
            return this;
        }

        /// <inheritdoc/>
        public UpsertEventRequest Build()
        {
            var request = new UpsertEventRequest
            {
                EventId = this.eventId,
                EventUid = this.eventUid,
                Summary = this.summary,
                Description = this.description,
                Start = GetEventTime("Start", this.startTime, this.startDate, this.startTimeZoneId),
                End = GetEventTime("End", this.endTime, this.endDate, this.endTimeZoneId),
                Url = this.url,
                Transparency = this.transparency,
                TimeZoneId = this.timeZoneId,
                Color = this.color,
                RemindersCreateOnly = this.remindersCreateOnly,
                EventPrivate = this.eventPrivate,
                Conferencing = this.conferencing,
            };

            if (string.IsNullOrEmpty(this.locationDescription) == false
                || string.IsNullOrEmpty(this.locationLatitude) == false
                || string.IsNullOrEmpty(this.locationLongitude) == false)
            {
                request.Location = new UpsertEventRequest.RequestLocation
                {
                    Description = this.locationDescription,
                    Latitude = this.locationLatitude,
                    Longitude = this.locationLongitude,
                };
            }

            if (this.reminders != null)
            {
                request.Reminders = this.reminders.Select(minutes => new UpsertEventRequest.RequestReminder { Minutes = minutes });
            }

            if (this.addedAttendees.Any() || this.removedAttendees.Any())
            {
                request.Attendees = new UpsertEventRequest.RequestAttendees();
            }

            if (this.addedAttendees.Any())
            {
                request.Attendees.Invite = this.addedAttendees;
            }

            if (this.removedAttendees.Any())
            {
                request.Attendees.Remove = this.removedAttendees;
            }

            return request;
        }

        /// <summary>
        /// Gets an <see cref="EventTime"/> from the two nullable values.
        /// </summary>
        /// <returns>
        /// An <see cref="EventTime"/>.
        /// </returns>
        /// <param name="propertyName">
        /// The name of the property the <see cref="EventTime"/> will be
        /// assigned to. Used to generate more meaningful exception messages.
        /// </param>
        /// <param name="time">
        /// The time to create the <see cref="EventTime"/> from when not
        /// <c>null</c>.
        /// </param>
        /// <param name="date">
        /// The date to create the <see cref="EventTime"/> from when not
        /// <c>null</c>.
        /// </param>
        /// <param name="timeZoneId">
        /// Time zone identifier for the <see cref="EventTime"/>.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Raised when both <paramref name="time"/> and <paramref name="date"/>
        /// are <c>null</c>.
        /// </exception>
        private static EventTime GetEventTime(string propertyName, DateTimeOffset? time, Date? date, string timeZoneId)
        {
            if (time.HasValue)
            {
                return new EventTime(time.Value.ToUniversalTime(), timeZoneId);
            }

            if (date.HasValue)
            {
                return new EventTime(date.Value, timeZoneId);
            }

            return null;
        }
    }
}
