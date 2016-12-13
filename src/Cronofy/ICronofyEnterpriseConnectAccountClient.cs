namespace Cronofy
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for a Cronofy Enterprise Connect client that
    /// interacts with a service account's resources and users.
    /// </summary>
    public interface ICronofyEnterpriseConnectAccountClient : ICronofyAccountClientBase
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
    }
}
