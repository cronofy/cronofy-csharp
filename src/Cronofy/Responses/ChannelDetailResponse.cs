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
        /// Converts the response into a <see cref="Cronofy.Channel"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Cronofy.Channel"/> based upon the response.
        /// </returns>
        public Channel ToChannel()
        {
            return new Channel
            {
                Id = this.ChannelId,
                CallbackUrl = this.CallbackUrl,
                Filters = new Channel.ChannelFilters(),
            };
        }
    }
}
