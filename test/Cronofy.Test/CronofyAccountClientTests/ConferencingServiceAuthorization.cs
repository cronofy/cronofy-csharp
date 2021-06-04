namespace Cronofy.Test.CronofyAccountClientTests
{
    using NUnit.Framework;

    internal sealed class ConferencingServiceAuthorization : Base
    {
        [Test]
        public void CanRequestAConferencingAuthorizationUrl()
        {
            var redirectUri = "https://example.com/redirect";
            var authUrl = "https://app.cronofy.com/conferencing_services/xxxxx";

            this.Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/conferencing_service_authorizations")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBody(@"{""redirect_uri"":""" + redirectUri + @"""}")
                    .ResponseCode(200)
                    .ResponseBody(
                    @"{
                        ""authorization_request"": {
                            ""url"": """ + authUrl + @"""
                        }
                    }"));

            var result = this.Client.GetConferencingServiceAuthorizationUrl(
                new Requests.ConferencingServiceAuthorizationRequest
                {
                    RedirectUri = redirectUri,
                });

            Assert.AreEqual(authUrl, result);
        }

        [Test]
        public void CanRequestAConferencingAuthorizationUrlWithProviderName()
        {
            var redirectUri = "https://example.com/redirect";
            var authUrl = "https://app.cronofy.com/conferencing_services/xxxxx";
            var provider = "zoom";

            this.Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/conferencing_service_authorizations")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBody(@"{""redirect_uri"":""" + redirectUri + @""",""provider_name"":""" + provider + @"""}")
                    .ResponseCode(200)
                    .ResponseBody(
                    @"{
                        ""authorization_request"": {
                            ""url"": """ + authUrl + @"""
                        }
                    }"));

            var result = this.Client.GetConferencingServiceAuthorizationUrl(
                new Requests.ConferencingServiceAuthorizationRequest
                {
                    RedirectUri = redirectUri,
                    ProviderName = provider,
                });

            Assert.AreEqual(authUrl, result);
        }
    }
}
