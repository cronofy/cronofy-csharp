namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cronofy.Requests;

    /// <summary>
    /// Builder class for smart invite events.
    /// </summary>
    public sealed class SmartInviteEventRequestBuilder : IBuilder<SmartInviteEventRequest>
    {
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
        /// Initializes a new instance of the
        /// <see cref="Cronofy.SmartInviteEventRequestBuilder"/> class.
        /// </summary>
        public SmartInviteEventRequestBuilder()
        {
            this.startTimeZoneId = TimeZoneIdentifiers.Default;
            this.endTimeZoneId = TimeZoneIdentifiers.Default;
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
        public SmartInviteEventRequestBuilder Summary(string summary)
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
        public SmartInviteEventRequestBuilder Description(string description)
        {
            this.description = description;
            return this;
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
        public SmartInviteEventRequestBuilder Start(DateTimeOffset start)
        {
            this.startTime = start;
            this.startDate = null;
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
        public SmartInviteEventRequestBuilder Start(int year, int month, int day, int hour, int minute)
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
        public SmartInviteEventRequestBuilder Start(Date start)
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
        public SmartInviteEventRequestBuilder Start(int year, int month, int day)
        {
            var date = new Date(year, month, day);
            return this.Start(date);
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
        public SmartInviteEventRequestBuilder End(DateTimeOffset end)
        {
            this.endTime = end;
            this.endDate = null;
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
        public SmartInviteEventRequestBuilder End(int year, int month, int day, int hour, int minute)
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
        public SmartInviteEventRequestBuilder End(Date end)
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
        public SmartInviteEventRequestBuilder End(int year, int month, int day)
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
        public SmartInviteEventRequestBuilder Location(string description)
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
        public SmartInviteEventRequestBuilder Location(string description, string latitude, string longitude)
        {
            this.locationDescription = description;
            this.locationLatitude = latitude;
            this.locationLongitude = longitude;
            return this;
        }

        /// <summary>
        /// Sets the time zone identifier for the start and end times of the
        /// event.
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
        public SmartInviteEventRequestBuilder TimeZoneId(string timeZoneId)
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
        /// <code>null</code>.
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
        public SmartInviteEventRequestBuilder Reminders(int[] reminders)
        {
            Preconditions.NotNull("reminders", reminders);

            this.reminders = reminders;

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
        public SmartInviteEventRequestBuilder StartTimeZoneId(string timeZoneId)
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
        public SmartInviteEventRequestBuilder EndTimeZoneId(string timeZoneId)
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
        public SmartInviteEventRequestBuilder Url(string url)
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
        public SmartInviteEventRequestBuilder Transparency(string transparency)
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
        public SmartInviteEventRequestBuilder Color(string color)
        {
            this.color = color;

            return this;
        }

        /// <inheritdoc/>
        public SmartInviteEventRequest Build()
        {
            var request = new SmartInviteEventRequest
            {
                Summary = this.summary,
                Description = this.description,
                Start = GetEventTime("Start", this.startTime, this.startDate, this.startTimeZoneId),
                End = GetEventTime("End", this.endTime, this.endDate, this.endTimeZoneId),
                Url = this.url,
                Transparency = this.transparency,
                TimeZoneId = this.timeZoneId,
                Color = this.color,
            };

            if (string.IsNullOrEmpty(this.locationDescription) == false
                || string.IsNullOrEmpty(this.locationLatitude) == false
                || string.IsNullOrEmpty(this.locationLongitude) == false)
            {
                request.Location = new BaseEventRequest.RequestLocation
                {
                    Description = this.locationDescription,
                    Latitude = this.locationLatitude,
                    Longitude = this.locationLongitude,
                };
            }

            if (this.reminders != null && this.reminders.Length > 0)
            {
                request.Reminders = this.reminders.Select(minutes => new BaseEventRequest.RequestReminder { Minutes = minutes });
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
        /// <code>null</code>.
        /// </param>
        /// <param name="date">
        /// The date to create the <see cref="EventTime"/> from when not
        /// <code>null</code>.
        /// </param>
        /// <param name="timeZoneId">
        /// Time zone identifier for the <see cref="EventTime"/>.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Raised when both <paramref name="time"/> and <paramref name="date"/>
        /// are <code>null</code>.
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
