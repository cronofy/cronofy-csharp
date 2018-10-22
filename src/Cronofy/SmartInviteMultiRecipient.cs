namespace Cronofy
{
    using System.Collections.Generic;

    /// <summary>
    /// Class to represent a Smart Invite.
    /// </summary>
    public sealed class SmartInviteMultiRecipient
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
        /// Gets or sets the current state of the recipients.
        /// </summary>
        /// <value>The recipients.</value>
        public IEnumerable<SmartInvite.Attendee> Recipients { get; set; }

        /// <summary>
        /// Gets or sets the event details for the invite.
        /// </summary>
        /// <value>The event details.</value>
        public Event Event { get; set; }

        /// <summary>
        /// Gets or sets the attachments for the invite.
        /// </summary>
        /// <value>The attachments.</value>
        public SmartInvite.InviteAttachments Attachments { get; set; }
    }
}
