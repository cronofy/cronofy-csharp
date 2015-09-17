using System;

namespace Cronofy
{
	public sealed class OAuthToken
	{
		public string AccessToken { get; private set; }
		public string RefreshToken { get; private set; }
		public int ExpiresIn { get; private set; }
		public string[] Scope { get; private set; }

		public string Response { get; private set; }

		public OAuthToken(string accessToken, string refreshToken, int expiresIn, string[] scope)
		{
			this.AccessToken = accessToken;
			this.RefreshToken = refreshToken;
			this.ExpiresIn = expiresIn;
			this.Scope = scope;
		}
	}
}
