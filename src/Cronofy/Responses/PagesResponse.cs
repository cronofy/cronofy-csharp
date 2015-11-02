namespace Cronofy.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of the paging information.
    /// </summary>
    internal sealed class PagesResponse
    {
        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>
        /// The current page.
        /// </value>
        [JsonProperty("current")]
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the total number of pages.
        /// </summary>
        /// <value>
        /// The total pages.
        /// </value>
        [JsonProperty("total")]
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the URL of the next page of events, if applicable.
        /// </summary>
        /// <value>
        /// The URL of the next page of events, <c>null</c> if this is the
        /// last page of results.
        /// </value>
        [JsonProperty("next_page")]
        public string NextPageUrl { get; set; }
    }
}
