namespace Cronofy.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of an OAuth token response.
    /// </summary>
    internal sealed class OAuthTokenResponse
    {
        /// <summary>
        /// Gets or sets the OAuth access token.
        /// </summary>
        /// <value>
        /// The OAuth access token.
        /// </value>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the OAuth refresh token.
        /// </summary>
        /// <value>
        /// The OAuth refresh token.
        /// </value>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets the number of seconds until the
        /// <see cref="AccessToken"/> expires.
        /// </summary>
        /// <value>
        /// The number of seconds until the <see cref="AccessToken"/> expires.
        /// </value>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Gets or sets the granted scope of the <see cref="AccessToken"/>.
        /// </summary>
        /// <value>
        /// The granted scope of the <see cref="AccessToken"/>.
        /// </value>
        [JsonProperty("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// Gets the granted scope of the <see cref="AccessToken"/> as an array
        /// of <see cref="string"/>s.
        /// </summary>
        /// <returns>
        /// The granted scope of the <see cref="AccessToken"/> as an array of
        /// <see cref="string"/>s.
        /// </returns>
        public string[] GetScopeArray()
        {
            return this.Scope.Split(new[] { ' ' });
        }

        /// <summary>
        /// Converts the response into a <see cref="Cronofy.OAuthToken"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Cronofy.OAuthToken"/> based upon the response.
        /// </returns>
        public OAuthToken ToToken()
        {
            return new OAuthToken(
                this.AccessToken,
                this.RefreshToken,
                this.ExpiresIn,
                this.GetScopeArray());
        }
    }
}
