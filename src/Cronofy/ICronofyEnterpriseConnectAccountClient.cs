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
        /// <param name="email">Email.</param>
        /// <param name="callbackUrl">Callback URL.</param>
        /// <param name="scope">Scope.</param>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        void AuthorizeUser(string email, string callbackUrl, IEnumerable<string> scope);

        /// <summary>
        /// Authorizes the user for this service account.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="callbackUrl">Callback URL.</param>
        /// <param name="scope">Scope.</param>
        /// <exception cref="CronofyException">
        /// Thrown if an error is encountered whilst making the request.
        /// </exception>
        void AuthorizeUser(string email, string callbackUrl, string scope);
    }
}
