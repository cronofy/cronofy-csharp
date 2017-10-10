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
