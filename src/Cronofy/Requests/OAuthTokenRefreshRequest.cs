namespace Cronofy.Requests
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of an OAuth token refresh request.
    /// </summary>
    internal sealed class OAuthTokenRefreshRequest
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
        /// Gets or sets the refresh token.
        /// </summary>
        /// <value>
        /// The refresh token.
        /// </value>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
