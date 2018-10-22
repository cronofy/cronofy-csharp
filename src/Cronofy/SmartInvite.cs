namespace Cronofy
{
    using System.Collections.Generic;

    /// <summary>
    /// Class to represent a Smart Invite.
    /// </summary>
    public sealed class SmartInvite
    {
        /// <summary>
        /// Gets or sets the method for the Smart Invite.
        /// </summary>
        /// <value>The method.</value>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the Smart Invite id.
        /// </summary>
        /// <value>The Smart Invite id.</value>
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
        /// Gets or sets the current state of the recipients.
        /// </summary>
        /// <value>The recipients.</value>
        public IEnumerable<Attendee> Recipients { get; set; }

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

            /// <summary>
            /// Gets or sets the current comment of the recipient.
            /// </summary>
            /// <value>
            /// The status.
            /// </value>
            public string Comment { get; set; }

            /// <summary>
            /// Gets or sets the counter proposal.
            /// </summary>
            /// <value>The proposal.</value>
            public Proposal Proposal { get; set; }
        }

        /// <summary>
        /// Class to represent an attendee.
        /// </summary>
        public sealed class Proposal
        {
            /// <summary>
            /// Gets or sets the start time.
            /// </summary>
            /// <value>The start.</value>
            public EventTime Start { get; set; }

            /// <summary>
            /// Gets or sets the end time.
            /// </summary>
            /// <value>The end.</value>
            public EventTime End { get; set; }
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
