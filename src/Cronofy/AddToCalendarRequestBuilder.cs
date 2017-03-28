namespace Cronofy
{
    using System;
    using Cronofy.Requests;

    /// <summary>
    /// Class to build an add to calendar request.
    /// </summary>
    public sealed class AddToCalendarRequestBuilder : IBuilder<AddToCalendarRequest>
    {
        /// <summary>
        /// The oauth details for the request.
        /// </summary>
        private AddToCalendarRequest.OAuthDetails oauth;

        /// <summary>
        /// The event details for the request.
        /// </summary>
        private UpsertEventRequest @event;

        /// <summary>
        /// Sets the OAuth details of the request.
        /// </summary>
        /// <param name="redirectUrl">
        /// The redirect url for the request's oauth details, must not be blank.
        /// </param>
        /// <param name="scope">
        /// The scope for the request's oauth details, must not be blank.
        /// </param>
        /// <param name="state">
        /// The state for the request's oauth details.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="AddToCalendarRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="redirectUrl"/> or <paramref name="scope"/> are empty.  
        /// </exception>
        public AddToCalendarRequestBuilder OAuthDetails(string redirectUrl, string scope, string state = null)
        {
            Preconditions.NotBlank("redirectUrl", redirectUrl);
            Preconditions.NotBlank("scope", scope);

            this.oauth = new AddToCalendarRequest.OAuthDetails
            {
                RedirectUrl = redirectUrl,
                Scope = scope,
                State = state
            };

            return this;
        }

        /// <summary>
        /// Sets the event details of the request.
        /// </summary>
        /// <param name="event">
        /// The event details for the request, must not be null.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="AddToCalendarRequestBuilder"/>.
        /// </returns>
        public AddToCalendarRequestBuilder Event(UpsertEventRequest @event)
        {
            Preconditions.NotNull("event", @event);

            this.@event = @event;

            return this;
        }

        /// <inheritdoc />
        public AddToCalendarRequest Build()
        {
            return new AddToCalendarRequest
            {
                OAuth = this.oauth,
                Event = this.@event
            };
        }
    }
}
