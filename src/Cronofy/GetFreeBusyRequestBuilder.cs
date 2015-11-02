namespace Cronofy
{
    using System;
    using Cronofy.Requests;

    /// <summary>
    /// Builder for generating <see cref="GetFreeBusyRequest"/>s.
    /// </summary>
    public sealed class GetFreeBusyRequestBuilder : IBuilder<GetFreeBusyRequest>
    {
        /// <summary>
        /// The default time zone ID for requests.
        /// </summary>
        private const string DefaultTimeZoneId = "Etc/UTC";

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
        /// Initializes a new instance of the
        /// <see cref="Cronofy.GetFreeBusyRequestBuilder"/> class.
        /// </summary>
        public GetFreeBusyRequestBuilder()
        {
            this.timeZoneId = DefaultTimeZoneId;
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

        /// <inheritdoc/>
        public GetFreeBusyRequest Build()
        {
            return new GetFreeBusyRequest
            {
                TimeZoneId = this.timeZoneId,
                From = this.from,
                To = this.to,
            };
        }
    }
}
