namespace Cronofy.Requests
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of an smart invite request.
    /// </summary>
    public sealed class SmartInviteRequest
    {
        /// <summary>
        /// Gets or sets the method for the invite.
        /// </summary>
        /// <value>
        /// The method of the invite.
        /// </value>
        [JsonProperty("method")]
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the Id for the invite.
        /// </summary>
        /// <value>
        /// The Id of the invite.
        /// </value>
        [JsonProperty("smart_invite_id")]
        public string SmartInviteId { get; set; }

        /// <summary>
        /// Gets or sets the callback url for notifications.
        /// </summary>
        /// <value>
        /// The callback url for notifications.
        /// </value>
        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }

        /// <summary>
        /// Gets or sets the recipient for the invite.
        /// </summary>
        /// <value>
        /// The recipient for the invite.
        /// </value>
        [JsonProperty("recipient")]
        public InviteRecipient Recipient { get; set; }

        /// <summary>
        /// Gets or sets the details for the event.
        /// </summary>
        /// <value>
        /// The details for the event.
        /// </value>
        [JsonProperty("event")]
        public SmartInviteEventRequest Event { get; set; }

        /// <summary>
        /// Class for the serialization of an smart invite request recipient.
        /// </summary>
        public sealed class InviteRecipient
        {
            /// <summary>
            /// Gets or sets the email address.
            /// </summary>
            /// <value>
            /// The email address.
            /// </value>
            [JsonProperty("email")]
            public string Email { get; set; }
        }
    }
}
