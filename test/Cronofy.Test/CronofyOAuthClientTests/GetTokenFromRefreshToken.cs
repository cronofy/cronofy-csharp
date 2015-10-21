using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyOAuthClientTests
{
    [TestFixture]
    public sealed class GetTokenFromRefreshToken
    {
        private const string clientId = "abcdef123456";
        private const string clientSecret = "s3cr3t1v3";
        private const string refreshToken = "zyxvut987654";

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
        public void CanRedeemToken()
        {
            const string accessToken = "asdnakjsdnas";
            const int expiresIn = 3600;
            const string newRefreshToken = "jerwpmsdkjngvdsk";
            const string scope = "read_events create_event delete_event";

            http.Stub(
                HttpPost
                    .Url("https://app.cronofy.com/oauth/token")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"grant_type\":\"refresh_token\",\"refresh_token\":\"{2}\"}}",
                        clientId, clientSecret, refreshToken)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        "{{\"token_type\":\"bearer\",\"access_token\":\"{0}\",\"expires_in\":{1},\"refresh_token\":\"{2}\",\"scope\":\"{3}\"}}",
                        accessToken, expiresIn, newRefreshToken, scope)
            );

            var actualToken = client.GetTokenFromRefreshToken(refreshToken);
            var expectedToken = new OAuthToken(accessToken, newRefreshToken, expiresIn, scope.Split(new[] { ' ' }));

            Assert.AreEqual(expectedToken, actualToken);
        }
    }
}
