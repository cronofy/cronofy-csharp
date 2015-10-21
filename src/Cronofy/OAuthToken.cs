using System;
using System.Linq;

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

        public override int GetHashCode()
        {
            return this.AccessToken.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var oauthToken = obj as OAuthToken;

            if (oauthToken == null)
            {
                return false;
            }

            return Equals(oauthToken);
        }

        public bool Equals(OAuthToken other)
        {
            return other != null
                && this.AccessToken == other.AccessToken
                && this.RefreshToken == other.RefreshToken
                && this.ExpiresIn == other.ExpiresIn
                && this.Scope.SequenceEqual(other.Scope);
        }
    }
}
