using System;
using System.Collections.Generic;
using System.Web;

namespace Cronofy
{
	public sealed class CronofyClient
	{
		private readonly string clientId;

		public CronofyClient(string clientId)
		{
			this.clientId = clientId;
		}

		public AuthorizationUrlBuilder GetAuthorizationUrlBuilder()
		{
			return new AuthorizationUrlBuilder(this.clientId);
		}

		public sealed class AuthorizationUrlBuilder
		{
			private readonly string clientId;
			private string[] scope;
			private string state;

			internal AuthorizationUrlBuilder(string clientId)
			{
				this.clientId = clientId;
				this.scope = new string[] { "read_account", "read_events", "create_event", "delete_event" };
			}

			public AuthorizationUrlBuilder Scope(params string[] scope)
			{
				this.scope = scope;
				return this;
			}

			public AuthorizationUrlBuilder State(string state)
			{
				this.state = state;
				return this;
			}

			public string Build()
			{
				var urlBuilder = new UrlBuilder()
					.Url("https://app.cronofy.com/oauth/authorize")
					.AddParameter("client_id", this.clientId)
					.AddParameter("scope", string.Join(" ", this.scope));

				if (this.state != null)
				{
					urlBuilder.AddParameter("state", this.state);
				}

				return urlBuilder.Build();
			}

			public override string ToString()
			{
				return this.Build();
			}
		}
	}
}
