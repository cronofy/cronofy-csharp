namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using Cronofy.Requests;

    /// <summary>
    /// Builder for generating <see cref="GetEventsRequest"/>s.
    /// </summary>
    public sealed class GetEventsRequestBuilder : IBuilder<GetEventsRequest>
    {
        /// <summary>
        /// The request's time zone ID.
        /// </summary>
        private string timeZoneId;

        /// <summary>
        /// The request's from date.
        /// </summary>
        private Date? from;

        /// <summary>
        /// The request's to date.
        /// </summary>
        private Date? to;

        /// <summary>
        /// The request's last modified time.
        /// </summary>
        private DateTime? lastModified;

        /// <summary>
        /// The request's include deleted flag.
        /// </summary>
        private bool? includeDeleted;

        /// <summary>
        /// The request's include moved flag.
        /// </summary>
        private bool? includeMoved;

        /// <summary>
        /// The request's include managed flag.
        /// </summary>
        private bool? includeManaged;

        /// <summary>
        /// The request's only managed flag.
        /// </summary>
        private bool? onlyManaged;

        /// <summary>
        /// The request's include geo flag.
        /// </summary>
        private bool? includeGeo;

        /// <summary>
        /// The request's calendar IDs.
        /// </summary>
        private IEnumerable<string> calendarIds;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.GetEventsRequestBuilder"/> class.
        /// </summary>
        public GetEventsRequestBuilder()
        {
            this.timeZoneId = TimeZoneIdentifiers.Default;
        }

        /// <summary>
        /// Sets the time zone ID for the request.
        /// </summary>
        /// <param name="timeZoneId">
        /// The time zone ID for the request, must not be empty.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="timeZoneId"/> is empty.
        /// </exception>
        public GetEventsRequestBuilder TimeZoneId(string timeZoneId)
        {
            Preconditions.NotEmpty("timeZoneId", timeZoneId);

            this.timeZoneId = timeZoneId;
            return this;
        }

        /// <summary>
        /// Sets the from date for the request.
        /// </summary>
        /// <param name="from">
        /// The from date for the request.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public GetEventsRequestBuilder From(Date from)
        {
            this.from = from;
            return this;
        }

        /// <summary>
        /// Sets the from date for the request.
        /// </summary>
        /// <param name="year">
        /// The year of the from date.
        /// </param>
        /// <param name="month">
        /// The month of the from date.
        /// </param>
        /// <param name="day">
        /// The day of the from date.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the provided parameters do no generate a valid date.
        /// </exception>
        public GetEventsRequestBuilder From(int year, int month, int day)
        {
            var date = new Date(year, month, day);

            return this.From(date);
        }

        /// <summary>
        /// Sets the to date for the request.
        /// </summary>
        /// <param name="to">
        /// The to date for the request.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public GetEventsRequestBuilder To(Date to)
        {
            this.to = to;
            return this;
        }

        /// <summary>
        /// Sets the from date for the request.
        /// </summary>
        /// <param name="year">
        /// The year of the to date.
        /// </param>
        /// <param name="month">
        /// The month of the to date.
        /// </param>
        /// <param name="day">
        /// The day of the to date.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the provided parameters do no generate a valid date.
        /// </exception>
        public GetEventsRequestBuilder To(int year, int month, int day)
        {
            var date = new Date(year, month, day);

            return this.To(date);
        }

        /// <summary>
        /// Sets the last modified time for the request.
        /// </summary>
        /// <param name="lastModified">
        /// The time the an event must have been modified on or after in order
        /// to be returned.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public GetEventsRequestBuilder LastModified(DateTime lastModified)
        {
            this.lastModified = lastModified;
            return this;
        }

        /// <summary>
        /// Sets the include deleted flag for the request.
        /// </summary>
        /// <param name="includeDeleted">
        /// A flag specifying whether deleted events should be returned.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public GetEventsRequestBuilder IncludeDeleted(bool includeDeleted)
        {
            this.includeDeleted = includeDeleted;
            return this;
        }

        /// <summary>
        /// Sets the include moved flag for the request.
        /// </summary>
        /// <param name="includeMoved">
        /// A flag specifying whether events that have moved out of the
        /// specified window should be returned.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public GetEventsRequestBuilder IncludeMoved(bool includeMoved)
        {
            this.includeMoved = includeMoved;
            return this;
        }

        /// <summary>
        /// Sets the include managed flag for the request.
        /// </summary>
        /// <param name="includeManaged">
        /// A flag specifying whether events that are managed by the application
        /// should be returned.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public GetEventsRequestBuilder IncludeManaged(bool includeManaged)
        {
            this.includeManaged = includeManaged;
            return this;
        }

        /// <summary>
        /// Sets the only managed flag for the request.
        /// </summary>
        /// <param name="onlyManaged">
        /// A flag specifying whether only events that are managed by the
        /// application should be returned.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public GetEventsRequestBuilder OnlyManaged(bool onlyManaged)
        {
            this.onlyManaged = onlyManaged;
            return this;
        }

        /// <summary>
        /// Sets the include geo flag for the request.
        /// </summary>
        /// <param name="includeGeo">
        /// A flag specifying whether latitude and longitude data should
        /// be returned with events where available.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public GetEventsRequestBuilder IncludeGeo(bool includeGeo)
        {
            this.includeGeo = includeGeo;
            return this;
        }

        /// <summary>
        /// Sets the calendar IDs for the request.
        /// </summary>
        /// <param name="calendarIds">
        /// The calendar IDs to restrict the events to, must not be null.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="calendarIds"/> is null.
        /// </exception>
        public GetEventsRequestBuilder CalendarIds(IEnumerable<string> calendarIds)
        {
            Preconditions.NotNull("calendarIds", calendarIds);

            this.calendarIds = calendarIds;
            return this;
        }

        /// <summary>
        /// Sets the calendar ID for the request.
        /// </summary>
        /// <param name="calendarId">
        /// The calendar ID to restrict the events to, must not be null.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="calendarId"/> is null.
        /// </exception>
        public GetEventsRequestBuilder CalendarId(string calendarId)
        {
            Preconditions.NotNull("calendarId", calendarId);

            return this.CalendarIds(new[] { calendarId });
        }

        /// <inheritdoc/>
        public GetEventsRequest Build()
        {
            return new GetEventsRequest
            {
                TimeZoneId = this.timeZoneId,
                From = this.from,
                To = this.to,
                LastModified = this.lastModified,
                IncludeDeleted = this.includeDeleted,
                IncludeMoved = this.includeMoved,
                IncludeManaged = this.includeManaged,
                OnlyManaged = this.onlyManaged,
                IncludeGeo = this.includeGeo,
                CalendarIds = this.calendarIds,
            };
        }
    }
}
