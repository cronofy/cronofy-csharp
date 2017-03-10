namespace Cronofy.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of a link token response.
    /// </summary>
    internal sealed class LinkTokenResponse
    {
        /// <summary>
        /// Gets or sets the link token.
        /// </summary>
        /// <value>
        /// The link token.
        /// </value>
        [JsonProperty("link_token")]
        public string LinkToken { get; set; }
    }
}
