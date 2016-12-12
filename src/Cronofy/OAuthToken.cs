using System;
namespace Cronofy
{
    using System.Linq;

    /// <summary>
    /// Class representing the details of an OAuth token.
    /// </summary>
    public sealed class OAuthToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Cronofy.OAuthToken"/>
        /// class.
        /// </summary>
        /// <param name="accessToken">
        /// The access token for the OAuth authorization, must not be empty.
        /// </param>
        /// <param name="refreshToken">
        /// The refresh token for the OAuth authorization, must not be empty.
        /// </param>
        /// <param name="expiresIn">
        /// The number of seconds until the <paramref name="accessToken"/>
        /// expires, must not be negative.
        /// </param>
        /// <param name="scope">
        /// The scope of the OAuth authorization, must not be null.
        /// </param>
        public OAuthToken(string accessToken, string refreshToken, int expiresIn, string[] scope)
        {
            Preconditions.NotEmpty("accessToken", accessToken);
            Preconditions.NotEmpty("refreshToken", refreshToken);
            Preconditions.NotNegative("expiresIn", expiresIn);
            Preconditions.NotNull("scope", scope);

            this.AccessToken = accessToken;
            this.RefreshToken = refreshToken;
            this.ExpiresIn = expiresIn;
            this.Scope = scope;
        }

        /// <summary>
        /// Gets the access token for the OAuth authorization.
        /// </summary>
        /// <value>
        /// The access token for the OAuth authorization.
        /// </value>
        public string AccessToken { get; private set; }

        /// <summary>
        /// Gets the refresh token for the OAuth authorization.
        /// </summary>
        /// <value>
        /// The refresh token for the OAuth authorization.
        /// </value>
        public string RefreshToken { get; private set; }

        /// <summary>
        /// Gets the number of seconds until the <see cref="AccessToken"/>
        /// expires.
        /// </summary>
        /// <value>
        /// The number of seconds until the <see cref="AccessToken"/> expires.
        /// </value>
        public int ExpiresIn { get; private set; }

        /// <summary>
        /// Gets the scope of the OAuth authorization.
        /// </summary>
        /// <value>
        /// The scope of the OAuth authorization.
        /// </value>
        public string[] Scope { get; private set; }

        /// <summary>
        /// Gets the account ID of the OAuth authorization.
        /// </summary>
        /// <value>
        /// The account ID of the OAuth authorization.
        /// </value>
        public string AccountId { get; set; }

        /// <summary>
        /// Gets the linking profile of the OAuth authorization.
        /// </summary>
        /// <value>
        /// The linking profile of the OAuth authorization.
        /// </value>
        public LinkingProfile LinkingProfile { get; set; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.AccessToken.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var oauthToken = obj as OAuthToken;

            if (oauthToken == null)
            {
                return false;
            }

            return this.Equals(oauthToken);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Cronofy.OAuthToken"/> is
        /// equal to the current <see cref="Cronofy.OAuthToken"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Cronofy.OAuthToken"/> to compare with the current
        /// <see cref="Cronofy.OAuthToken"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Cronofy.OAuthToken"/> is
        /// equal to the current <see cref="Cronofy.OAuthToken"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public bool Equals(OAuthToken other)
        {
            return other != null
                && this.AccessToken == other.AccessToken
                && this.RefreshToken == other.RefreshToken
                && this.ExpiresIn == other.ExpiresIn
                && this.Scope.SequenceEqual(other.Scope)
                && this.AccountId == other.AccountId
                && object.Equals(this.LinkingProfile, other.LinkingProfile);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} AccessToken={1}, RefreshToken={2}, ExpiresIn={3}, Scope={4}, AccountId={5}, LinkingProfile={6}>",
                this.GetType(),
                this.AccessToken,
                this.RefreshToken,
                this.ExpiresIn,
                string.Join(" ", this.Scope),
                this.AccountId,
                this.LinkingProfile);
        }
    }
}
