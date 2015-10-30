namespace Cronofy
{
    using System.Collections.Generic;

    /// <summary>
    /// Class representing a HTTP response.
    /// </summary>
    public sealed class HttpResponse
    {
        /// <summary>
        /// Gets or sets the response code.
        /// </summary>
        /// <value>
        /// The response code.
        /// </value>
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the headers of the response.
        /// </summary>
        /// <value>
        /// The headers of the response.
        /// </value>
        public IDictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Gets or sets the body of the response.
        /// </summary>
        /// <value>
        /// The body of the response.
        /// </value>
        public string Body { get; set; }
    }
}
