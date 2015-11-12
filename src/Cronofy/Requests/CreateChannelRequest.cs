namespace Cronofy.Requests
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of a create channel request.
    /// </summary>
    internal sealed class CreateChannelRequest
    {
        /// <summary>
        /// Gets or sets the callback URL for the request.
        /// </summary>
        /// <value>
        /// The callback URL for the request.
        /// </value>
        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }
    }
}
