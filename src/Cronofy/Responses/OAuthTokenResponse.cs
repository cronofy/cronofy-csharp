using System;
using Newtonsoft.Json;

namespace Cronofy.Responses
{
	internal sealed class OAuthTokenResponse
	{
		[JsonProperty("access_token")]
		public string AccessToken { get; set; }

		[JsonProperty("refresh_token")]
		public string RefreshToken { get; set; }

		[JsonProperty("expires_in")]
		public int ExpiresIn { get; set; }

		[JsonProperty("scope")]
		public string Scope { get; set; }

		public string[] GetScopeArray()
		{
			return Scope.Split(new[] { ' ' });
		}
	}
}
