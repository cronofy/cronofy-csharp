using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Cronofy.Test.CronofyOAuthClientTests
{
    [TestFixture]
    public sealed class GetTokenFromCode
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
        public void CanRedeemToken()
        {
            const string accessToken = "asdnakjsdnas";
            const int expiresIn = 3600;
            const string refreshToken = "jerwpmsdkjngvdsk";
            const string scope = "read_events create_event delete_event";

            http.Stub(
                HttpPost
                    .Url("https://app.cronofy.com/oauth/token")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"grant_type\":\"authorization_code\",\"code\":\"{2}\",\"redirect_uri\":\"{3}\"}}",
                        clientId, clientSecret, oauthCode, redirectUri)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        "{{\"token_type\":\"bearer\",\"access_token\":\"{0}\",\"expires_in\":{1},\"refresh_token\":\"{2}\",\"scope\":\"{3}\"}}",
                        accessToken, expiresIn, refreshToken, scope)
            );

            var actualToken = client.GetTokenFromCode(oauthCode, redirectUri);
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
            const string profileId = "pro_n23kjnwrw2";
            const string profileName = "example@cronofy.com";

            http.Stub(
                HttpPost
                    .Url("https://app.cronofy.com/oauth/token")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"grant_type\":\"authorization_code\",\"code\":\"{2}\",\"redirect_uri\":\"{3}\"}}",
                        clientId, clientSecret, oauthCode, redirectUri)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        "{{" +
                        "\"token_type\":\"bearer\"," +
                        "\"access_token\":\"{0}\"," +
                        "\"expires_in\":{1}," +
                        "\"refresh_token\":\"{2}\"," +
                        "\"scope\":\"{3}\"," +
                        "\"account_id\":\"{4}\"," +
                        "\"linking_profile\":" +
                            "{{" +
                            "\"provider_name\":\"{5}\"," +
                            "\"profile_id\":\"{6}\"," +
                            "\"profile_name\":\"{7}\"" +
                            "}}" +
                        "}}",
                        accessToken, expiresIn, refreshToken, scope,
                        accountId, providerName, profileId, profileName)
            );

            var actualToken = client.GetTokenFromCode(oauthCode, redirectUri);
            var expectedToken = new OAuthToken(accessToken, refreshToken, expiresIn, scope.Split(new[] { ' ' }))
            {
                AccountId = accountId,
                LinkingProfile = new LinkingProfile()
                {
                    ProviderName = providerName,
                    Id = profileId,
                    Name = profileName,
                },
            };

            Assert.AreEqual(expectedToken, actualToken);
        }

        [Test]
        public void ExceptionWhenBadRequest()
        {
            http.Stub(
                HttpPost
                .Url("https://app.cronofy.com/oauth/token")
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"grant_type\":\"authorization_code\",\"code\":\"{2}\",\"redirect_uri\":\"{3}\"}}",
                    clientId, clientSecret, oauthCode, redirectUri)
                .ResponseCode(400)
                .ResponseBody("{\"error\":\"invalid_grant\"}")
            );

            Assert.Throws<CronofyResponseException>(() => client.GetTokenFromCode(oauthCode, redirectUri));
        }
    }
}
