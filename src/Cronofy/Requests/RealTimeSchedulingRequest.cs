namespace Cronofy.Requests
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of an add to calendar request.
    /// </summary>
    public sealed class RealTimeSchedulingRequest : RealTimeSchedulingBaseRequest
    {
        /// <summary>
        /// Gets or sets the availability details for the request.
        /// </summary>
        /// <value>
        /// The availability details for the request.
        /// </value>
        [JsonProperty("availability")]
        public AvailabilityRequest Availability { get; set; }
    }
}
