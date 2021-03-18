namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using Cronofy.Requests;

    /// <summary>
    /// Builder class for
    /// <see cref="ICronofyOAuthClient.CreateInvite(SmartInviteMultiRecipientRequest)"/>
    /// method calls.
    /// </summary>
    public sealed class SmartInviteMultiRecipientRequestBuilder : IBuilder<SmartInviteMultiRecipientRequest>
    {
        /// <summary>
        /// The Smart Invite identifier.
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
        /// The recipients.
        /// </summary>
        private IList<SmartInviteMultiRecipientRequest.InviteRecipient> recipients;

        /// <summary>
        /// The organizer.
        /// </summary>
        private SmartInviteMultiRecipientRequest.InviteOrganizer organizer;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.SmartInviteMultiRecipientRequestBuilder"/> class.
        /// </summary>
        public SmartInviteMultiRecipientRequestBuilder()
        {
            this.recipients = new List<SmartInviteMultiRecipientRequest.InviteRecipient>();
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
        public SmartInviteMultiRecipientRequestBuilder Method(string method)
        {
            Preconditions.NotEmpty("method", method);

            this.method = method;
            return this;
        }

        /// <summary>
        /// Sets the Smart Invite id.
        /// </summary>
        /// <param name="smartInviteId">
        /// The Smart Invite id.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="smartInviteId"/> is empty.
        /// </exception>
        public SmartInviteMultiRecipientRequestBuilder InviteId(string smartInviteId)
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
        public SmartInviteMultiRecipientRequestBuilder CallbackUrl(string callbackUrl)
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
        public SmartInviteMultiRecipientRequestBuilder Event(SmartInviteEventRequest inviteEvent)
        {
            Preconditions.NotNull("inviteEvent", inviteEvent);

            this.inviteEvent = inviteEvent;
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
        public SmartInviteMultiRecipientRequestBuilder AddRecipient(string email)
        {
            Preconditions.NotNull("email", email);

            this.recipients.Add(new SmartInviteMultiRecipientRequest.InviteRecipient
            {
                Email = email,
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
        public SmartInviteMultiRecipientRequestBuilder Organizer(string name)
        {
            Preconditions.NotNull("name", name);

            this.organizer = new SmartInviteMultiRecipientRequest.InviteOrganizer
            {
                Name = name,
            };

            return this;
        }

        /// <summary>
        /// Sets the Organizer details.
        /// </summary>
        /// <param name="name">
        /// The name of the organizer.
        /// </param>
        /// <param name="email">
        /// The email of the organizer. Note that you must set up forwarding if
        /// using this parameter.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="name"/> or <paramref name="email"/> is null.
        /// </exception>
        public SmartInviteMultiRecipientRequestBuilder Organizer(string name, string email)
        {
            Preconditions.NotNull("name", name);
            Preconditions.NotNull("email", email);

            this.organizer = new SmartInviteMultiRecipientRequest.InviteOrganizer
            {
                Name = name,
                Email = email,
            };

            return this;
        }

        /// <inheritdoc/>
        public SmartInviteMultiRecipientRequest Build()
        {
            var request = new SmartInviteMultiRecipientRequest()
            {
                SmartInviteId = this.smartInviteId,
                CallbackUrl = this.callbackUrl,
                Event = this.inviteEvent,
                Recipients = this.recipients,
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
