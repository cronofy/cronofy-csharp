using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyOAuthClientTests
{
    [TestFixture]
    public sealed class RevokeToken
    {
        private const string clientId = "abcdef123456";
        private const string clientSecret = "s3cr3t1v3";
        private const string oauthCode = "zyxvut987654";
        private const string redirectUri = "http://example.com/redirectUri";

        private CronofyOAuthClient client;
        private StubHttpClient http;

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyOAuthClient(clientId, clientSecret);
            this.http = new StubHttpClient();

            client.HttpClient = http;
        }

        [Test]
        public void CanRevokeToken()
        {
            const string refreshToken = "jerwpmsdkjngvdsk";

            http.Stub(
                HttpPost
                    .Url("https://app.cronofy.com/oauth/token/revoke")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"token\":\"{2}\"}}",
                        clientId, clientSecret, refreshToken)
                    .ResponseCode(200)
            );

            client.RevokeToken(refreshToken);
        }
    }
}
