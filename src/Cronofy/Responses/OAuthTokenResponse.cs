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
        /// Gets or sets the account ID of the <see cref="AccessToken"/>.
        /// </summary>
        /// <value>
        /// The account ID of the <see cref="AccessToken"/>.
        /// </value>
        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the linking profile of the <see cref="AccessToken"/>.
        /// </summary>
        /// <value>
        /// The linking profile of the <see cref="AccessToken"/>.
        /// </value>
        [JsonProperty("linking_profile")]
        public ResponseLinkingProfile LinkingProfile { get; set; }

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
            if (string.IsNullOrEmpty(this.Scope))
            {
                return new string[0];
            }

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
            var token = new OAuthToken(
                this.AccessToken,
                this.RefreshToken,
                this.ExpiresIn,
                this.GetScopeArray());

            token.AccountId = this.AccountId;

            if (this.LinkingProfile != null)
            {
                token.LinkingProfile = this.LinkingProfile.ToLinkingProfile();
            }

            return token;
        }

        /// <summary>
        /// Class for the deserialization of the linking profile in an access
        /// token response.
        /// </summary>
        internal sealed class ResponseLinkingProfile
        {
            /// <summary>
            /// Gets or sets the provider name.
            /// </summary>
            /// <value>
            /// The provider name.
            /// </value>
            [JsonProperty("provider_name")]
            public string ProviderName { get; set; }

            /// <summary>
            /// Gets or sets the profile ID.
            /// </summary>
            /// <value>
            /// The profile ID.
            /// </value>
            [JsonProperty("profile_id")]
            public string ProfileId { get; set; }

            /// <summary>
            /// Gets or sets the profile name.
            /// </summary>
            /// <value>
            /// The profile name.
            /// </value>
            [JsonProperty("profile_name")]
            public string ProfileName { get; set; }

            /// <summary>
            /// Converts the response into a <see cref="LinkingProfile"/>.
            /// </summary>
            /// <returns>
            /// A <see cref="LinkingProfile"/> based upon the response.
            /// </returns>
            public LinkingProfile ToLinkingProfile()
            {
                return new LinkingProfile
                {
                    ProviderName = this.ProviderName,
                    Id = this.ProfileId,
                    Name = this.ProfileName,
                };
            }
        }
    }
}
