namespace Cronofy
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for a Cronofy Enterprise Connect client that
    /// interacts with a service account's resources and users.
    /// </summary>
    public interface ICronofyEnterpriseConnectAccountClient : ICronofyUserInfoClient
    {
        /// <summary>
        /// Gets a list of the account's resources.
        /// </summary>
        /// <returns>
        /// An enumerable of <see cref="Resource"/>s.
        /// </returns>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        IEnumerable<Resource> GetResources();

        /// <summary>
        /// Authorizes the user for this service account.
        /// </summary>
        /// <param name="email">
        /// The email of the user to be authorized, must not be empty.
        /// </param>
        /// <param name="callbackUrl">
        /// The URL that will receive the callback for the result of the
        /// authorization attempt, must not be empty.
        /// </param>
        /// <param name="scope">
        /// The scope to request authorization for, must not be empty.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="email"/>, <paramref name="callbackUrl"/>,
        /// or <paramref name="scope"/> are empty.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        void AuthorizeUser(string email, string callbackUrl, IEnumerable<string> scope);

        /// <summary>
        /// Authorizes the user for this service account.
        /// </summary>
        /// <param name="email">
        /// The email of the user to be authorized, must not be empty.
        /// </param>
        /// <param name="callbackUrl">
        /// The URL that will receive the callback for the result of the
        /// authorization attempt, must not be empty.
        /// </param>
        /// <param name="scope">
        /// The scope to request authorization for, must not be empty.
        /// </param>
        /// <param name="state">
        /// The optional state to pass through the authorization flow, which
        /// will be returned to the <paramref name="callbackUrl"/> unmodified. Can be null.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="email"/>, <paramref name="callbackUrl"/>,
        /// or <paramref name="scope"/> are empty.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        void AuthorizeUser(string email, string callbackUrl, string scope, string state = null);

        /// <summary>
        /// Authorizes the collection of users for this service account.
        /// </summary>
        /// <param name="options">
        /// The collection of users to be authorized, must not be null.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="options"/> is null.
        /// </exception>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        void AuthorizeUsers(IEnumerable<EnterpriseConnectAuthorizeUserOptions> options);
    }
}
