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
        /// Gets or sets a value indicating whether integrated conferencing is available for an account.
        /// </summary>
        [JsonProperty("cronofy.data.profiles.profile_calendars.calendar_integrated_conferencing_available")]
        public bool CalendarIntegratedConferencingAvailable { get; set; }

        /// <summary>
        /// Gets or sets the calendar provider name.
        /// </summary>
        [JsonProperty("cronofy.data.profiles.provider_name")]
        public string ProviderName { get; set; }

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
                CalendarIntegratedConferencingAvailable = this.CalendarIntegratedConferencingAvailable,
                ProviderName = this.ProviderName,
            };
        }
    }
}
