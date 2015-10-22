namespace Cronofy
{
    using System;
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
        private DateTimeOffset startTime;

        /// <summary>
        /// The end time of the event.
        /// </summary>
        private DateTimeOffset endTime;

        /// <summary>
        /// The description of the event's location.
        /// </summary>
        private string locationDescription;

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
        /// Sets the start time of the event.
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
        /// Thrown if the provided parameters do no generate a valid date.
        /// </exception>
        public UpsertEventRequestBuilder Start(int year, int month, int day, int hour, int minute)
        {
            var dateTimeOffset = new DateTimeOffset(year, month, day, hour, minute, 0, new TimeSpan(0));
            return this.Start(dateTimeOffset);
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
        public UpsertEventRequestBuilder End(DateTimeOffset end)
        {
            this.endTime = end;
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
        /// Thrown if the provided parameters do no generate a valid date.
        /// </exception>
        public UpsertEventRequestBuilder End(int year, int month, int day, int hour, int minute)
        {
            var dateTimeOffset = new DateTimeOffset(year, month, day, hour, minute, 0, new TimeSpan(0));
            return this.End(dateTimeOffset);
        }

        /// <summary>
        /// Sets the description of the event's location.
        /// </summary>
        /// <param name="location">
        /// The description of the event's location.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public UpsertEventRequestBuilder Location(string location)
        {
            this.locationDescription = location;
            return this;
        }

        /// <inheritdoc/>
        public UpsertEventRequest Build()
        {
            // TODO Conditionally assign location based upon locationDescription
            return new UpsertEventRequest
            {
                EventId = this.eventId,
                Summary = this.summary,
                Description = this.description,
                Start = new EventTime(this.startTime.ToUniversalTime(), "Etc/UTC"),
                End = new EventTime(this.endTime.ToUniversalTime(), "Etc/UTC"),
                Location = new UpsertEventRequest.RequestLocation
                {
                    Description = this.locationDescription,
                },
            };
        }
    }
}
