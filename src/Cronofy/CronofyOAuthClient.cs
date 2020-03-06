namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Cronofy.Requests;
    using Cronofy.Responses;

    /// <summary>
    /// Client for the Cronofy API.
    /// </summary>
    public sealed class CronofyOAuthClient : ICronofyOAuthClient
    {
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
        /// The URL provider.
        /// </summary>
        private readonly UrlProvider urlProvider;

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
            : this(clientId, clientSecret, null)
        {
        }

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
        /// <param name="dataCentre">
        /// The data centre to use.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="clientId"/> or
        /// <paramref name="clientSecret"/> are blank.
        /// </exception>
        public CronofyOAuthClient(string clientId, string clientSecret, string dataCentre)
        {
            Preconditions.NotBlank("clientId", clientId);
            Preconditions.NotBlank("clientSecret", clientSecret);

            this.clientId = clientId;
            this.clientSecret = clientSecret;
            this.urlProvider = UrlProviderFactory.GetProvider(dataCentre);
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

            return new AuthorizationUrlBuilder(this.clientId, this.urlProvider, redirectUri);
        }

        /// <summary>
        /// Creates a new <see cref="AuthorizationUrlBuilder"/>, set as an Enterprise
        /// Connect authorization URL, and seeded with your client configuration.
        /// </summary>
        /// <param name="redirectUri">
        /// The URI to redirect the user's response for the authorization
        /// request to, must not be empty.
        /// </param>
        /// <returns>
        /// Returns a new <see cref="AuthorizationUrlBuilder"/>.
        /// </returns>
        /// <remarks>
        /// The delegated scopes read_account, read_events, create_event, and delete_event
        /// are requested by default.
        /// <para>
        /// The Enterprise Connect service_account/accounts/manage and
        /// service_account/resources/manage are requested by default.
        /// </para>
        /// </remarks>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="redirectUri"/> is null or empty.
        /// </exception>
        public AuthorizationUrlBuilder GetEnterpriseConnectAuthorizationUrlBuilder(string redirectUri)
        {
            Preconditions.NotEmpty("redirectUri", redirectUri);

            return new AuthorizationUrlBuilder(this.clientId, this.urlProvider, redirectUri)
                .EnterpriseConnect();
        }

        /// <inheritdoc/>
        public OAuthToken GetTokenFromCode(string code, string redirectUri)
        {
            Preconditions.NotEmpty("code", code);
            Preconditions.NotEmpty("redirectUri", redirectUri);

            var request = new HttpRequest();

            request.Method = "POST";
            request.Url = this.urlProvider.TokenUrl;

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
            request.Url = this.urlProvider.TokenRevocationUrl;

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
            request.Url = this.urlProvider.TokenUrl;

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

        /// <inheritdoc/>
        public string RealTimeScheduling(RealTimeSchedulingRequest realTimeSchedulingRequest)
        {
            Preconditions.NotNull("realTimeSchedulingRequest", realTimeSchedulingRequest);

            realTimeSchedulingRequest.ClientId = this.clientId;
            realTimeSchedulingRequest.ClientSecret = this.clientSecret;

            var request = new HttpRequest();

            request.Method = "POST";
            request.Url = this.urlProvider.RealTimeSchedulingUrl;
            request.SetJsonBody(realTimeSchedulingRequest);

            var response = this.HttpClient.GetJsonResponse<RealTimeSchedulingResponse>(request);

            return response.Url;
        }

        /// <inheritdoc/>
        public string RealTimeSequencing(RealTimeSequencingRequest realTimeSequencingRequest)
        {
            Preconditions.NotNull("realTimeSequencingRequest", realTimeSequencingRequest);

            realTimeSequencingRequest.ClientId = this.clientId;
            realTimeSequencingRequest.ClientSecret = this.clientSecret;

            var request = new HttpRequest();

            request.Method = "POST";
            request.Url = this.urlProvider.RealTimeSequencingUrl;
            request.SetJsonBody(realTimeSequencingRequest);

            var response = this.HttpClient.GetJsonResponse<RealTimeSequencingResponse>(request);

            return response.Url;
        }

        /// <inheritdoc/>
        public string AddToCalendar(AddToCalendarRequest addToCalendarRequest)
        {
            Preconditions.NotNull("addToCalendarRequest", addToCalendarRequest);

            addToCalendarRequest.ClientId = this.clientId;
            addToCalendarRequest.ClientSecret = this.clientSecret;

            var request = new HttpRequest();

            request.Method = "POST";
            request.Url = this.urlProvider.AddToCalendarUrl;
            request.SetJsonBody(addToCalendarRequest);

            var response = this.HttpClient.GetJsonResponse<AddToCalendarResponse>(request);

            return response.Url;
        }

        /// <inheritdoc/>
        public bool HmacMatches(string sha256Hmac, byte[] requestBytes)
        {
            Preconditions.NotEmpty("sha256Hmac", sha256Hmac);

            var keyBytes = Encoding.UTF8.GetBytes(this.clientSecret);

            using (var hmacsha256 = new HMACSHA256(keyBytes))
            {
                var requestHash = hmacsha256.ComputeHash(requestBytes);
                return sha256Hmac == Convert.ToBase64String(requestHash);
            }
        }

        /// <inheritdoc/>
        public SmartInvite CreateInvite(SmartInviteRequest smartInviteRequest)
        {
            var request = new HttpRequest
            {
                Method = "POST",
                Url = this.urlProvider.SmartInviteUrl
            };

            request.AddOAuthAuthorization(this.clientSecret);
            request.SetJsonBody(smartInviteRequest);

            var response = this.HttpClient.GetJsonResponse<SmartInviteResponse>(request);
            return response.ToSmartInvite();
        }

        /// <inheritdoc/>
        public SmartInvite CancelInvite(string smartInviteId, string recipientEmail)
        {
            Preconditions.NotEmpty("smartInviteId", smartInviteId);
            Preconditions.NotEmpty("emailAddress", recipientEmail);

            var request = new HttpRequest
            {
                Method = "POST",
                Url = this.urlProvider.SmartInviteUrl
            };

            var smartInviteRequest = new SmartInviteCancelRequest(smartInviteId, recipientEmail);

            request.AddOAuthAuthorization(this.clientSecret);
            request.SetJsonBody(smartInviteRequest);

            var response = this.HttpClient.GetJsonResponse<SmartInviteResponse>(request);
            return response.ToSmartInvite();
        }

        /// <inheritdoc/>
        public SmartInvite GetSmartInvite(string smartInviteId, string emailAddress)
        {
            Preconditions.NotEmpty("smartInviteId", smartInviteId);
            Preconditions.NotEmpty("emailAddress", emailAddress);

            var request = new HttpRequest
            {
                Method = "GET",
                Url = this.urlProvider.SmartInviteUrl,
                QueryString = new HttpRequest.QueryStringCollection
                {
                    { "smart_invite_id", smartInviteId },
                    { "recipient_email", emailAddress }
                }
            };

            request.AddOAuthAuthorization(this.clientSecret);

            var response = this.HttpClient.GetJsonResponse<SmartInviteResponse>(request);
            return response.ToSmartInvite();
        }

        /// <inheritdoc/>
        public SmartInviteMultiRecipient CreateInvite(SmartInviteMultiRecipientRequest smartInviteRequest)
        {
            var request = new HttpRequest
            {
                Method = "POST",
                Url = this.urlProvider.SmartInviteUrl
            };

            request.AddOAuthAuthorization(this.clientSecret);
            request.SetJsonBody(smartInviteRequest);

            var response = this.HttpClient.GetJsonResponse<SmartInviteMultiRecipientResponse>(request);
            return response.ToSmartInvite();
        }

        /// <inheritdoc/>
        public SmartInviteMultiRecipient GetSmartInvite(string smartInviteId)
        {
            Preconditions.NotEmpty("smartInviteId", smartInviteId);

            var request = new HttpRequest
            {
                Method = "GET",
                Url = this.urlProvider.SmartInviteUrl,
                QueryString = new HttpRequest.QueryStringCollection
                {
                    { "smart_invite_id", smartInviteId }
                }
            };

            request.AddOAuthAuthorization(this.clientSecret);

            var response = this.HttpClient.GetJsonResponse<SmartInviteMultiRecipientResponse>(request);
            return response.ToSmartInvite();
        }

        /// <inheritdoc />
        public OAuthToken ApplicationCalendar(string applicationCalendarId)
        {
            Preconditions.NotEmpty("applicationCalendarId", applicationCalendarId);

            var request = new HttpRequest();

            request.Method = "POST";
            request.Url = this.urlProvider.ApplicationCalendarsUrl;

            var requestBody = new ApplicationCalendarRequest
            {
                ClientId = this.clientId,
                ClientSecret = this.clientSecret,
                ApplicationCalendarId = applicationCalendarId,
            };

            request.SetJsonBody(requestBody);

            var tokenResponse = this.HttpClient.GetJsonResponse<OAuthTokenResponse>(request);

            return tokenResponse.ToToken();
        }

        /// <inheritdoc />
        public void SubmitApplicationVerification(ApplicationVerificationRequest applicationVerificationRequest)
        {
            Preconditions.NotNull(nameof(applicationVerificationRequest), applicationVerificationRequest);
            Preconditions.NotEmpty(nameof(applicationVerificationRequest.RedirectUris), applicationVerificationRequest.RedirectUris);
            Preconditions.NotEmpty(nameof(applicationVerificationRequest.Contact.Email), applicationVerificationRequest.Contact.Email);

            var request = new HttpRequest
            {
                Method = "POST",
                Url = this.urlProvider.ApplicationVerificationUrl
            };

            request.AddOAuthAuthorization(this.clientSecret);
            request.SetJsonBody(applicationVerificationRequest);

            this.HttpClient.GetValidResponse(request);
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
            /// The default scipes for a new Enterprise Connect OAuth authorization request.
            /// </summary>
            private static readonly string[] DefaultEnterpriseConnectScopes =
            {
                "service_account/accounts/manage",
                "service_account/resources/manage",
            };

            /// <summary>
            /// The client ID of the OAuth application.
            /// </summary>
            private readonly string clientId;

            /// <summary>
            /// The URL provider for the client.
            /// </summary>
            private readonly UrlProvider urlProvider;

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
            /// Whether or not the OAuth URL is for Enterprise Connect or not.
            /// </summary>
            private bool enterpriseConnect;

            /// <summary>
            /// The scope the OAuth application is requestion from the Enterprise
            /// Connect user.
            /// </summary>
            private string[] enterpriseConnectScope;

            /// <summary>
            /// The value of the link token for the OAuth authorization process.
            /// </summary>
            private string linkToken;

            /// <summary>
            /// The value of the provider name for the OAuth authorization process.
            /// </summary>
            private string providerName;

            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="AuthorizationUrlBuilder"/> class.
            /// </summary>
            /// <param name="clientId">
            /// The application's OAuth client_id, must not be blank.
            /// </param>
            /// <param name="urlProvider">
            /// The URL provider for the current context, must not be
            /// <code>null</code>.
            /// </param>
            /// <param name="redirectUri">
            /// The URI to redirect the user's response for the authorization
            /// request to, must not be empty.
            /// </param>
            /// <exception cref="System.ArgumentException">
            /// Thrown if <paramref name="clientId"/> is blank,
            /// <paramref name="urlProvider"/> is <code>null</code>, or if
            /// <paramref name="redirectUri"/> is empty.
            /// </exception>
            internal AuthorizationUrlBuilder(string clientId, UrlProvider urlProvider, string redirectUri)
            {
                Preconditions.NotBlank("clientId", clientId);
                Preconditions.NotNull("urlProvider", urlProvider);
                Preconditions.NotEmpty("redirectUri", redirectUri);

                this.clientId = clientId;
                this.urlProvider = urlProvider;
                this.redirectUri = redirectUri;
                this.scope = DefaultScopes;
                this.enterpriseConnectScope = DefaultEnterpriseConnectScopes;
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
            /// Sets the URL to be an Enterprise Connect OAuth authorization
            /// URL.
            /// </summary>
            /// <returns>
            /// A reference to the builder.
            /// </returns>
            public AuthorizationUrlBuilder EnterpriseConnect()
            {
                this.enterpriseConnect = true;

                return this;
            }

            /// <summary>
            /// Sets the scope the authorization URL will request from the Enterprise
            /// Connect user.
            /// </summary>
            /// <param name="scope">
            /// The scope to request from the Enterprise Connect user, must not be empty.
            /// </param>
            /// <returns>
            /// A reference to the builder.
            /// </returns>
            /// <exception cref="System.ArgumentException">
            /// Thrown if <paramref name="scope"/> is empty.
            /// </exception>
            public AuthorizationUrlBuilder EnterpriseConnectScope(params string[] scope)
            {
                Preconditions.NotEmpty("scope", scope);

                this.enterpriseConnectScope = scope;

                return this;
            }

            /// <summary>
            /// Sets the scope the authorization URL will request from the Enterprise
            /// Connect user.
            /// </summary>
            /// <param name="scope">
            /// The scope to request from the Enterprise Connect user, must not be empty.
            /// </param>
            /// <returns>
            /// A reference to the builder.
            /// </returns>
            /// <exception cref="System.ArgumentException">
            /// Thrown if <paramref name="scope"/> is empty.
            /// </exception>
            public AuthorizationUrlBuilder EnterpriseConnectScope(IEnumerable<string> scope)
            {
                this.enterpriseConnectScope = scope.ToArray();

                return this;
            }

            /// <summary>
            /// Sets the link token parameter for the OAuth authorization
            /// process.
            /// </summary>
            /// <param name="linkToken">
            /// The link token to use for the OAuth authorization process.
            /// </param>
            /// <returns>
            /// A reference to the builder.
            /// </returns>
            public AuthorizationUrlBuilder LinkToken(string linkToken)
            {
                this.linkToken = linkToken;

                return this;
            }

            /// <summary>
            /// Sets the provider name parameter for the OAuth authorization
            /// process.
            /// </summary>
            /// <param name="providerName">
            /// The provider name to use for the OAuth authorization process.
            /// </param>
            /// <returns>
            /// A reference to the builder.
            /// </returns>
            public AuthorizationUrlBuilder ProviderName(string providerName)
            {
                this.providerName = providerName;

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
                var authUrl = this.enterpriseConnect ? this.urlProvider.EnterpriseConnectAuthorizationUrl : this.urlProvider.AuthorizationUrl;

                var urlBuilder = new UrlBuilder()
                    .Url(authUrl)
                    .AddParameter("client_id", this.clientId)
                    .AddParameter("response_type", "code")
                    .AddParameter("redirect_uri", this.redirectUri);

                if (this.enterpriseConnect)
                {
                    urlBuilder.AddParameter("delegated_scope", string.Join(" ", this.scope));
                    urlBuilder.AddParameter("scope", string.Join(" ", this.enterpriseConnectScope));
                }
                else
                {
                    urlBuilder.AddParameter("scope", string.Join(" ", this.scope));
                }

                if (this.state != null)
                {
                    urlBuilder.AddParameter("state", this.state);
                }

                if (this.avoidLinking.HasValue)
                {
                    urlBuilder.AddParameter("avoid_linking", this.avoidLinking.Value);
                }

                if (this.linkToken != null)
                {
                    urlBuilder.AddParameter("link_token", this.linkToken);
                }

                if (this.providerName != null)
                {
                    urlBuilder.AddParameter("provider_name", this.providerName);
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
