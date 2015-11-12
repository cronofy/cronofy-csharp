namespace Cronofy.Responses
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of a channel response.
    /// </summary>
    internal sealed class ChannelDetailResponse
    {
        /// <summary>
        /// Gets or sets the ID of the channel.
        /// </summary>
        /// <value>
        /// The ID of the channel.
        /// </value>
        [JsonProperty("channel_id")]
        public string ChannelId { get; set; }

        /// <summary>
        /// Gets or sets the callback URL of the channel.
        /// </summary>
        /// <value>
        /// The callback URL of the channel.
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
        /// Converts the response into a <see cref="Cronofy.Channel"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Cronofy.Channel"/> based upon the response.
        /// </returns>
        public Channel ToChannel()
        {
            var filters = new Channel.ChannelFilters();

            if (this.Filters != null)
            {
                filters.OnlyManaged = this.Filters.OnlyManaged;
            }

            return new Channel
            {
                Id = this.ChannelId,
                CallbackUrl = this.CallbackUrl,
                Filters = filters,
            };
        }

        /// <summary>
        /// Class for the deserialization of the filtering options of a channel
        /// response.
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
        }
    }
}
