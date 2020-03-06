namespace Cronofy
{
    using Cronofy.Requests;
    using Cronofy.Responses;

    /// <summary>
    /// Interface for a client for performing operations at a Cronofy Developer account level.
    /// </summary>
    public interface ICronofyApiKeyClient
    {
        /// <summary>
        /// Provision a new application within your developer account.
        /// </summary>
        /// <param name="provisionApplicationRequest">The application provisioning request.</param>
        /// <returns>An application provisioning response.</returns>
        ProvisionApplicationResponse ProvisionApplication(ProvisionApplicationRequest provisionApplicationRequest);
    }
}
