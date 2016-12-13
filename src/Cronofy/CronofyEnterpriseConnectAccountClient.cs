namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cronofy.Responses;

    /// <summary>
    /// Class for a Cronofy Enterprise Connect client that
    /// interacts with a service account's resources and users.
    /// </summary>
    public class CronofyEnterpriseConnectAccountClient : CronofyAccountClientBase, ICronofyEnterpriseConnectAccountClient 
    {
        /// <summary>
        /// The URL of the resources endpoint.
        /// </summary>
        private const string ResourcesUrl = "https://api.cronofy.com/v1/resources";

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.CronofyEnterpriseConnectAccountClient"/> class.
        /// </summary>
        /// <param name="accessToken">
        /// The user's access token.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="accessToken"/> is null or empty.
        /// </exception>
        public CronofyEnterpriseConnectAccountClient(string accessToken) : base(accessToken)
        {
        }

        /// <inheritdoc/>
        public IEnumerable<Resource> GetResources()
        {
            var request = new HttpRequest();

            request.Method = "GET";
            request.Url = ResourcesUrl;
            request.AddOAuthAuthorization(this.AccessToken);

            var response = this.HttpClient.GetJsonResponse<ResourcesResponse>(request);

            return response.Resources.Select(x => x.ToResource());
        }
    }
}
