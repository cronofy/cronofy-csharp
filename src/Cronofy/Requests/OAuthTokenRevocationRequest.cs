namespace Cronofy.Requests
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of an OAuth token revocation request.
    /// </summary>
    public class OAuthTokenRevocationRequest
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
        /// Gets or sets the token of the OAuth authorization to be revoked.
        /// </summary>
        /// <value>
        /// The token of the OAuth authorization to be revoked.
        /// </value>
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
