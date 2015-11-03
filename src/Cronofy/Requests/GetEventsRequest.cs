namespace Cronofy.Requests
{
    using System;

    /// <summary>
    /// Class for the serialization of an get events request.
    /// </summary>
    public sealed class GetEventsRequest
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
        /// Gets or sets the last modified time for the request.
        /// </summary>
        /// <value>
        /// The last modified time for the request.
        /// </value>
        public DateTime? LastModified { get; set; }
    }
}
