namespace Cronofy
{
    using System.Collections.Generic;

    /// <summary>
    /// Class to represent a smart invite.
    /// </summary>
    public sealed class SmartInvite
    {
        /// <summary>
        /// Gets or sets the method for the smart invite.
        /// </summary>
        /// <value>The method.</value>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the smart invite id.
        /// </summary>
        /// <value>The smart invite id.</value>
        public string SmartInviteId { get; set; }

        /// <summary>
        /// Gets or sets the callback URL.
        /// </summary>
        /// <value>The callback url.</value>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// Gets or sets the replies to the invite.
        /// </summary>
        /// <value>The replies.</value>
        public IEnumerable<Attendee> Replies { get; set; }

        /// <summary>
        /// Gets or sets the current state of the primary recipient.
        /// </summary>
        /// <value>The primary recipient.</value>
        public Attendee Recipient { get; set; }

        /// <summary>
        /// Gets or sets the event details for the invite.
        /// </summary>
        /// <value>The event details.</value>
        public Event Event { get; set; }

        /// <summary>
        /// Gets or sets the attachments for the invite.
        /// </summary>
        /// <value>The attachments.</value>
        public InviteAttachments Attachments { get; set; }

        /// <summary>
        /// Class to represent an attendee.
        /// </summary>
        public sealed class Attendee
        {
            /// <summary>
            /// Gets or sets the email address of the recipient.
            /// </summary>
            /// <value>
            /// The email address.
            /// </value>
            public string Email { get; set; }

            /// <summary>
            /// Gets or sets the current status of the recipient.
            /// </summary>
            /// <value>
            /// The status.
            /// </value>
            public string Status { get; set; }
        }

        /// <summary>
        /// A Class to represent attachments.
        /// </summary>
        public sealed class InviteAttachments
        {
            /// <summary>
            /// Gets or sets the ICalendar Attachment.
            /// </summary>
            /// <value>The icalendar attachment.</value>
            public string ICalendar { get; set; }
        }
    }
}
