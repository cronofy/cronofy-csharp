namespace Cronofy.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of an real time sequencing response.
    /// </summary>
    internal sealed class RealTimeSequencingResponse
    {
        /// <summary>
        /// Gets or sets the Url of the response.
        /// </summary>
        /// <value>
        /// The URL for the user to visit in order to add the event to their calendar.
        /// </value>
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}