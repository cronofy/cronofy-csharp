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
    public sealed class CronofyEnterpriseConnectAccountClient : CronofyAccessTokenClient, ICronofyEnterpriseConnectAccountClient
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.CronofyEnterpriseConnectAccountClient"/> class.
        /// </summary>
        /// <param name="accessToken">
        /// The access token for the OAuth authorization for the account, must
        /// not be empty.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="accessToken"/> is null or empty.
        /// </exception>
        public CronofyEnterpriseConnectAccountClient(string accessToken)
            : base(accessToken)
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.CronofyEnterpriseConnectAccountClient"/> class.
        /// </summary>
        /// <param name="accessToken">
        /// The access token for the OAuth authorization for the account, must
        /// not be empty.
        /// </param>
        /// <param name="dataCentre">
        /// The data centre to use.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="accessToken"/> is <c>null</c> or
        /// empty.
        /// </exception>
        public CronofyEnterpriseConnectAccountClient(string accessToken, string dataCentre)
            : base(accessToken, dataCentre)
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.CronofyEnterpriseConnectAccountClient"/> class.
        /// </summary>
        /// <param name="accessToken">
        /// The access token for the OAuth authorization for the account, must
        /// not be empty.
        /// </param>
        /// <param name="dataCentre">
        /// The data centre to use, must not be <c>null</c>.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="accessToken"/> is <c>null</c> or
        /// empty, or if <paramref name="dataCentre"/> is <c>null</c>.
        /// </exception>
        public CronofyEnterpriseConnectAccountClient(string accessToken, DataCentre dataCentre)
            : base(accessToken, dataCentre)
        {
        }

        /// <inheritdoc/>
        public IEnumerable<Resource> GetResources()
        {
            var request = new HttpRequest();

            request.Method = "GET";
            request.Url = this.UrlProvider.ResourcesUrl;
            request.AddOAuthAuthorization(this.AccessToken);

            var response = this.HttpClient.GetJsonResponse<ResourcesResponse>(request);

            return response.Resources.Select(x => x.ToResource());
        }

        /// <inheritdoc/>
        public void AuthorizeUser(string email, string callbackUrl, IEnumerable<string> scope)
        {
            this.AuthorizeUser(email, callbackUrl, string.Join(" ", scope.ToArray()));
        }

        /// <inheritdoc/>
        public void AuthorizeUser(string email, string callbackUrl, string scope)
        {
            Preconditions.NotEmpty("email", email);
            Preconditions.NotEmpty("callbackUrl", callbackUrl);
            Preconditions.NotEmpty("scope", scope);

            var request = new HttpRequest
            {
                Method = "POST",
                Url = this.UrlProvider.AuthorizeWithServiceAccountUrl,
            };
            request.AddOAuthAuthorization(this.AccessToken);

            var requestBody = new
            {
                email,
                callback_url = callbackUrl,
                scope,
            };
            request.SetJsonBody(requestBody);

            this.HttpClient.GetValidResponse(request);
        }
    }
}
