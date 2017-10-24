namespace Cronofy.Requests
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of a create calendar request.
    /// </summary>
    public sealed class CreateCalendarRequest
    {
        /// <summary>
        /// Gets or sets the profile ID for the request.
        /// </summary>
        /// <value>
        /// The profile ID for the request.
        /// </value>
        [JsonProperty("profile_id")]
        public string ProfileId { get; set; }

        /// <summary>
        /// Gets or sets the name for the calendar.
        /// </summary>
        /// <value>
        /// The name for the calendar.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the color for the calendar.
        /// </summary>
        /// <value>
        /// The color for the calendar.
        /// </value>
        [JsonProperty("color")]
        public string Color { get; set; }
    }
}
