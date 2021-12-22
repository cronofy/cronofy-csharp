namespace Cronofy
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for providing options to an Enterprise Connect authorize user
    /// request.
    /// </summary>
    public sealed class EnterpriseConnectAuthorizeUserOptions
    {
        /// <summary>
        /// Gets or sets the email of the user to be authorized.
        /// </summary>
        /// <value>
        /// The email to be authorized.
        /// </value>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the URL that will receive the callback for the result
        /// of the authorization attempt.
        /// </summary>
        /// <value>
        /// The URL that will receive the callback for the result of the
        /// authorization attempt.
        /// </value>
        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }

        /// <summary>
        /// Gets or sets the scope to request authorization for.
        /// </summary>
        /// <value>
        /// The scope to request authorization for.
        /// </value>
        [JsonProperty("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// Gets or sets the optional state to pass through the authorization
        /// flow, which will be returned to the callback URL unmodified.
        /// </summary>
        /// <value>
        /// The optional state to pass through the authorization flow, which
        /// will be returned to the callback URL unmodified.
        /// </value>
        [JsonProperty("state")]
        public string State { get; set; }
    }
}
