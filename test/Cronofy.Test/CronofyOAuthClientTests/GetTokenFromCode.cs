namespace Cronofy.Test.CronofyOAuthClientTests
{
    using NUnit.Framework;

    [TestFixture]
    public sealed class GetTokenFromCode
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
        public void CanRedeemToken()
        {
            const string accessToken = "asdnakjsdnas";
            const int expiresIn = 3600;
            const string refreshToken = "jerwpmsdkjngvdsk";
            const string scope = "read_events create_event delete_event";

            this.http.Stub(
                HttpPost
                    .Url("https://app.cronofy.com/oauth/token")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"grant_type\":\"authorization_code\",\"code\":\"{2}\",\"redirect_uri\":\"{3}\"}}",
                        ClientId, ClientSecret, OauthCode, RedirectUri)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        "{{\"token_type\":\"bearer\",\"access_token\":\"{0}\",\"expires_in\":{1},\"refresh_token\":\"{2}\",\"scope\":\"{3}\"}}",
                        accessToken, expiresIn, refreshToken, scope));

            var actualToken = this.client.GetTokenFromCode(OauthCode, RedirectUri);
            var expectedToken = new OAuthToken(accessToken, refreshToken, expiresIn, scope.Split(new[] { ' ' }));

            Assert.AreEqual(expectedToken, actualToken);
        }

        [Test]
        public void CanRedeemTokenWithLinkingProfileInfo()
        {
            const string accessToken = "asdnakjsdnas";
            const int expiresIn = 3600;
            const string refreshToken = "jerwpmsdkjngvdsk";
            const string scope = "read_events create_event delete_event";
            const string accountId = "acc_567236000909002";
            const string providerName = "google";
            const string providerService = "gsuite";
            const string profileId = "pro_n23kjnwrw2";
            const string profileName = "example@cronofy.com";

            this.http.Stub(
                HttpPost
                    .Url("https://app.cronofy.com/oauth/token")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"grant_type\":\"authorization_code\",\"code\":\"{2}\",\"redirect_uri\":\"{3}\"}}",
                        ClientId, ClientSecret, OauthCode, RedirectUri)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        "{{" +
                        "\"token_type\":\"bearer\"," +
                        "\"access_token\":\"{0}\"," +
                        "\"expires_in\":{1}," +
                        "\"refresh_token\":\"{2}\"," +
                        "\"scope\":\"{3}\"," +
                        "\"account_id\":\"{4}\"," +
                        "\"sub\":\"{4}\"," +
                        "\"linking_profile\":" +
                            "{{" +
                            "\"provider_name\":\"{5}\"," +
                            "\"provider_service\":\"{6}\"," +
                            "\"profile_id\":\"{7}\"," +
                            "\"profile_name\":\"{8}\"" +
                            "}}" +
                        "}}",
                        accessToken, expiresIn, refreshToken, scope,
                        accountId, providerName, providerService, profileId, profileName));

            var actualToken = this.client.GetTokenFromCode(OauthCode, RedirectUri);
            var expectedToken = new OAuthToken(accessToken, refreshToken, expiresIn, scope.Split(new[] { ' ' }))
            {
                AccountId = accountId,
                Sub = accountId,
                LinkingProfile = new LinkingProfile()
                {
                    ProviderName = providerName,
                    ProviderService = providerService,
                    Id = profileId,
                    Name = profileName,
                },
            };

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
                    "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"grant_type\":\"authorization_code\",\"code\":\"{2}\",\"redirect_uri\":\"{3}\"}}",
                    ClientId, ClientSecret, OauthCode, RedirectUri)
                .ResponseCode(400)
                .ResponseBody("{\"error\":\"invalid_grant\"}"));

            Assert.Throws<CronofyResponseException>(() => this.client.GetTokenFromCode(OauthCode, RedirectUri));
        }
    }
}
