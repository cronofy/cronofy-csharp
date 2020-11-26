namespace Cronofy
{
    using System;
    using Cronofy.Requests;

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
        /// Gets the OAuth tokens for an Application Calendar.
        /// </summary>
        /// <param name="applicationCalendarId">
        /// The application calendar id to create.
        /// </param>
        /// <returns>
        /// Returns an <see cref="OAuthToken"/> for the provided authorization
        /// code.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="applicationCalendarId"/> is null or empty.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        /// <remarks>
        /// TODO Validation exceptions.
        /// </remarks>
        OAuthToken ApplicationCalendar(string applicationCalendarId);

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

        /// <summary>
        /// Validates whether the provided HMAC matches the one generated for
        /// the corresponding request bytes.
        /// </summary>
        /// <param name="sha256Hmac">
        /// The HMAC taken from the <code>Cronofy-HMAC-SHA256</code> header of
        /// the request, must not be null or empty.
        /// </param>
        /// <param name="requestBytes">
        /// The contents of the request.
        /// </param>
        /// <returns>
        /// <code>true</code> if the HMAC matches, otherwise <code>false</code>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="sha256Hmac"/> is null or empty.
        /// </exception>
        bool HmacMatches(string sha256Hmac, byte[] requestBytes);

        /// <summary>
        /// Generates a link for real time scheduling based on the request provided.
        /// </summary>
        /// <returns>The url to visit for a real time scheduling flow.</returns>
        /// <param name="realTimeSchedulingRequest">Real time scheduling request.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="realTimeSchedulingRequest"/> is null.
        /// </exception>
        [Obsolete("Use CreateRealTimeSchedulingLink() instead and access the response's Url attribute")]
        string RealTimeScheduling(RealTimeSchedulingRequest realTimeSchedulingRequest);

        /// <summary>
        /// Generates a link for real time scheduling based on the request provided.
        /// </summary>
        /// <returns>The status of the newly-created link.</returns>
        /// <param name="realTimeSchedulingRequest">Real time scheduling request.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="realTimeSchedulingRequest"/> is null.
        /// </exception>
        RealTimeSchedulingLinkStatus CreateRealTimeSchedulingLink(RealTimeSchedulingRequest realTimeSchedulingRequest);

        /// <summary>
        /// Generates a link for real time sequencing based on the request provided.
        /// </summary>
        /// <returns>The url to visit for a real time scheduling flow.</returns>
        /// <param name="realTimeSequencingRequest">Real time scheduling request.</param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="realTimeSequencingRequest"/> is null.
        /// </exception>
        string RealTimeSequencing(RealTimeSequencingRequest realTimeSequencingRequest);

        /// <summary>
        /// Creates a Smart Invite for the given request.
        /// </summary>
        /// <param name="smartInviteRequest">
        /// The details of the invite, must not be <code>null</code>.
        /// </param>
        /// <returns>
        /// A Smart Invite for the given request.
        /// </returns>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        SmartInvite CreateInvite(SmartInviteRequest smartInviteRequest);

        /// <summary>
        /// Cancels a Smart Invite for the given request.
        /// </summary>
        /// <param name="smartInviteId">The invite id to cancel.</param>
        /// <param name="recipientEmail">The recipient for the cancellation.</param>
        /// <returns>
        /// A Smart Invite for the given request.
        /// </returns>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        SmartInvite CancelInvite(string smartInviteId, string recipientEmail);

        /// <summary>
        /// Retreives details of a Smart Invite.
        /// </summary>
        /// <param name="smartInviteId">
        /// The invite id.
        /// </param>
        /// <param name="emailAddress">
        /// The email address of the invitee.
        /// </param>
        /// <returns>
        /// A Smart Invite for the given request.
        /// </returns>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        SmartInvite GetSmartInvite(string smartInviteId, string emailAddress);

        /// <summary>
        /// Creates a Smart Invite for the given request.
        /// </summary>
        /// <param name="smartInviteRequest">
        /// The details of the invite, must not be <code>null</code>.
        /// </param>
        /// <returns>
        /// A Smart Invite for the given request.
        /// </returns>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        SmartInviteMultiRecipient CreateInvite(SmartInviteMultiRecipientRequest smartInviteRequest);

        /// <summary>
        /// Retreives details of a Smart Invite.
        /// </summary>
        /// <param name="smartInviteId">
        /// The invite id.
        /// </param>
        /// <returns>
        /// A Smart Invite for the given request.
        /// </returns>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        SmartInviteMultiRecipient GetSmartInvite(string smartInviteId);

        /// <summary>
        /// Submits your application for verification.
        /// </summary>
        /// <param name="applicationVerificationRequest">
        /// The details of the verification request, must not be <code>null</code>.
        /// </param>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="applicationVerificationRequest"/> is null or invalid.
        /// </exception>
        void SubmitApplicationVerification(ApplicationVerificationRequest applicationVerificationRequest);

        /// <summary>
        /// Gets an Element Token for use with UI Elements.
        /// </summary>
        /// <param name="elementTokenRequest">
        /// The details of the Element Token request, must not be <code>null</code>.
        /// </param>
        /// <returns>
        /// Returns an <see cref="ElementToken"/>.
        /// </returns>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="elementTokenRequest"/> is null or invalid.
        /// </exception>
        ElementToken GetElementToken(ElementTokenRequest elementTokenRequest);
    }
}
