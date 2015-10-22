namespace Cronofy.Requests
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of an OAuth token request.
    /// </summary>
    internal sealed class OAuthTokenRequest
    {
        /// <summary>
        /// Gets or sets the OAuth application's client identifier.
        /// </summary>
        /// <value>
        /// The OAuth application's client identifier.
        /// </value>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the OAuth application's client secret.
        /// </summary>
        /// <value>
        /// The OAuth application's client secret.
        /// </value>
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the grant type of the OAuth token request.
        /// </summary>
        /// <value>
        /// The grant type of the OAuth token request.
        /// </value>
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        /// <summary>
        /// Gets or sets the code retrieved from a successful OAuth
        /// authorization request.
        /// </summary>
        /// <value>
        /// The code retrieved from a successful OAuth authorization request.
        /// </value>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the redirect URI passed to the successful OAuth
        /// authorization request.
        /// </summary>
        /// <value>
        /// The redirect URI passed to the successful OAuth authorization request.
        /// </value>
        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }
    }
}
