namespace Cronofy.Test.CronofyOAuthClientTests
{
    using NUnit.Framework;

    [TestFixture]
    public sealed class GetTokenFromRefreshToken
    {
        private const string ClientId = "abcdef123456";
        private const string ClientSecret = "s3cr3t1v3";
        private const string RefreshToken = "zyxvut987654";

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
        public void CanRedeemToken()
        {
            const string accessToken = "asdnakjsdnas";
            const int expiresIn = 3600;
            const string newRefreshToken = "jerwpmsdkjngvdsk";
            const string scope = "read_events create_event delete_event";

            this.http.Stub(
                HttpPost
                    .Url("https://app.cronofy.com/oauth/token")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"grant_type\":\"refresh_token\",\"refresh_token\":\"{2}\"}}",
                        ClientId, ClientSecret, RefreshToken)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        "{{\"token_type\":\"bearer\",\"access_token\":\"{0}\",\"expires_in\":{1},\"refresh_token\":\"{2}\",\"scope\":\"{3}\"}}",
                        accessToken, expiresIn, newRefreshToken, scope));

            var actualToken = this.client.GetTokenFromRefreshToken(RefreshToken);
            var expectedToken = new OAuthToken(accessToken, newRefreshToken, expiresIn, scope.Split(new[] { ' ' }));

            Assert.AreEqual(expectedToken, actualToken);
        }

        [Test]
        public void ExceptionWhenBadRequest()
        {
            this.http.Stub(
                HttpPost
                .Url("https://app.cronofy.com/oauth/token")
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"grant_type\":\"refresh_token\",\"refresh_token\":\"{2}\"}}",
                    ClientId, ClientSecret, RefreshToken)
                .ResponseCode(400)
                .ResponseBody("{\"error\":\"invalid_grant\"}"));

            Assert.Throws<CronofyResponseException>(() => this.client.GetTokenFromRefreshToken(RefreshToken));
        }
    }
}
