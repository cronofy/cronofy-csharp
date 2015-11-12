namespace Cronofy.Responses
{
    using System;
    using Cronofy;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of a channel response.
    /// </summary>
    internal sealed class ChannelResponse
    {
        /// <summary>
        /// Gets or sets the details of the channel.
        /// </summary>
        /// <value>
        /// The details of the channel.
        /// </value>
        [JsonProperty("channel")]
        public ChannelDetailResponse Channel { get; set; }

        /// <summary>
        /// Converts the response into a <see cref="Cronofy.Channel"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Cronofy.Channel"/> based upon the response.
        /// </returns>
        public Channel ToChannel()
        {
            if (this.Channel == null)
            {
                return null;
            }

            return this.Channel.ToChannel();
        }
    }
}
