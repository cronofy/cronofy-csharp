namespace Cronofy.Responses
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of a batch response.
    /// </summary>
    public sealed class BatchResponse
    {
        /// <summary>
        /// Gets or sets the responses for the batch entries.
        /// </summary>
        /// <value>
        /// The responses for the batch.
        /// </value>
        [JsonProperty("batch")]
        public EntryResponse[] Batch { get; set; }

        /// <summary>
        /// Gets a value indicating whether the response contains entries with
        /// errors.
        /// </summary>
        /// <value>
        /// Whether the response contains entries with errors.
        /// </value>
        public bool HasErrors
        {
            get { return this.Errors.Any(); }
        }

        /// <summary>
        /// Gets the entry responses that contain errors.
        /// </summary>
        /// <value>
        /// The entry responses that contain errors.
        /// </value>
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
            /// <value>
            /// The request the response relates to.
            /// </value>
            public Requests.BatchRequest.Entry Request { get; set; }

            /// <summary>
            /// Gets or sets the status code of the response.
            /// </summary>
            /// <value>
            /// The status code of the response.
            /// </value>
            [JsonProperty("status")]
            public int Status { get; set; }

            /// <inheritdoc />
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }

                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                var entry = obj as EntryResponse;

                return entry != null && this.Equals(entry);
            }

            /// <inheritdoc />
            public override int GetHashCode()
            {
                unchecked
                {
                    return ((this.Request != null ? this.Request.GetHashCode() : 0) * 397) ^ this.Status;
                }
            }

            /// <summary>
            /// Determines whether the specified <see cref="EntryResponse"/> is
            /// equal to the current <see cref="EntryResponse"/>.
            /// </summary>
            /// <param name="other">
            /// The <see cref="EntryResponse"/> to compare with the current
            /// <see cref="EntryResponse"/>.
            /// </param>
            /// <returns>
            /// <c>true</c> if the specified <see cref="EntryResponse"/> is
            /// equal to the current <see cref="EntryResponse"/>; otherwise,
            /// <c>false</c>.
            /// </returns>
            private bool Equals(EntryResponse other)
            {
                return this.Status == other.Status && object.Equals(this.Request, other.Request);
            }
        }
    }
}
