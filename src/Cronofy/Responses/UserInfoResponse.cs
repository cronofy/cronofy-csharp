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

        /// <summary>
        /// Gets or sets the Cronofy account data of the response.
        /// </summary>
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
                Profiles = this.Data?.Profiles?.Select((p) => p.ToProfile()).ToArray(),
            };
        }

        /// <summary>
        /// Class for the deserialization of CronofyData.
        /// </summary>
        internal sealed class CronofyData
        {
            /// <summary>
            /// Gets or sets the authorization.
            /// </summary>
            /// <value>The authorization.</value>
            [JsonProperty("service_account")]
            public CronofyServiceAccount ServiceAccount { get; set; }

            /// <summary>
            /// Gets or sets the authorization.
            /// </summary>
            /// <value>The authorization.</value>
            [JsonProperty("authorization")]
            public CronofyAuthorization Authorization { get; set; }

            /// <summary>
            /// Gets or sets the account's email.
            /// </summary>
            /// <value>
            /// The email of the account.
            /// </value>
            [JsonProperty("email")]
            public string Email { get; set; }

            /// <summary>
            /// Gets or sets the profiles.
            /// </summary>
            /// <value>The profiles.</value>
            [JsonProperty("profiles")]
            public ProfileResponse[] Profiles { get; set; }
        }

                /// <summary>
        /// Class for the deserialization of an authorization response.
        /// </summary>
        internal sealed class CronofyServiceAccount
        {
            /// <summary>
            /// Gets or sets the provider name of the Service Account.
            /// </summary>
            /// <value>
            /// The provider name of the Service Account.
            /// </value>
            [JsonProperty("provider_name")]
            public string ProviderName { get; set; }

            /// <summary>
            /// Converts the response into a <see cref="Cronofy.Account"/>.
            /// </summary>
            /// <returns>
            /// A <see cref="Cronofy.Account"/> based upon the response.
            /// </returns>
            public UserInfo.ServiceAccount ToServiceAccount()
            {
                var serviceAccount = new UserInfo.ServiceAccount
                {
                    ProviderName = this.ProviderName,
                };

                return new UserInfo.ServiceAccount
                {
                    ProviderName = this.ProviderName,
                };
            }
        }

        /// <summary>
        /// Class for the deserialization of an authorization response.
        /// </summary>
        internal sealed class CronofyAuthorization
        {
            /// <summary>
            /// Gets or sets the scope.
            /// </summary>
            /// <value>
            /// The scope of the authorization.
            /// </value>
            [JsonProperty("scope")]
            public string Scope { get; set; }

            /// <summary>
            /// Gets or sets the status of the authorization.
            /// </summary>
            /// <value>
            /// The status of the authorization.
            /// </value>
            [JsonProperty("status")]
            public string Status { get; set; }

            /// <summary>
            /// Gets or sets the delegated scope of the authorization.
            /// </summary>
            /// <value>
            /// The delegated scope of the authorization.
            /// </value>
            [JsonProperty("delegated_scope")]
            public string DelegatedScope { get; set; }

            /// <summary>
            /// Converts the response into a <see cref="Cronofy.Account"/>.
            /// </summary>
            /// <returns>
            /// A <see cref="Cronofy.Account"/> based upon the response.
            /// </returns>
            public UserInfo.Authorization ToAuthorization()
            {
                var authorization = new UserInfo.Authorization
                {
                    Scope = this.Scope,
                    Status = this.Status,
                    DelegatedScope = this.DelegatedScope,
                };

                return new UserInfo.Authorization
                {
                    Scope = this.Scope,
                    Status = this.Status,
                    DelegatedScope = this.DelegatedScope,
                };
            }
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
                var profileSummary = new Calendar.ProfileSummary
                {
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
                    Calendars = this.ProfileCalendars?.Select((pc) =>
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
