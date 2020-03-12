namespace Cronofy
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for serialization of an Application Verification request.
    /// </summary>
    public sealed class ApplicationVerificationRequest
    {
        /// <summary>
        /// Gets or sets the redirect URIs used by your application.
        /// </summary>
        /// <value>
        /// The redirect URIs used by your application.
        /// </value>
        [JsonProperty("redirect_uris")]
        public IEnumerable<string> RedirectUris { get; set; }

        /// <summary>
        /// Gets or sets the contact details for your application.
        /// </summary>
        /// <value>
        /// The contact details for your application.
        /// </value>
        [JsonProperty("contact")]
        public ContactDetails Contact { get; set; }

        /// <summary>
        /// Class for serialization of Application Verification Contact Details.
        /// </summary>
        public sealed class ContactDetails
        {
            /// <summary>
            /// Gets or sets the email address to contact if there are problems with the verification process and once the process is completed.
            /// </summary>
            /// <value>
            /// The email address to contact if there are problems with the verification process and once the process is completed.
            /// </value>
            [JsonProperty("email")]
            public string Email { get; set; }

            /// <summary>
            /// Gets or sets a friendly name to use when addressing the contact. Optional.
            /// </summary>
            /// <value>
            /// A friendly name to use when addressing the contact. Optional.
            /// </value>
            [JsonProperty("display_name")]
            public string DisplayName { get; set; }
        }
    }
}
