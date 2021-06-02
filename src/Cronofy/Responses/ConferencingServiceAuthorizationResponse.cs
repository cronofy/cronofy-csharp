namespace Cronofy.Responses
{
    using System;
    using Newtonsoft.Json;

    internal class ConferencingServiceAuthorizationResponse
    {
        [JsonProperty("authorization_request")]
        public ConferencingServiceAuthorizationResponseContent AuthorizationRequest { get; set; }

        internal class ConferencingServiceAuthorizationResponseContent
        {
            [JsonProperty("url")]
            public string Url { get; set; }
        }
    }
}
