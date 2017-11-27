namespace Cronofy.Responses
{
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Class to represent a smart invite response.
    /// </summary>
    internal sealed class SmartInviteResponse
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
        /// Gets or sets the smart invite identifier.
        /// </summary>
        /// <value>The smart invite identifier.</value>
        [JsonProperty("smart_invite_id")]
        public string SmartInviteId { get; set; }

        /// <summary>
        /// Gets or sets the callback URL.
        /// </summary>
        /// <value>The callback URL.</value>
        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }

        /// <summary>
        /// Gets or sets the replies.
        /// </summary>
        /// <value>The replies.</value>
        [JsonProperty("replies")]
        public ResponseAttendee[] Replies { get; set; }

        /// <summary>
        /// Gets or sets the recipient.
        /// </summary>
        /// <value>The recipient.</value>
        [JsonProperty("recipient")]
        public ResponseAttendee Recipient { get; set; }

        /// <summary>
        /// Gets or sets the event.
        /// </summary>
        /// <value>The event.</value>
        [JsonProperty("event")]
        public ReadEventsResponse.EventResponse Event { get; set; }

        /// <summary>
        /// Gets or sets the attachments.
        /// </summary>
        /// <value>The attachments.</value>
        [JsonProperty("attachments")]
        public AttachmentsResponse Attachments { get; set; }

        /// <summary>
        /// Converts this response to a SmartInvite object.
        /// </summary>
        /// <returns>A Smart invite object.</returns>
        public SmartInvite ToSmartInvite()
        {
            var invite = new SmartInvite();
            invite.SmartInviteId = this.SmartInviteId;
            invite.CallbackUrl = this.CallbackUrl;
            invite.Method = this.Method;

            if (this.Replies != null)
            {
                invite.Replies = this.Replies.Select(t => t.ToAttendee());
            }
            else
            {
                invite.Replies = Enumerable.Empty<SmartInvite.Attendee>();
            }

            invite.Recipient = this.Recipient.ToAttendee();
            invite.Event = this.Event.ToEvent();
            invite.Attachments = this.Attachments.ToAttachments();

            return invite;
        }

        /// <summary>
        /// A Class to represent attachments.
        /// </summary>
        public sealed class AttachmentsResponse
        {
            /// <summary>
            /// Gets or sets the ICalendar attachment.
            /// </summary>
            /// <value>The ICalendar attachment.</value>
            [JsonProperty("icalendar")]
            public string ICalendar { get; set; }

            /// <summary>
            /// Converts this response to an attachments object.
            /// </summary>
            /// <returns>
            /// A Attachments object.
            /// </returns>
            public SmartInvite.InviteAttachments ToAttachments()
            {
                return new SmartInvite.InviteAttachments()
                {
                    ICalendar = this.ICalendar
                };
            }
        }

        /// <summary>
        /// Class to represent the attendee.
        /// </summary>
        public sealed class ResponseAttendee
        {
            /// <summary>
            /// Gets or sets the email.
            /// </summary>
            /// <value>The email.</value>
            [JsonProperty("email")]
            public string Email { get; set; }

            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>The status.</value>
            [JsonProperty("status")]
            public string Status { get; set; }

            /// <summary>
            /// Converts this response to an attendee object.
            /// </summary>
            /// <returns>
            /// A Attendee object.
            /// </returns>
            public SmartInvite.Attendee ToAttendee()
            {
                return new SmartInvite.Attendee
                {
                    Email = this.Email,
                    Status = this.Status
                };
            }
        }
    }
}
