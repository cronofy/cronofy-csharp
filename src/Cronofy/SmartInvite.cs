namespace Cronofy
{
    using System.Collections.Generic;

    /// <summary>
    /// Class to represent a smart invite
    /// </summary>
    public sealed class SmartInvite
    {
        /// <summary>
        /// Class to represent an attendee.
        /// </summary>
        public sealed class Attendee
        {
            /// <summary>
            /// The email address of the recipient.
            /// </summary>
            public string Email { get; set; }
            
            /// <summary>
            /// The current status of the recipient.
            /// </summary>
            public string Status { get; set; }
        }

        /// <summary>
        /// A Class to represent attachments.
        /// </summary>
        public sealed class InviteAttachments
        {
            /// <summary>
            /// Gets the ICalendar Attachment.
            /// </summary>
            public string ICalendar { get; set; }
        }

        /// <summary>
        /// The smart invite id.
        /// </summary>
        public string SmartInviteId { get; set; }

        /// <summary>
        /// The callback URL 
        /// </summary>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// The replies to the invite.
        /// </summary>
        public IEnumerable<Attendee> Replies { get; set; }

        /// <summary>
        /// The current state of the primary recipient.
        /// </summary>
        public Attendee Recipient { get; set; }

        /// <summary>
        /// The event details for the invite
        /// </summary>
        public Event Event { get; set; }

        /// <summary>
        /// The attachments for the invite.
        /// </summary>
        public InviteAttachments Attachments { get; set; }
    }
}
