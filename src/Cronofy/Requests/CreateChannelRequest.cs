namespace Cronofy.Requests
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of a create channel request.
    /// </summary>
    public sealed class CreateChannelRequest
    {
        /// <summary>
        /// Gets or sets the callback URL for the request.
        /// </summary>
        /// <value>
        /// The callback URL for the request.
        /// </value>
        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }

        /// <summary>
        /// Gets or sets the filters for the channel.
        /// </summary>
        /// <value>
        /// The filters for the channel.
        /// </value>
        [JsonProperty("filters")]
        public ChannelFilters Filters { get; set; }

        /// <summary>
        /// Class for the serialization of the filtering options of a create
        /// channel request.
        /// </summary>
        public sealed class ChannelFilters
        {
            /// <summary>
            /// Gets or sets the only managed flag.
            /// </summary>
            /// <value>
            /// The only managed flag.
            /// </value>
            [JsonProperty("only_managed")]
            public bool? OnlyManaged { get; set; }

            /// <summary>
            /// Gets or sets the calendar IDs for the request.
            /// </summary>
            /// <value>
            /// The calendar IDs for the request.
            /// </value>
            [JsonProperty("calendar_ids")]
            public IEnumerable<string> CalendarIds { get; set; }
        }
    }
}
