namespace Cronofy.Requests
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of a batch request.
    /// </summary>
    public sealed class BatchRequest
    {
        /// <summary>
        /// Gets or sets the entries of the batch request.
        /// </summary>
        [JsonProperty("batch")]
        public Entry[] Batch { get; set; }

        /// <summary>
        /// Class for the serialization of an entry of a batch request.
        /// </summary>
        public sealed class Entry
        {
            /// <summary>
            /// Gets or sets the method of the batch entry.
            /// </summary>
            [JsonProperty("method")]
            public string Method { get; set; }

            /// <summary>
            /// Gets or sets the relative URL of the batch entry.
            /// </summary>
            [JsonProperty("relative_url")]
            public string RelativeUrl { get; set; }

            /// <summary>
            /// Gets or sets the data of the batch entry.
            /// </summary>
            [JsonProperty("data")]
            public object Data { get; set; }
        }
    }
}
