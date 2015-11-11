namespace Cronofy
{
    using System.Collections.Generic;
    using System.Linq;
    using Cronofy.Requests;
    using Cronofy.Responses;

    /// <summary>
    /// Client for the Cronofy API.
    /// </summary>
    public sealed class CronofyOAuthClient : ICronofyOAuthClient
    {
        /// <summary>
        /// The URL for the OAuth authorization endpoint.
        /// </summary>
        private const string AuthorizationUrl = "https://app.cronofy.com/oauth/authorize";

        /// <summary>
        /// The URL for the OAuth token endpoint.
        /// </summary>
        private const string TokenUrl = "https://app.cronofy.com/oauth/token";

        /// <summary>
        /// The URL for the OAuth token revocation endpoint.
        /// </summary>
        private const string TokenRevocationUrl = "https://app.cronofy.com/oauth/token/revoke";

        /// <summary>
        /// The grant type for exchanging an OAuth authorization code.
        /// </summary>
        private const string CodeGrantType = "authorization_code";

        /// <summary>
        /// The grant type for refreshing an OAuth authorization's access token.
        /// </summary>
        private const string RefreshTokenGrantType = "refresh_token";

        /// <summary>
        /// The client ID of the OAuth application.
        /// </summary>
        private readonly string clientId;

        /// <summary>
        /// The client secret of the OAuth application.
        /// </summary>
        private readonly string clientSecret;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.CronofyOAuthClient"/> class.
        /// </summary>
        /// <param name="clientId">
        /// Your OAuth client_id, must not be blank.
        /// </param>
        /// <param name="clientSecret">
        /// Your OAuth client_secret, must not be blank.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="clientId"/> or
        /// <paramref name="clientSecret"/> are blank.
        /// </exception>
        public CronofyOAuthClient(string clientId, string clientSecret)
        {
            Preconditions.NotBlank("clientId", clientId);
            Preconditions.NotBlank("clientSecret", clientSecret);

            this.clientId = clientId;
            this.clientSecret = clientSecret;
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
        /// Creates a new <see cref="AuthorizationUrlBuilder"/> seeded with your
        /// client configuration.
        /// </summary>
        /// <param name="redirectUri">
        /// The URI to redirect the user's response for the authorization
        /// request to, must not be empty.
        /// </param>
        /// <returns>
        /// Returns a new <see cref="AuthorizationUrlBuilder"/>.
        /// </returns>
        /// <remarks>
        /// The read_account, read_events, create_event, and delete_event scopes
        /// are requested by default.
        /// </remarks>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="redirectUri"/> is null or empty.
        /// </exception>
        public AuthorizationUrlBuilder GetAuthorizationUrlBuilder(string redirectUri)
        {
            Preconditions.NotEmpty("redirectUri", redirectUri);

            return new AuthorizationUrlBuilder(this.clientId, redirectUri);
        }

        /// <inheritdoc/>
        public OAuthToken GetTokenFromCode(string code, string redirectUri)
        {
            Preconditions.NotEmpty("code", code);
            Preconditions.NotEmpty("redirectUri", redirectUri);

            var request = new HttpRequest();

            request.Method = "POST";
            request.Url = TokenUrl;

            var requestBody = new OAuthTokenRequest
                {
                    ClientId = this.clientId,
                    ClientSecret = this.clientSecret,
                    GrantType = CodeGrantType,
                    Code = code,
                    RedirectUri = redirectUri,
                };

            request.SetJsonBody(requestBody);

            var tokenResponse = this.HttpClient.GetJsonResponse<OAuthTokenResponse>(request);

            return tokenResponse.ToToken();
        }

        /// <inheritdoc/>
        public void RevokeToken(string token)
        {
            Preconditions.NotEmpty("token", token);

            var request = new HttpRequest();

            request.Method = "POST";
            request.Url = TokenRevocationUrl;

            var requestBody = new OAuthTokenRevocationRequest
            {
                ClientId = this.clientId,
                ClientSecret = this.clientSecret,
                Token = token,
            };

            request.SetJsonBody(requestBody);

            var response = this.HttpClient.GetResponse(request);

            if (response.Code != 200)
            {
                throw new CronofyResponseException("Request failed", response);
            }
        }

        /// <inheritdoc/>
        public OAuthToken GetTokenFromRefreshToken(string refreshToken)
        {
            Preconditions.NotEmpty("refreshToken", refreshToken);

            var request = new HttpRequest();

            request.Method = "POST";
            request.Url = TokenUrl;

            var requestBody = new OAuthTokenRefreshRequest
            {
                ClientId = this.clientId,
                ClientSecret = this.clientSecret,
                GrantType = RefreshTokenGrantType,
                RefreshToken = refreshToken,
            };

            request.SetJsonBody(requestBody);

            var token = this.HttpClient.GetJsonResponse<OAuthTokenResponse>(request);

            return token.ToToken();
        }

        /// <summary>
        /// Builder class for authorization URLs.
        /// </summary>
        public sealed class AuthorizationUrlBuilder
        {
            /// <summary>
            /// The default scopes for a new OAuth authorization request.
            /// </summary>
            private static readonly string[] DefaultScopes =
            {
                "read_account",
                "read_events",
                "create_event",
                "delete_event",
            };

            /// <summary>
            /// The client ID of the OAuth application.
            /// </summary>
            private readonly string clientId;

            /// <summary>
            /// The URI to redirect the user's authorization response to.
            /// </summary>
            private readonly string redirectUri;

            /// <summary>
            /// The scope the OAuth application is requesting from the user.
            /// </summary>
            private string[] scope;

            /// <summary>
            /// The state to persist through the OAuth authorization process.
            /// </summary>
            private string state;

            /// <summary>
            /// The value of the avoid linking parameter for the OAuth
            /// authorization process.
            /// </summary>
            private bool? avoidLinking;

            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="AuthorizationUrlBuilder"/> class.
            /// </summary>
            /// <param name="clientId">
            /// The application's OAuth client_id, must not be blank.
            /// </param>
            /// <param name="redirectUri">
            /// The URI to redirect the user's response for the authorization
            /// request to, must not be empty.
            /// </param>
            /// <exception cref="System.ArgumentException">
            /// Thrown if <paramref name="clientId"/> is blank, or if
            /// <paramref name="redirectUri"/> is empty.
            /// </exception>
            internal AuthorizationUrlBuilder(string clientId, string redirectUri)
            {
                Preconditions.NotBlank("clientId", clientId);
                Preconditions.NotEmpty("redirectUri", redirectUri);

                this.clientId = clientId;
                this.redirectUri = redirectUri;
                this.scope = DefaultScopes;
            }

            /// <summary>
            /// Sets the scope the authorization URL will request from the user.
            /// </summary>
            /// <param name="scope">
            /// The scope to request from the user, must not be empty.
            /// </param>
            /// <returns>
            /// A reference to the builder.
            /// </returns>
            /// <exception cref="System.ArgumentException">
            /// Thrown if <paramref name="scope"/> is empty.
            /// </exception>
            public AuthorizationUrlBuilder Scope(params string[] scope)
            {
                Preconditions.NotEmpty("scope", scope);

                this.scope = scope;

                return this;
            }

            /// <summary>
            /// Sets the scope the authorization URL will request from the user.
            /// </summary>
            /// <param name="scope">
            /// The scope to request from the user, must not be empty.
            /// </param>
            /// <returns>
            /// A reference to the builder.
            /// </returns>
            /// <exception cref="System.ArgumentException">
            /// Thrown if <paramref name="scope"/> is empty.
            /// </exception>
            public AuthorizationUrlBuilder Scope(IEnumerable<string> scope)
            {
                return this.Scope(scope.ToArray());
            }

            /// <summary>
            /// Sets the state to be passed through the authorization process.
            /// </summary>
            /// <param name="state">
            /// The state to be passed through the authorization process, must
            /// not be null or empty.
            /// </param>
            /// <returns>
            /// A reference to the builder.
            /// </returns>
            /// <exception cref="System.ArgumentException">
            /// Thrown if <paramref name="state"/> is null or empty.
            /// </exception>
            public AuthorizationUrlBuilder State(string state)
            {
                Preconditions.NotEmpty("state", state);

                this.state = state;

                return this;
            }

            /// <summary>
            /// Sets the avoid linking parameter for the OAuth authorization
            /// process.
            /// </summary>
            /// <param name="avoidLinking">
            /// If set to <c>true</c>, avoid linking calendars during the OAuth
            /// authorization process.
            /// </param>
            /// <returns>
            /// A reference to the builder.
            /// </returns>
            public AuthorizationUrlBuilder AvoidLinking(bool avoidLinking)
            {
                this.avoidLinking = avoidLinking;

                return this;
            }

            /// <summary>
            /// Generates an authorization URL based on the current state of the
            /// builder.
            /// </summary>
            /// <returns>
            /// An authorization URL based on the current state of the builder.
            /// </returns>
            public string Build()
            {
                var urlBuilder = new UrlBuilder()
                    .Url(AuthorizationUrl)
                    .AddParameter("client_id", this.clientId)
                    .AddParameter("response_type", "code")
                    .AddParameter("scope", string.Join(" ", this.scope))
                    .AddParameter("redirect_uri", this.redirectUri);

                if (this.state != null)
                {
                    urlBuilder.AddParameter("state", this.state);
                }

                if (this.avoidLinking.HasValue)
                {
                    urlBuilder.AddParameter("avoid_linking", this.avoidLinking.Value);
                }

                return urlBuilder.Build();
            }

            /// <summary>
            /// Returns a <see cref="System.String"/> that represents the
            /// current <see cref="AuthorizationUrlBuilder"/>.
            /// </summary>
            /// <returns>
            /// A <see cref="System.String"/> that represents the current
            /// <see cref="AuthorizationUrlBuilder"/>.
            /// </returns>
            public override string ToString()
            {
                return this.Build();
            }
        }
    }
}
