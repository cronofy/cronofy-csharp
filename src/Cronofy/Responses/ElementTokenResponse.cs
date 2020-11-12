namespace Cronofy
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    internal sealed class ElementTokenResponse
    {
        [JsonProperty("element_token")]
        public ElementTokenResponse.InternalElementToken ElementToken { get; set; }

        public ElementToken ToElementToken()
        {
            return new ElementToken(
                this.ElementToken.Token,
                this.ElementToken.Origin,
                this.ElementToken.Permissions.ToArray(),
                this.ElementToken.ExpiresIn);
        }

        internal sealed class InternalElementToken
        {
            [JsonProperty("permissions")]
            public IEnumerable<string> Permissions { get; set; }

            [JsonProperty("origin")]
            public string Origin { get; set; }

            [JsonProperty("token")]
            public string Token { get; set; }

            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }
        }
    }
}