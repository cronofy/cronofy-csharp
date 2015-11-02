namespace Cronofy.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of a read events response.
    /// </summary>
    internal sealed class FreeBusyResponse : IPagedResultsResponse<FreeBusy>
    {
        /// <summary>
        /// Gets or sets the array of events.
        /// </summary>
        /// <value>
        /// The array of events.
        /// </value>
        [JsonProperty("free_busy")]
        public FreeBusyPeriodResponse[] FreeBusy { get; set; }

        /// <summary>
        /// Gets or sets the paging information.
        /// </summary>
        /// <value>
        /// The paging information.
        /// </value>
        [JsonProperty("pages")]
        public PagesResponse Pages { get; set; }

        /// <inheritdoc/>
        public IEnumerable<FreeBusy> GetResults()
        {
            return this.FreeBusy.Select(e => e.ToFreeBusy());
        }

        /// <summary>
        /// Class for the deserialization of a free-busy period.
        /// </summary>
        internal sealed class FreeBusyPeriodResponse
        {
            /// <summary>
            /// Gets or sets the ID of the calendar the free-busy period is
            /// within.
            /// </summary>
            /// <value>
            /// The ID of the calendar the free-busy period is within.
            /// </value>
            [JsonProperty("calendar_id")]
            public string CalendarId { get; set; }

            /// <summary>
            /// Gets or sets the start time of the free-busy period.
            /// </summary>
            /// <value>
            /// The start time of the free-busy period.
            /// </value>
            [JsonProperty("start")]
            [JsonConverter(typeof(EventTimeConverter))]
            public EventTime Start { get; set; }

            /// <summary>
            /// Gets or sets the end time of the free-busy period.
            /// </summary>
            /// <value>
            /// The end time of the free-busy period.
            /// </value>
            [JsonProperty("end")]
            [JsonConverter(typeof(EventTimeConverter))]
            public EventTime End { get; set; }

            /// <summary>
            /// Gets or sets the account's free-busy status.
            /// </summary>
            /// <value>
            /// The account's free-busy status.
            /// </value>
            [JsonProperty("free_busy_status")]
            public string FreeBusyStatus { get; set; }

            /// <summary>
            /// Converts the response into a <see cref="Cronofy.FreeBusy"/>.
            /// </summary>
            /// <returns>
            /// A <see cref="Cronofy.FreeBusy"/> based upon the response.
            /// </returns>
            public FreeBusy ToFreeBusy()
            {
                return new FreeBusy
                {
                    CalendarId = this.CalendarId,
                    Start = this.Start,
                    End = this.End,
                    FreeBusyStatus = this.FreeBusyStatus,
                };
            }
        }
    }
}
