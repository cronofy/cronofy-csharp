namespace Cronofy.Responses
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of a user info response.
    /// </summary>
    internal sealed class UserInfoResponse
    {
        /// <summary>
        /// Gets or sets the Cronofy account ID of the response.
        /// </summary>
        [JsonProperty("sub")]
        public string Sub { get; set; }

        /// <summary>
        /// Gets or sets the Cronofy account type of the response.
        /// </summary>
        [JsonProperty("cronofy.type")]
        public string CronofyType { get; set; }

        [JsonProperty("cronofy.data")]
        public CronofyData Data { get; set; }

        /// <summary>
        /// Converts the response into a <see cref="Cronofy.UserInfo"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="Cronofy.UserInfo"/> based upon the response.
        /// </returns>
        public UserInfo ToUserInfo()
        {
            return new UserInfo
            {
                Sub = this.Sub,
                CronofyType = this.CronofyType,
                Profiles = this.Data.Profiles.Select((p) => p.ToProfile()).ToArray(),
            };
        }

        public sealed class CronofyData 
        {
            /// <summary>
            /// Gets or sets the profiles.
            /// </summary>
            /// <value>The profiles.</value>
            [JsonProperty("profiles")]
            public ProfileResponse[] Profiles { get; set; }
        }

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
            /// Gets or sets the service name of the provider.
            /// </summary>
            /// <value>
            /// The service name of the provider.
            /// </value>
            [JsonProperty("provider_service")]
            public string ProviderService { get; set; }

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
            /// Gets or sets the profile calendars.
            /// </summary>
            /// <value>The profile calendars.</value>
            [JsonProperty("profile_calendars")]
            public CalendarsResponse.CalendarResponse[] ProfileCalendars { get; set; }

            /// <summary>
            /// Converts the response into a <see cref="Cronofy.Profile"/>.
            /// </summary>
            /// <returns>
            /// A <see cref="Cronofy.Profile"/> based upon the response.
            /// </returns>
            public UserInfo.Profile ToProfile()
            {
                var profileSummary = new Calendar.ProfileSummary {
                    ProviderName = this.ProviderName,
                    ProfileId = this.ProfileId,
                    Name = this.ProfileName,
                };

                return new UserInfo.Profile
                {
                    ProviderName = this.ProviderName,
                    ProviderService = this.ProviderService,
                    Id = this.ProfileId,
                    Name = this.ProfileName,
                    Connected = this.ProfileConnected,
                    RelinkUrl = this.ProfileRelinkUrl,
                    Calendars = this.ProfileCalendars.Select((pc) => 
                    {
                        var calendar = pc.ToCalendar();
                        calendar.Profile = profileSummary;
                        return calendar;
                    }).ToArray(),
                };
            }
        }
    }
}
