namespace Cronofy.Requests
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of an smart invite cancel request.
    /// </summary>
    public sealed class SmartInviteCancelRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Cronofy.Requests.SmartInviteCancelRequest"/> class.
        /// </summary>
        /// <param name="smartInviteId">The smart invite identifier.</param>
        /// <param name="recipientEmail">The recipient email.</param>
        public SmartInviteCancelRequest(string smartInviteId, string recipientEmail)
        {
            this.Method = "cancel";
            this.SmartInviteId = smartInviteId;
            this.Recipient = new InviteRecipient()
            {
                Email = recipientEmail
            };
        }

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
        /// Gets or sets the recipient for the invite.
        /// </summary>
        /// <value>
        /// The recipient for the invite.
        /// </value>
        [JsonProperty("recipient")]
        public InviteRecipient Recipient { get; set; }

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
