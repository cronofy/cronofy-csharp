using System.Collections.Generic;
using System.Linq;
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
        /// Gets whether the response contains entries with errors.
        /// </summary>
        public bool HasErrors
        {
            get { return this.Errors.Any(); }
        }

        /// <summary>
        /// Gets the entry responses that contain errors.
        /// </summary>
        public IList<EntryResponse> Errors
        {
            get { return this.Batch.Where(entry => entry.Status / 100 != 2).ToList(); }
        }

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

            /// <inheritdoc />
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj is EntryResponse && Equals((EntryResponse) obj);
            }

            /// <inheritdoc />
            public override int GetHashCode()
            {
                unchecked
                {
                    return ((this.Request != null ? this.Request.GetHashCode() : 0) * 397) ^ this.Status;
                }
            }

            private bool Equals(EntryResponse other)
            {
                return this.Status == other.Status && Equals(this.Request, other.Request);
            }
        }
    }
}
