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

        /// <summary>
        /// Gets or sets the redirect URLs for the request.
        /// </summary>
        /// <value>
        /// The redirect URLs for the request.
        /// </value>
        [JsonProperty("redirect_urls")]
        public RedirectUrlsInfo RedirectUrls { get; set; }

        /// <summary>
        /// Class for serialization of real-time scheduling redirect URLs.
        /// </summary>
        public class RedirectUrlsInfo
        {
            /// <summary>
            /// Gets or sets the Completed URL for the request.
            /// </summary>
            /// <value>
            /// The Completed URL for the request.
            /// </value>
            [JsonProperty("completed_url")]
            public string CompletedUrl { get; set; }
        }

        /// <summary>
        /// Gets or sets the callback URLs for the request.
        /// </summary>
        /// <value>
        /// The callback URLs for the request.
        /// </value>
        [JsonProperty("callback_urls", NullValueHandling=NullValueHandling.Ignore)]
        public CallbackUrlsInfo CallbackUrls { get; set; }

        /// <summary>
        /// Class for the callback URLs parameters.
        /// </summary>
        public class CallbackUrlsInfo
        {
            /// <summary>
            /// Gets or sets the callback URLs for the request.
            /// </summary>
            /// <value>
            /// The callback URLs for the request.
            /// </value>
            [JsonProperty("callback_urls", NullValueHandling=NullValueHandling.Ignore)]
            public CallbackUrlsInfo CallbackUrls { get; set; }
              
            /// <summary>
            /// Gets or sets the Completed URL for the request.
            /// </summary>
            /// <value>
            /// The Completed URL for the request.
            /// </value>
            [JsonProperty("completed_url", NullValueHandling=NullValueHandling.Ignore)]
            public string CallbackCompletedUrl { get; set; }

            /// <summary>
            /// Gets or sets the No Times Suitable URL for the request.
            /// </summary>
            /// <value>
            /// The No Times Suitable URL for the request.
            /// </value>
            [JsonProperty("no_times_suitable_url", NullValueHandling=NullValueHandling.Ignore)]
            public string NoTimesSuitableUrl { get; set; }

            /// <summary>
            /// Gets or sets the No Times Displayed URL for the request.
            /// </summary>
            /// <value>
            /// The No Times Suitable URL for the request.
            /// </value>
            [JsonProperty("no_times_displayed_url", NullValueHandling=NullValueHandling.Ignore)]
            public string NoTimesDisplayedUrl { get; set; }
        }
    }
}
