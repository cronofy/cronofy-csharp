using Newtonsoft.Json;

namespace Cronofy.Responses
{
    /// <summary>
    /// Class for the deserialization of a batch response.
    /// </summary>
    public sealed class BatchResponse
    {
        /// <summary>
        /// Gets or sets the responses for the batch entries.
        /// </summary>
        [JsonProperty("batch")]
        public EntryResponse[] Batch { get; set; }

        /// <summary>
        /// Class for the deserialization of a batch entry response.
        /// </summary>
        public sealed class EntryResponse
        {
            /// <summary>
            /// Gets or sets the request the response relates to.
            /// </summary>
            public Requests.BatchRequest.Entry Request { get; set; }

            /// <summary>
            /// Gets or sets the status code of the response.
            /// </summary>
            [JsonProperty("status")]
            public int Status { get; set; }
        }
    }
}
