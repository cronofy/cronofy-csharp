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
        private IBuilder<AddToCalendarRequest.OAuthDetails> oauthBuilder;

        /// <summary>
        /// The event details builder for the request.
        /// </summary>
        private IBuilder<UpsertEventRequest> upsertEventRequestBuilder;

        /// <summary>
        /// Sets the OAuth details of the request.
        /// </summary>
        /// <param name="redirectUri">
        /// The redirect uri for the request's oauth details, must not be blank.
        /// </param>
        /// <param name="scope">
        /// The scope for the request's oauth details, must not be blank.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="AddToCalendarRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="redirectUrl"/> is empty.  
        /// </exception>
        public AddToCalendarRequestBuilder OAuthDetails(string redirectUri, string scope)
        {
            return this.OAuthDetails(redirectUri, scope, null);
        }

        /// <summary>
        /// Sets the OAuth details of the request.
        /// </summary>
        /// <param name="redirectUri">
        /// The redirect uri for the request's oauth details, must not be blank.
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
        public AddToCalendarRequestBuilder OAuthDetails(string redirectUri, string scope, string state)
        {
            Preconditions.NotBlank("redirectUri", redirectUri);

            if (scope != null)
            {
                Preconditions.NotBlank("scope", scope);
            }

            this.oauthBuilder = new BuilderWrapper<AddToCalendarRequest.OAuthDetails>(
                new AddToCalendarRequest.OAuthDetails()
                {
                    RedirectUri = redirectUri,
                    Scope = scope,
                    State = state
                });

            return this;
        }

        /// <summary>
        /// Sets the OAuth details of the request.
        /// </summary>
        /// <param name="oauthBuilder">
        /// The builder for <see cref="AddToCalendarRequest.OAuthDetails"/>.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="AddToCalendarRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="oauthBuilder"/> is empty.  
        /// </exception>
        public AddToCalendarRequestBuilder OAuthDetails(IBuilder<AddToCalendarRequest.OAuthDetails> oauthBuilder)
        {
            Preconditions.NotNull("oauthBuilder", oauthBuilder);

            this.oauthBuilder = oauthBuilder;

            return this;
        }

        /// <summary>
        /// Sets the event details of the request.
        /// </summary>
        /// <param name="upsertEventRequestBuilder">
        /// The event details builder for the request, must not be null.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="AddToCalendarRequestBuilder"/> 
        /// </returns>
        public AddToCalendarRequestBuilder UpsertEventRequestBuilder(UpsertEventRequestBuilder upsertEventRequestBuilder)
        {
            Preconditions.NotNull("upsertEventRequestBuilder", upsertEventRequestBuilder);

            this.upsertEventRequestBuilder = upsertEventRequestBuilder;

            return this;
        }

        /// <summary>
        /// Sets the event details of the request.
        /// </summary>
        /// <param name="upsertEventRequest">
        /// The event details for the request, must not be null.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="AddToCalendarRequestBuilder"/>.
        /// </returns>
        public AddToCalendarRequestBuilder UpsertEventRequest(UpsertEventRequest upsertEventRequest)
        {
            Preconditions.NotNull("event", upsertEventRequest);

            this.upsertEventRequestBuilder = new BuilderWrapper<UpsertEventRequest>(upsertEventRequest);

            return this;
        }

        /// <inheritdoc />
        public AddToCalendarRequest Build()
        {
            return new AddToCalendarRequest
            {
                OAuth = this.oauthBuilder.Build(),
                UpsertEventRequest = this.upsertEventRequestBuilder.Build(),
            };
        }
    }
}
