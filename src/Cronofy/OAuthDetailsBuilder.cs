namespace Cronofy
{
    using System;
    using Cronofy.Requests;

    /// <summary>
    /// Builder for OAuthDetailsBuilder.
    /// </summary>
    public class OAuthDetailsBuilder : IBuilder<AddToCalendarRequest.OAuthDetails>
    {
        /// <summary>
        /// The redirectUri to set.
        /// </summary>
        private string redirectUri;

        /// <summary>
        /// The scopes to set.
        /// </summary>
        private string scope;

        /// <summary>
        /// The state to set.
        /// </summary>
        private string state;

        /// <summary>
        /// Sets the redirect Uri of the request.
        /// </summary>
        /// <param name="redirectUri">
        /// The redirectUri for the request, must not be blank.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="OAuthDetailsBuilder"/>.
        /// </returns>
        public IBuilder<AddToCalendarRequest.OAuthDetails> RedirectUri(string redirectUri)
        {
            Preconditions.NotBlank("redirectUri", redirectUri);
            this.redirectUri = redirectUri;
            return this;
        }

        /// <summary>
        /// Sets the scope of the request.
        /// </summary>
        /// <param name="scope">
        /// The scope for the request, must not be blank.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="OAuthDetailsBuilder"/>.
        /// </returns>
        public IBuilder<AddToCalendarRequest.OAuthDetails> Scope(string scope)
        {
            Preconditions.NotBlank("scope", scope);
            this.scope = scope;
            return this;
        }

        /// <summary>
        /// Sets the state of the request.
        /// </summary>
        /// <param name="state">
        /// The state for the request, must not be blank.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="OAuthDetailsBuilder"/>.
        /// </returns>
        public IBuilder<AddToCalendarRequest.OAuthDetails> State(string state)
        {
            Preconditions.NotBlank("state", state);
            this.state = state;
            return this;
        }

        /// <inheritdoc />
        public AddToCalendarRequest.OAuthDetails Build()
        {
            return new AddToCalendarRequest.OAuthDetails
            {
                RedirectUri = this.redirectUri,
                Scope = this.scope,
                State = this.state,
            };
        }
    }
}
