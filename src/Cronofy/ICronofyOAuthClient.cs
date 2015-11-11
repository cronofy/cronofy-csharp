namespace Cronofy
{
    using System;

    /// <summary>
    /// Interface for a Cronofy client that performs OAuth related operations.
    /// </summary>
    public interface ICronofyOAuthClient
    {
        /// <summary>
        /// Gets the OAuth token from an authorization code provided by a
        /// successful authorization request.
        /// </summary>
        /// <param name="code">
        /// The authorization code provided by a successful authorization
        /// request, must not be empty.
        /// </param>
        /// <param name="redirectUri">
        /// The redirect URI provided for the authorization requests, must not
        /// be empty.
        /// </param>
        /// <returns>
        /// Returns an <see cref="OAuthToken"/> for the provided authorization
        /// code.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="code"/> or <paramref name="redirectUri"/>
        /// are null or empty.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        /// <remarks>
        /// TODO Validation exceptions.
        /// </remarks>
        OAuthToken GetTokenFromCode(string code, string redirectUri);

        /// <summary>
        /// Gets the OAuth token from a refresh token retrieved with a previous
        /// OAuth token.
        /// </summary>
        /// <param name="refreshToken">
        /// The refresh token that can be used to retrieve a new
        /// <see cref="OAuthToken"/>, must not be empty.
        /// </param>
        /// <returns>
        /// Returns an <see cref="OAuthToken"/> for the provided refresh token.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="refreshToken"/> is null or empty.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        /// <remarks>
        /// TODO Validation exceptions.
        /// </remarks>
        OAuthToken GetTokenFromRefreshToken(string refreshToken);

        /// <summary>
        /// Revokes the given authorization token.
        /// </summary>
        /// <param name="token">
        /// <para>
        /// The refresh token or access token of the OAuth authorization to
        /// revoke, must not be null or empty.
        /// </para>
        /// <para>
        /// It is recommended that the refresh token is passed as it cannot
        /// expire.
        /// </para>
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="token"/> is null or empty.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        void RevokeToken(string token);
    }
}
