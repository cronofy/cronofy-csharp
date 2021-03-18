namespace Cronofy
{
    using Cronofy.Responses;

    /// <summary>
    /// Class for a Cronofy client base that manages the
    /// access token and any shared client methods.
    /// </summary>
    public class CronofyAccessTokenClient : ICronofyUserInfoClient
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.CronofyAccessTokenClient"/> class.
        /// </summary>
        /// <param name="accessToken">
        /// The access token for the OAuth authorization for the account, must
        /// not be empty.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="accessToken"/> is null or empty.
        /// </exception>
        public CronofyAccessTokenClient(string accessToken)
            : this(accessToken, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.CronofyAccessTokenClient"/> class.
        /// </summary>
        /// <param name="accessToken">
        /// The access token for the OAuth authorization for the account, must
        /// not be empty.
        /// </param>
        /// <param name="dataCenter">
        /// The data center to use.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="accessToken"/> is <c>null</c> or
        /// empty.
        /// </exception>
        public CronofyAccessTokenClient(string accessToken, string dataCenter)
        {
            Preconditions.NotEmpty("accessToken", accessToken);

            this.AccessToken = accessToken;
            this.UrlProvider = UrlProviderFactory.GetProvider(dataCenter);
            this.HttpClient = new ConcreteHttpClient();
        }

        /// <summary>
        /// Gets or sets the HTTP client.
        /// </summary>
        /// <value>
        /// The HTTP client.
        /// </value>
        /// <remarks>
        /// Intend for test purposes only.
        /// </remarks>
        internal IHttpClient HttpClient { get; set; }

        /// <summary>
        /// Gets the access token for the OAuth authorization for the account.
        /// </summary>
        protected string AccessToken { get; }

        /// <summary>
        /// Gets or sets the URL provider for the context.
        /// </summary>
        protected UrlProvider UrlProvider { get; set; }

        /// <inheritdoc/>
        public UserInfo GetUserInfo()
        {
            var request = new HttpRequest
            {
                Method = "GET",
                Url = this.UrlProvider.UserInfoUrl,
            };
            request.AddOAuthAuthorization(this.AccessToken);

            var response = this.HttpClient.GetJsonResponse<UserInfoResponse>(request);

            return response.ToUserInfo();
        }
    }
}
