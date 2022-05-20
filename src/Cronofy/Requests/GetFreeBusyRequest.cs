namespace Cronofy.Requests
{
    using System;
    using System.Collections.Generic;

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

        /// <summary>
        /// Gets or sets the calendar IDs for the request.
        /// </summary>
        /// <value>
        /// The calendar IDs for the request.
        /// </value>
        public IEnumerable<string> CalendarIds { get; set; }

        /// <summary>
        /// Gets or sets the include IDs flag for the request.
        /// </summary>
        /// <value>
        /// The include IDs flag for the request.
        /// </value>
        public bool? IncludeIds { get; set; }

        /// <summary>
        /// Gets or sets the include free events flag for the request.
        /// </summary>
        /// <value>
        /// The include free events flag for the request.
        /// </value>
        public bool? IncludeFree { get; set; }

        /// <summary>
        /// Gets or sets the include deleted flag for the request.
        /// </summary>
        /// <value>
        /// The include deleted flag for the request.
        /// </value>
        public bool? IncludeDeleted { get; set; }

        /// <summary>
        /// Gets or sets the last modified time for the request.
        /// </summary>
        /// <value>
        /// The last modified time for the request.
        /// </value>
        public DateTime? LastModified { get; set; }
    }
}
