namespace Cronofy.Requests
{
    /// <summary>
    /// Class representing a free-busy request.
    /// </summary>
    public sealed class GetFreeBusyRequest
    {
        /// <summary>
        /// Gets or sets the time zone ID for the request.
        /// </summary>
        /// <value>
        /// The time zone ID for the request.
        /// </value>
        public string TimeZoneId { get; set; }

        /// <summary>
        /// Gets or sets the from date for the request.
        /// </summary>
        /// <value>
        /// The from date for the request.
        /// </value>
        public Date? From { get; set; }

        /// <summary>
        /// Gets or sets the to date for the request.
        /// </summary>
        /// <value>
        /// The to date for the request.
        /// </value>
        public Date? To { get; set; }

        /// <summary>
        /// Gets or sets the include managed flag for the request.
        /// </summary>
        /// <value>
        /// The include managed flag for the request.
        /// </value>
        public bool? IncludeManaged { get; set; }
    }
}
