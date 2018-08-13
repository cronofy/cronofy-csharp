namespace Cronofy.Requests
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of an real time sequencing request.
    /// </summary>
    public sealed class RealTimeSequencingRequest : RealTimeSchedulingBaseRequest
    {
        /// <summary>
        /// Gets or sets the sequence details for the request.
        /// </summary>
        /// <value>
        /// The sequence details for the request.
        /// </value>
        [JsonProperty("availablity")]
        public SequencedAvailabilityRequest Availability { get; set; }
    }
}
