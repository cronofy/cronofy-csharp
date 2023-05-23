namespace Cronofy.Responses
{
    using System;
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
                //CalendarIntegratedConferencingAvailable = this.CalendarIntegratedConferencingAvailable,
                Profiles = this.Profiles,
            };

            UserInfo.ProviderName = this.ProviderName.ToProfileCalendar();
        }
        public sealed class CronofyData {

            /// <summary>
            /// Gets or sets the profiles.
            /// </summary>
            /// <value>The profiles.</value>
            [JsonProperty("profiles")]
            public Profile[] Profiles { get; set; }


        }

        public sealed class Profile {

            /// <summary>
            /// Gets or sets the calendar provider name.
            /// </summary>
            [JsonProperty("provider_name")]
            public string ProviderName { get; set; }

            /// <summary>
            /// Gets or sets the profile calendars.
            /// </summary>
            /// <value>The profile calendars.</value>
            [JsonProperty("profile_calendars")]
            public ProfileCalendar[] ProfileCalendars { get; set; }

        }

        public sealed class ProfileCalendar {

            /// <summary>
            /// Gets or sets a value indicating whether integrated conferencing is available for a calendar.
            /// </summary>
            [JsonProperty("calendar_integrated_conferencing_available")]
            public bool CalendarIntegratedConferencingAvailable { get; set; }


            /// <summary>
            /// Converts this response to a profile calendar object.
            /// </summary>
            /// <returns>
            /// A Profile Calendar object.
            /// </returns>
            public UserInfo.ProfileCalendar ToProfileCalendar()
            {
                return new UserInfo.ProfileCalendars
                {
                    ProviderName = this.ProviderName,
                };
            }

        }
    }
}
