using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Cronofy.Responses;
using Cronofy.Requests;

namespace Cronofy
{
	/// <summary>
	/// Client for the Cronofy API.
	/// </summary>
	public sealed class CronofyOAuthClient
	{
		private const string AuthorizationUrl = "https://app.cronofy.com/oauth/authorize";
		private const string TokenUrl = "https://app.cronofy.com/oauth/token";

		private const string CodeGrantType = "authorization_code";
		private const string RefreshTokenGrantType = "refresh_token";

		private readonly string clientId;
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
		/// <exception cref="ArgumentException">
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
		/// <exception cref="ArgumentException">
		/// Thrown if <paramref name="redirectUri"/> is null or empty.
		/// </exception>
		public AuthorizationUrlBuilder GetAuthorizationUrlBuilder(string redirectUri)
		{
			Preconditions.NotEmpty("redirectUri", redirectUri);

			return new AuthorizationUrlBuilder(this.clientId, redirectUri);
		}

		/// <summary>
		/// Gets the OAuth token from authorization code provided from a
		/// successful authorization request.
		/// </summary>
		/// <param name="code">
		/// The authorization code provided from an successful authorization
		/// request, must not be empty.
		/// </param>
		/// <param name="redirectUri">
		/// The redirect URI provided for the authorization requests, must not
		/// be empty.
		/// </param>
		/// <returns>
		/// Returns an <see cref="OAuthToken"/> for the provided authorization
		/// code.
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Thrown if <paramref name="code"/> or <paramref name="redirectUri"/>
		/// are null or empty.
		/// </exception>
		public OAuthToken GetTokenFromCode(string code, string redirectUri)
		{
			Preconditions.NotEmpty("code", code);
			Preconditions.NotEmpty("redirectUri", redirectUri);

			var request = new HttpRequest();

			request.Method = "POST";
			request.Url = TokenUrl;
			request.Headers = new Dictionary<string, string> {
				{ "Content-Type", "application/json; charset=utf-8" },
			};

			var requestBody = new OAuthTokenRequest {
				ClientId = this.clientId,
				ClientSecret = this.clientSecret,
				GrantType = CodeGrantType,
				Code = code,
				RedirectUri = redirectUri,
			};

			request.Body = JsonConvert.SerializeObject(requestBody);

			var response = HttpClient.GetResponse(request);
			var token = JsonConvert.DeserializeObject<OAuthTokenResponse>(response.Body);

			return new OAuthToken(token.AccessToken, token.RefreshToken, token.ExpiresIn, token.GetScopeArray());
		}

		/// <summary>
		/// Gets the OAuth token from authorization code provided from a
		/// successful authorization request.
		/// </summary>
		/// <param name="refreshToken">
		/// The refresh token that can be used to retrieve a new
		/// <see cref="OAuthToken"/>, must not be empty.
		/// </param>
		/// <returns>
		/// Returns an <see cref="OAuthToken"/> for the provided refresh token.
		/// </returns>
		/// <exception cref="ArgumentException">
		/// Thrown if <paramref name="refreshToken"/> is null or empty.
		/// </exception>
		public OAuthToken GetTokenFromRefreshToken(string refreshToken)
		{
			Preconditions.NotEmpty("refreshToken", refreshToken);

			var request = new HttpRequest();

			request.Method = "POST";
			request.Url = TokenUrl;
			request.Headers = new Dictionary<string, string> {
				{ "Content-Type", "application/json; charset=utf-8" },
			};

			var requestBody = new OAuthTokenRefreshRequest {
				ClientId = this.clientId,
				ClientSecret = this.clientSecret,
				GrantType = RefreshTokenGrantType,
				RefreshToken = refreshToken,
			};

			request.Body = JsonConvert.SerializeObject(requestBody);

			var response = HttpClient.GetResponse(request);
			var token = JsonConvert.DeserializeObject<OAuthTokenResponse>(response.Body);

			return new OAuthToken(token.AccessToken, token.RefreshToken, token.ExpiresIn, token.GetScopeArray());
		}

		/// <summary>
		/// Builder class for authorization URLs.
		/// </summary>
		public sealed class AuthorizationUrlBuilder
		{

			private static readonly string[] DefaultScopes = {
				"read_account",
				"read_events",
				"create_event",
				"delete_event",
			};

			private readonly string clientId;
			private readonly string redirectUri;
			private string[] scope;
			private string state;

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
			/// <exception cref="ArgumentException">
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
			/// <exception cref="ArgumentException">
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
			/// <exception cref="ArgumentException">
			/// Thrown if <paramref name="scope"/> is empty.
			/// </exception>
			public AuthorizationUrlBuilder Scope(IEnumerable<string> scope)
			{
				return Scope(scope.ToArray());
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
			/// <exception cref="ArgumentException">
			/// Thrown if <paramref name="state"/> is null or empty
			/// </exception>
			public AuthorizationUrlBuilder State(string state)
			{
				Preconditions.NotEmpty("state", state);

				this.state = state;

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
