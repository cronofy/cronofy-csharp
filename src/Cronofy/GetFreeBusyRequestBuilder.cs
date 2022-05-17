namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using Cronofy.Requests;

    /// <summary>
    /// Builder for generating <see cref="GetFreeBusyRequest"/>s.
    /// </summary>
    public sealed class GetFreeBusyRequestBuilder : IBuilder<GetFreeBusyRequest>
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
        /// The request's include managed flag.
        /// </summary>
        private bool? includeManaged;

        /// <summary>
        /// The request's include IDs flag.
        /// </summary>
        private bool? includeIds;

        /// <summary>
        /// The request's include free events flag.
        /// </summary>
        private bool? includeFree;

        /// <summary>
        /// The request's include deleted events flag.
        /// </summary>
        private bool? includeDeleted;

        /// <summary>
        /// The request's last modified time.
        /// </summary>
        private DateTime? lastModified;

        /// <summary>
        /// The request's calendar IDs.
        /// </summary>
        private IEnumerable<string> calendarIds;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.GetFreeBusyRequestBuilder"/> class.
        /// </summary>
        public GetFreeBusyRequestBuilder()
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
        public GetFreeBusyRequestBuilder TimeZoneId(string timeZoneId)
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
        public GetFreeBusyRequestBuilder From(Date from)
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
        public GetFreeBusyRequestBuilder From(int year, int month, int day)
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
        public GetFreeBusyRequestBuilder To(Date to)
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
        public GetFreeBusyRequestBuilder To(int year, int month, int day)
        {
            var date = new Date(year, month, day);

            return this.To(date);
        }

        /// <summary>
        /// Sets the include managed flag for the request.
        /// </summary>
        /// <param name="includeManaged">
        /// A flag specifying whether free-busy information for events that are
        /// managed by the application should be returned.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public GetFreeBusyRequestBuilder IncludeManaged(bool includeManaged)
        {
            this.includeManaged = includeManaged;
            return this;
        }

        /// <summary>
        /// Sets the calendar IDs for the request.
        /// </summary>
        /// <param name="calendarIds">
        /// The calendar IDs to restrict the free-busy information to, must not
        /// be null.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="calendarIds"/> is null.
        /// </exception>
        public GetFreeBusyRequestBuilder CalendarIds(IEnumerable<string> calendarIds)
        {
            Preconditions.NotNull("calendarIds", calendarIds);

            this.calendarIds = calendarIds;
            return this;
        }

        /// <summary>
        /// Sets the calendar ID for the request.
        /// </summary>
        /// <param name="calendarId">
        /// The calendar ID to restrict the free-busy information to, must not
        /// be null.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="calendarId"/> is null.
        /// </exception>
        public GetFreeBusyRequestBuilder CalendarId(string calendarId)
        {
            Preconditions.NotNull("calendarId", calendarId);

            return this.CalendarIds(new[] { calendarId });
        }

        /// <summary>
        /// Sets the include IDs flag for the request.
        /// </summary>
        /// <param name="includeIds">
        /// A flag specifying whether event IDs should be returned.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public GetFreeBusyRequestBuilder IncludeIds(bool includeIds)
        {
            this.includeIds = includeIds;
            return this;
        }

        /// <summary>
        /// Sets the include free flag for the request.
        /// </summary>
        /// <param name="includeFree">
        /// A flag specifying whether free events should be returned.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public GetFreeBusyRequestBuilder IncludeFree(bool includeFree)
        {
            this.includeFree = includeFree;
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
        public GetFreeBusyRequestBuilder IncludeDeleted(bool includeDeleted)
        {
            this.includeDeleted = includeDeleted;
            return this;
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
        public GetFreeBusyRequestBuilder LastModified(DateTime lastModified)
        {
            this.lastModified = lastModified;
            return this;
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
        public GetFreeBusyRequestBuilder LastModified(DateTime lastModified)
        {
            this.lastModified = lastModified;
            return this;
        }

        /// <inheritdoc/>
        public GetFreeBusyRequest Build()
        {
            return new GetFreeBusyRequest
            {
                TimeZoneId = this.timeZoneId,
                From = this.from,
                To = this.to,
                IncludeManaged = this.includeManaged,
                CalendarIds = this.calendarIds,
                IncludeIds = this.includeIds,
                IncludeFree = this.includeFree,
                IncludeDeleted = this.includeDeleted,
                LastModified = this.lastModified,
            };
        }
    }
}
