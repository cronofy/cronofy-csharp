namespace Cronofy.Responses
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of a channels response.
    /// </summary>
    internal sealed class ChannelsResponse
    {
        /// <summary>
        /// Gets or sets the details of the channels.
        /// </summary>
        /// <value>
        /// The details of the channels.
        /// </value>
        [JsonProperty("channels")]
        public ChannelDetailResponse[] Channels { get; set; }
    }
}
