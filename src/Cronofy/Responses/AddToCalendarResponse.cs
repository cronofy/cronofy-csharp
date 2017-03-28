namespace Cronofy.Responses
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of an add to calendar response.
    /// </summary>
    internal sealed class AddToCalendarResponse
    {
        /// <summary>
        /// Gets or sets the OAuthUrl of the response.
        /// </summary>
        /// <value>
        /// The OAuth URL for the user to visit in order to add the event to their calendar.
        /// </value>
        [JsonProperty("oauth_url")]
        public string OAuthUrl { get; set; }
    }
}
