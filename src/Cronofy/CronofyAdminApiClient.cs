namespace Cronofy
{
    using Cronofy.Requests;
    using Cronofy.Responses;

    /// <summary>
    /// A client for performing operations at a Cronofy Developer account level.
    /// </summary>
    public class CronofyAdminApiClient : ICronofyAdminApiClient
    {
        /// <summary>
        /// The admin api key.
        /// </summary>
        private readonly string adminApiKey;

        /// <summary>
        /// The url provider.
        /// </summary>
        private readonly UrlProvider urlProvider;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.CronofyAdminApiClient"/> class.
        /// </summary>
        /// <param name="adminApiKey">
        /// Your admin API key, must not be blank.
        /// </param>
        /// <param name="dataCenter">
        /// The data center to use.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="adminApiKey"/> is blank.
        /// </exception>
        public CronofyAdminApiClient(string adminApiKey, string dataCenter = null)
        {
            Preconditions.NotBlank(nameof(adminApiKey), adminApiKey);

            this.adminApiKey = adminApiKey;
            this.urlProvider = UrlProviderFactory.GetProvider(dataCenter);
            this.HttpClient = new ConcreteHttpClient();
        }

        /// <summary>
        /// Gets or sets the HTTP client.
        /// </summary>
        /// <value>
        /// The HTTP client.
        /// </value>
        /// <remarks>
        /// Intended for test purposes only.
        /// </remarks>
        internal IHttpClient HttpClient { get; set; }

        /// <inheritdoc/>
        public ProvisionApplicationResponse ProvisionApplication(ProvisionApplicationRequest provisionApplicationRequest)
        {
            Preconditions.NotNull(nameof(provisionApplicationRequest), provisionApplicationRequest);
            Preconditions.NotBlank(nameof(provisionApplicationRequest.Name), provisionApplicationRequest.Name);
            Preconditions.NotBlank(nameof(provisionApplicationRequest.Url), provisionApplicationRequest.Url);

            var request = new HttpRequest
            {
                Method = "POST",
                Url = this.urlProvider.ProvisionApplicationUrl,
            };

            request.SetJsonBody(provisionApplicationRequest);
            request.AddOAuthAuthorization(this.adminApiKey);

            return this.HttpClient.GetJsonResponse<ProvisionApplicationResponse>(request);
        }
    }
}
