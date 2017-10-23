namespace Cronofy.Responses
{
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Class to represent a smart invite response
    /// </summary>
    public sealed class SmartInviteResponse
    {
        /// <summary>
        /// The smart invite id.
        /// </summary>
        [JsonProperty("smart_invite_id")]
        public string SmartInviteId { get; set; }

        /// <summary>
        /// The callback URL 
        /// </summary>
        [JsonProperty("callback_url")]
        public string CallbackUrl { get; set; }
        
        /// <summary>
        /// The replies to the invite.
        /// </summary>
        [JsonProperty("replies")]
        public ResponseAttendee[] Replies { get; set; }
        
        /// <summary>
        /// The current state of the primary recipient.
        /// </summary>
        [JsonProperty("recipient")]
        public ResponseAttendee Recipient { get; set; }
        
        /// <summary>
        /// The event details for the invite
        /// </summary>
        [JsonProperty("event")]
        public ReadEventsResponse.EventResponse Event { get; set; }
        
        /// <summary>
        /// The attachment details for the invite
        /// </summary>
        [JsonProperty("attachments")]
        public AttachmentsResponse Attachments { get; set; }

        /// <summary>
        /// A Class to represent attachments.
        /// </summary>
        public sealed class AttachmentsResponse
        {
            /// <summary>
            /// Gets the ICalendar Attachment.
            /// </summary>
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
            /// The email address of the recipient.
            /// </summary>
            [JsonProperty("email")]
            public string Email { get; set; }
            
            /// <summary>
            /// The current status of the recipient.
            /// </summary>
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
                    Email = Email,
                    Status = Status
                };
            }
        }
        
        /// <summary>
        /// Converts this response to a SmartInvite object. 
        /// </summary>
        /// <returns>A Smart invite object.</returns>
        public SmartInvite ToSmartInvite()
        {
            var invite = new SmartInvite();
            invite.SmartInviteId = this.SmartInviteId;
            invite.CallbackUrl = this.CallbackUrl;
            
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
    }
}
