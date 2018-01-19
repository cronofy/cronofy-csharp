namespace Cronofy.Requests
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of an Application Calendar request.
    /// </summary>
    internal sealed class ApplicationCalendarRequest
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
        /// Gets or sets application calendar id.
        /// </summary>
        /// <value>
        /// The application calendar id.
        /// </value>
        [JsonProperty("application_calendar_id")]
        public string ApplicationCalendarId { get; set; }
    }
}
