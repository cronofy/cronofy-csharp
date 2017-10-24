namespace Cronofy.Requests
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of scheduling formatting.
    /// </summary>
    public sealed class SchedulingFormatting
    {
        /// <summary>
        /// Gets or sets the hour format for the request.
        /// </summary>
        /// <value>
        /// The hour format for the request.
        /// </value>
        [JsonProperty("hour_format")]
        public string HourFormat { get; set; }
    }
}
