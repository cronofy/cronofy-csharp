namespace Cronofy.Responses
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of a profiles response.
    /// </summary>
    internal sealed class ProfilesResponse
    {
        /// <summary>
        /// Gets or sets the array of profiles.
        /// </summary>
        /// <value>
        /// The array of profiles.
        /// </value>
        [JsonProperty("profiles")]
        public ProfileResponse[] Profiles { get; set; }

        /// <summary>
        /// Class for the deserialization of a profile response.
        /// </summary>
        internal sealed class ProfileResponse
        {
            /// <summary>
            /// Gets or sets the name of the provider.
            /// </summary>
            /// <value>
            /// The name of the provider.
            /// </value>
            [JsonProperty("provider_name")]
            public string ProviderName { get; set; }

            /// <summary>
            /// Gets or sets the ID of the profile.
            /// </summary>
            /// <value>
            /// The ID of the profile.
            /// </value>
            [JsonProperty("profile_id")]
            public string ProfileId { get; set; }

            /// <summary>
            /// Gets or sets the name of the profile.
            /// </summary>
            /// <value>
            /// The name of the profile.
            /// </value>
            [JsonProperty("profile_name")]
            public string ProfileName { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this profile is
            /// connected.
            /// </summary>
            /// <value>
            /// <c>true</c> if the profile is connected; otherwise,
            /// <c>false</c>.
            /// </value>
            [JsonProperty("profile_connected")]
            public bool ProfileConnected { get; set; }

            /// <summary>
            /// Gets or sets the relink URL for the profile.
            /// </summary>
            /// <value>
            /// The relink URL for the profile.
            /// </value>
            [JsonProperty("profile_relink_url")]
            public string ProfileRelinkUrl { get; set; }

            /// <summary>
            /// Converts the response into a <see cref="Cronofy.Profile"/>.
            /// </summary>
            /// <returns>
            /// A <see cref="Cronofy.Profile"/> based upon the response.
            /// </returns>
            public Profile ToProfile()
            {
                return new Profile
                {
                    ProviderName = this.ProviderName,
                    Id = this.ProfileId,
                    Name = this.ProfileName,
                    Connected = this.ProfileConnected,
                    RelinkUrl = this.ProfileRelinkUrl,
                };
            }
        }
    }
}
