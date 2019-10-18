namespace Cronofy.Requests
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of an Smart Invite request.
    /// </summary>
    public sealed class SmartInviteMultiRecipientRequest
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
        /// Gets or sets the recipients for the invite.
        /// </summary>
        /// <value>
        /// The recipients for the invite.
        /// </value>
        [JsonProperty("recipients")]
        public IEnumerable<InviteRecipient> Recipients { get; set; }

        /// <summary>
        /// Gets or sets the details for the event.
        /// </summary>
        /// <value>
        /// The details for the event.
        /// </value>
        [JsonProperty("event")]
        public SmartInviteEventRequest Event { get; set; }

        /// <summary>
        /// Gets or sets the organizer for the invite.
        /// </summary>
        /// <value>
        /// The organizer for the invite.
        /// </value>
        [JsonProperty("organizer")]
        public InviteOrganizer Organizer { get; set; }

        /// <summary>
        /// Class for the serialization of an Smart Invite request recipient.
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

        /// <summary>
        /// Class for the serialization of an Smart Invite request organizer.
        /// </summary>
        public sealed class InviteOrganizer
        {
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>
            /// The name.
            /// </value>
            [JsonProperty("name")]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the email.
            /// </summary>
            /// <value>
            /// The email.
            /// </value>
            [JsonProperty("email")]
            public string Email { get; set; }
        }
    }
}
