namespace Cronofy.Responses
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Class to represent a Smart Invite response.
    /// </summary>
    internal sealed class SmartInviteMultiRecipientResponse
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
        /// Gets or sets the Smart Invite identifier.
        /// </summary>
        /// <value>The Smart Invite identifier.</value>
        [JsonProperty("smart_invite_id")]
        public string SmartInviteId { get; set; }

        /// <summary>
        /// Gets or sets the callback URL.
        /// </summary>
        /// <value>The callback URL.</value>
        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }

        /// <summary>
        /// Gets or sets the recipients list.
        /// </summary>
        /// <value>The recipient.</value>
        [JsonProperty("recipients")]
        public SmartInviteResponse.ResponseAttendee[] Recipients { get; set; }

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
        public SmartInviteResponse.AttachmentsResponse Attachments { get; set; }

        /// <summary>
        /// Converts this response to a SmartInviteMultiRecipient object.
        /// </summary>
        /// <returns>A Smart invite object.</returns>
        public SmartInviteMultiRecipient ToSmartInvite()
        {
            var invite = new SmartInviteMultiRecipient();

            invite.SmartInviteId = this.SmartInviteId;
            invite.CallbackUrl = this.CallbackUrl;
            invite.Method = this.Method;

            if (this.Recipients != null)
            {
                invite.Recipients = this.Recipients.Select(t => t.ToAttendee());
            }
            else
            {
                invite.Recipients = Enumerable.Empty<SmartInvite.Attendee>();
            }

            invite.Event = this.Event.ToEvent();
            invite.Attachments = this.Attachments.ToAttachments();

            return invite;
        }
    }
}
