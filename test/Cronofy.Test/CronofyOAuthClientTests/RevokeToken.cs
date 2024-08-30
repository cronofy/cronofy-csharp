﻿namespace Cronofy.Test.CronofyOAuthClientTests
{
    using NUnit.Framework;

    [TestFixture]
    public sealed class RevokeToken
    {
        private const string ClientId = "abcdef123456";
        private const string ClientSecret = "s3cr3t1v3";
        private const string OauthCode = "zyxvut987654";
        private const string RedirectUri = "http://example.com/redirectUri";

        private CronofyOAuthClient client;
        private StubHttpClient http;

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyOAuthClient(ClientId, ClientSecret);
            this.http = new StubHttpClient();

            this.client.HttpClient = this.http;
        }

        [Test]
        public void CanRevokeToken()
        {
            const string refreshToken = "jerwpmsdkjngvdsk";

            this.http.Stub(
                HttpPost
                    .Url("https://app.cronofy.com/oauth/token/revoke")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"token\":\"{2}\"}}",
                        ClientId, ClientSecret, refreshToken)
                    .ResponseCode(200));

            this.client.RevokeToken(refreshToken);
        }

        [Test]
        public void CanRevokeSub()
        {
            const string sub = "acc_1234567890";

            this.http.Stub(
                HttpPost
                    .Url("https://app.cronofy.com/oauth/token/revoke")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"sub\":\"{2}\"}}",
                        ClientId, ClientSecret, sub)
                    .ResponseCode(200));

            this.client.RevokeSub(sub);
        }

        [Test]
        public void CanRevokeWithRemovePiiRequest()
        {
            const string sub = "acc_1234567890";

            this.http.Stub(
                HttpPost
                    .Url("https://app.cronofy.com/oauth/token/revoke")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"sub\":\"{2}\",\"request_pii_erasure\":{3}}}",
                        ClientId, ClientSecret, sub, "true") // Need to provide bool as string to avoid incorrect serialization
                    .ResponseCode(200));

            this.client.RevokeAuthorization(new RevokeAuthorizationOptions
            {
                Sub = sub,
                RequestPiiErasure = true,
            });
        }

        [Test]
        public void RevokeAuthorizationThrowsCronofyResponseExceptionOnFailure()
        {
            const string sub = "acc_1234567890";

            this.http.Stub(
                HttpPost
                    .Url("https://app.cronofy.com/oauth/token/revoke")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"sub\":\"{2}\",\"request_pii_erasure\":{3}}}",
                        ClientId, ClientSecret, sub, "true") // Need to provide bool as string to avoid incorrect serialization
                    .ResponseCode(400)
                    .ResponseBody("{\"error\":\"invalid_client\"}"));

            var exception = Assert.Throws<CronofyResponseException>(() =>
                this.client.RevokeAuthorization(new RevokeAuthorizationOptions
                {
                    Sub = sub,
                    RequestPiiErasure = true,
                }));

            Assert.AreEqual(exception.Message, "Request failed - code=400");
            Assert.AreEqual(exception.Response.Code, 400);
            Assert.AreEqual(exception.Response.Body, "{\"error\":\"invalid_client\"}");
        }
    }
}
