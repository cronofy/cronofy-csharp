namespace Cronofy.Requests
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of a conferencing service authorization URL request.
    /// </summary>
    public sealed class ConferencingServiceAuthorizationRequest
    {
        /// <summary>
        /// Gets or sets the redirect URI for after completion of the authorization flow.
        /// </summary>
        /// <value>
        /// The redirect URI after completion of the authorization flow.
        /// </value>
        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        /// <summary>
        /// Gets or sets the optional pre-selected conferencing provider name.
        /// </summary>
        /// <value>
        /// The optional pre-selected conferencing provider name.
        /// </value>
        [JsonProperty("provider_name")]
        public string ProviderName { get; set; }
    }
}
