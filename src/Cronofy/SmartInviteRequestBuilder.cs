namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using Cronofy.Requests;

    /// <summary>
    /// Builder class for
    /// <see cref="CronofyOAuthClient.CreateInvite(SmartInviteRequest)"/>
    /// method calls.
    /// </summary>
    public sealed class SmartInviteRequestBuilder : IBuilder<SmartInviteRequest>
    {
        /// <summary>
        /// The smart invite identifier.
        /// </summary>
        private string smartInviteId;

        /// <summary>
        /// The method.
        /// </summary>
        private string method;

        /// <summary>
        /// The callback URL.
        /// </summary>
        private string callbackUrl;

        /// <summary>
        /// The invite event.
        /// </summary>
        private SmartInviteEventRequest inviteEvent;

        /// <summary>
        /// The recipient.
        /// </summary>
        private SmartInviteRequest.InviteRecipient recipient;

        /// <summary>
        /// The recipients.
        /// </summary>
        private IList<SmartInviteRequest.InviteRecipient> recipients;

        /// <summary>
        /// The organizer.
        /// </summary>
        private SmartInviteRequest.InviteOrganizer organizer;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.SmartInviteRequestBuilder"/> class.
        /// </summary>
        public SmartInviteRequestBuilder()
        {
        }

        /// <summary>
        /// Sets the method for the invite.
        /// </summary>
        /// <param name="method">
        /// The method for the invite, must not be empty.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="method"/> is empty.
        /// </exception>
        public SmartInviteRequestBuilder Method(string method)
        {
            Preconditions.NotEmpty("method", method);

            this.method = method;
            return this;
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
        /// The email address of the recipient.
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

        /// <summary>
        /// Add a new recipient to the recipients list.
        /// </summary>
        /// <param name="email">
        /// The email address of the recipient.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="email"/> is null.
        /// </exception>
        public SmartInviteRequestBuilder AddRecipient(string email)
        {
            Preconditions.NotNull("email", email);

            if (this.recipients == null)
            {
                this.recipients = new List<SmartInviteRequest.InviteRecipient>();
            }

            this.recipients.Add(new SmartInviteRequest.InviteRecipient
            {
                Email = email
            });

            return this;
        }

        /// <summary>
        /// Sets the Organizer details.
        /// </summary>
        /// <param name="name">
        /// The name of the organizer.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="name"/> is null.
        /// </exception>
        public SmartInviteRequestBuilder Organizer(string name)
        {
            Preconditions.NotNull("name", name);

            this.organizer = new SmartInviteRequest.InviteOrganizer
            {
                Name = name
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
                Recipients = this.recipients
            };

            if (this.organizer != null)
            {
                request.Organizer = this.organizer;
            }

            if (string.IsNullOrEmpty(this.method) == false)
            {
                request.Method = this.method;
            }

            return request;
        }
    }
}
