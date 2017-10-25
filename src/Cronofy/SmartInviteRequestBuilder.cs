namespace Cronofy
{
    using System;
    using Cronofy.Requests;

    /// <summary>
    /// Builder class for
    /// <see cref="CronofyOAuthClient.CreateInvite(SmartInviteRequest)"/>
    /// method calls.
    /// </summary>
    public sealed class SmartInviteRequestBuilder : IBuilder<SmartInviteRequest>
    {
        private string smartInviteId;
        private string callbackUrl;
        private SmartInviteEventRequest inviteEvent;
        private SmartInviteRequest.InviteRecipient recipient;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.SmartInviteRequestBuilder"/> class.
        /// </summary>
        public SmartInviteRequestBuilder()
        {
        }

        /// <summary>
        /// Sets the smart invite id.
        /// </summary>
        /// <param name="smartInviteId">
        /// The smart invite id.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="smartInviteId"/> is empty.
        /// </exception>
        public SmartInviteRequestBuilder InviteId(string smartInviteId)
        {
            Preconditions.NotEmpty("smartInviteId", smartInviteId);

            this.smartInviteId = smartInviteId;
            return this;
        }

        /// <summary>
        /// Sets the callback url.
        /// </summary>
        /// <param name="callbackUrl">
        /// The callback Url for the event, must not be empty.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="callbackUrl"/> is empty.
        /// </exception>
        public SmartInviteRequestBuilder CallbackUrl(string callbackUrl)
        {
            Preconditions.NotEmpty("callbackUrl", callbackUrl);

            this.callbackUrl = callbackUrl;
            return this;
        }

        /// <summary>
        /// Sets the Event details.
        /// </summary>
        /// <param name="inviteEvent">
        /// The event, must not be null.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="inviteEvent"/> is null.
        /// </exception>
        public SmartInviteRequestBuilder Event(SmartInviteEventRequest inviteEvent)
        {
            Preconditions.NotNull("inviteEvent", inviteEvent);

            this.inviteEvent = inviteEvent;
            return this;
        }

        /// <summary>
        /// Sets the Recipient details.
        /// </summary>
        /// <param name="email">
        /// The email address of the recipient
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="email"/> is null.
        /// </exception>
        public SmartInviteRequestBuilder Recipient(string email)
        {
            Preconditions.NotNull("email", email);

            this.recipient = new SmartInviteRequest.InviteRecipient
            {
                Email = email
            };
            return this;
        }

        /// <inheritdoc/>
        public SmartInviteRequest Build()
        {
            var request = new SmartInviteRequest()
            {
                SmartInviteId = this.smartInviteId,
                CallbackUrl = this.callbackUrl,
                Event = this.inviteEvent,
                Recipient = this.recipient,
            };

            return request;
        }
    }
}
