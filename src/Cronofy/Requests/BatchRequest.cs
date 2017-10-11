using System.Web;

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

            /// <inheritdoc />
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;

                var entry = obj as Entry;

                return entry != null && this.Equals(entry);
            }

            /// <inheritdoc />
            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (this.Method != null ? this.Method.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.RelativeUrl != null ? this.RelativeUrl.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (this.Data != null ? this.Data.GetHashCode() : 0);
                    return hashCode;
                }
            }

            /// <summary>
            /// Determines whether the specified <see cref="Entry"/> is equal to
            /// the current <see cref="Entry"/>.
            /// </summary>
            /// <param name="other">
            /// The <see cref="Entry"/> to compare with the current
            /// <see cref="Entry"/>.
            /// </param>
            /// <returns>
            /// <c>true</c> if the specified <see cref="Entry"/> is equal to the
            /// current <see cref="Entry"/>; otherwise, <c>false</c>.
            /// </returns>
            public bool Equals(Entry other)
            {
                return this.Method == other.Method
                    && this.RelativeUrl == other.RelativeUrl
                    && this.Data == null ? other.Data == null : this.Data.Equals(other.Data);
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return $"{nameof(this.Method)}: {this.Method}, {nameof(this.RelativeUrl)}: {this.RelativeUrl}, {nameof(this.Data)}: {this.Data}";
            }
        }

        /// <summary>
        /// Builder for <see cref="Entry"/> instances.
        /// </summary>
        public sealed class EntryBuilder : IBuilder<Entry>
        {
            private string method;
            private string relativeUrl;
            private object data;

            /// <summary>
            /// Sets the method of the builder.
            /// </summary>
            /// <param name="method">
            /// The method for the entry.
            /// </param>
            /// <returns>
            /// A reference to the modified builder.
            /// </returns>
            public EntryBuilder Method(string method)
            {
                this.method = method;
                return this;
            }

            /// <summary>
            /// Sets relative URL of the builder.
            /// </summary>
            /// <param name="relativeUrl">
            /// The relative URL for the entry.
            /// </param>
            /// <returns>
            /// A reference to the modified builder.
            /// </returns>
            public EntryBuilder RelativeUrl(string relativeUrl)
            {
                this.relativeUrl = relativeUrl;
                return this;
            }

            /// <summary>
            /// Sets relative URL of the builder.
            /// </summary>
            /// <param name="relativeUrl">
            /// The relative URL for the entry.
            /// </param>
            /// <param name="args">
            /// Arguments to apply to the format string.
            /// </param>
            /// <returns>
            /// A reference to the modified builder.
            /// </returns>
            public EntryBuilder RelativeUrlFormat(string relativeUrl, params object[] args)
            {
                this.relativeUrl = string.Format(relativeUrl, args);
                return this;
            }

            /// <summary>
            /// Sets the data of the builder.
            /// </summary>
            /// <param name="data">
            /// The data for the entry.
            /// </param>
            /// <returns>
            /// A reference to the modified builder.
            /// </returns>
            public EntryBuilder Data(object data)
            {
                this.data = data;
                return this;
            }

            /// <summary>
            /// Sets the data of the builder.
            /// </summary>
            /// <param name="dataBuilder">
            /// The builder for the data for the entry.
            /// </param>
            /// <returns>
            /// A reference to the modified builder.
            /// </returns>
            public EntryBuilder Data<T>(IBuilder<T> dataBuilder)
            {
                this.data = dataBuilder.Build();
                return this;
            }

            /// <inheritdoc />
            public Entry Build()
            {
                return new Entry
                {
                    Method = this.method,
                    RelativeUrl = this.relativeUrl,
                    Data = this.data,
                };
            }
        }
    }
}
