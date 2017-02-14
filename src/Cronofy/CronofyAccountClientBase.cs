namespace Cronofy
{
    using Cronofy.Responses;

    /// <summary>
    /// Class for a Cronofy client base that manages the
    /// access token and any shared client methods.
    /// </summary>
    public class CronofyAccountClientBase : ICronofyUserInfoClient
    {
        /// <summary>
        /// The access token for the OAuth authorization for the account.
        /// </summary>
        protected readonly string AccessToken;

        /// <summary>
        /// The URL provider for the context.
        /// </summary>
        protected readonly UrlProvider UrlProvider;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.CronofyAccountClientBase"/> class.
        /// </summary>
        /// <param name="accessToken">
        /// The access token for the OAuth authorization for the account, must
        /// not be empty.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="accessToken"/> is null or empty.
        /// </exception>
        public CronofyAccountClientBase(string accessToken)
            : this(accessToken, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.CronofyAccountClientBase"/> class.
        /// </summary>
        /// <param name="accessToken">
        /// The access token for the OAuth authorization for the account, must
        /// not be empty.
        /// </param>
        /// <param name="dataCentre">
        /// The data centre to use.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="accessToken"/> is <code>null</code> or
        /// empty, or if <paramref name="dataCentre"/> is <code>null</code>.
        /// </exception>
        public CronofyAccountClientBase(string accessToken, string dataCentre)
        {
            Preconditions.NotEmpty("accessToken", accessToken);

            this.AccessToken = accessToken;
            this.UrlProvider = UrlProviderFactory.GetProvider(dataCentre);
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

        /// <inheritdoc/>
        public UserInfo GetUserInfo()
        {
            var request = new HttpRequest();

            request.Method = "GET";
            request.Url = this.UrlProvider.UserInfoUrl;
            request.AddOAuthAuthorization(this.AccessToken);

            var response = this.HttpClient.GetJsonResponse<UserInfoResponse>(request);

            return response.ToUserInfo();
        }
    }
}
