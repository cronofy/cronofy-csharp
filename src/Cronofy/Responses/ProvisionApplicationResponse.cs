namespace Cronofy.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for serialization of Application Provisioning responses.
    /// </summary>
    public class ProvisionApplicationResponse
    {
        /// <summary>
        /// Gets or sets the OAuth Client details for your application.
        /// </summary>
        /// <value>
        /// The OAuth Client details for your application.
        /// </value>
        [JsonProperty("oauth_client")]
        public OAuthClientDetails OAuthClient { get; set; }

        /// <summary>
        /// Class for serialization of OAuth Client Details for Application Provisioning responses.
        /// </summary>
        public class OAuthClientDetails
        {
            /// <summary>
            /// Gets or sets the Client ID of your new application.
            /// </summary>
            /// <value>
            /// The Client ID of your new application.
            /// </value>
            [JsonProperty("client_id")]
            public string ClientId { get; set; }

            /// <summary>
            /// Gets or sets the Client Secret of your new application.
            /// </summary>
            /// <value>
            /// The Client Secret of your new application.
            /// </value>
            [JsonProperty("client_secret")]
            public string ClientSecret { get; set; }
        }
    }
}
