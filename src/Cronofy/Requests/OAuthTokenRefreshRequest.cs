using System;
using Newtonsoft.Json;

namespace Cronofy.Requests
{
	internal sealed class OAuthTokenRefreshRequest
	{
		[JsonProperty("client_id")]
		public string ClientId { get; set; }

		[JsonProperty("client_secret")]
		public string ClientSecret { get; set; }

		[JsonProperty("grant_type")]
		public string GrantType { get; set; }

		[JsonProperty("refresh_token")]
		public string RefreshToken { get; set; }
	}
}
