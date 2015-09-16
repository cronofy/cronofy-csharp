using System;
using System.Collections.Generic;
using System.Web;

namespace Cronofy
{
	/// <summary>
	/// Client for the Cronofy API.
	/// </summary>
	public sealed class CronofyClient
	{
		private readonly string clientId;

		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Cronofy.CronofyClient"/> class.
		/// </summary>
		/// <param name="clientId">
		/// Your OAuth client_id, must not be blank.
		/// </param>
		/// <exception cref="ArgumentException">
		/// Thrown if <paramref name="clientId"/> is blank.
		/// </exception>
		public CronofyClient(string clientId)
		{
			Preconditions.NotBlank("clientId", clientId);

			this.clientId = clientId;
		}

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
		/// Builder class for authorization URLs.
		/// </summary>
		public sealed class AuthorizationUrlBuilder
		{
			private const string AuthorizationUrl = "https://app.cronofy.com/oauth/authorize";

			private static readonly string[] DefaultScopes = new [] {
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
