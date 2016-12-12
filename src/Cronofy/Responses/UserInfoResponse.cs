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
            };
        }
    }
}
