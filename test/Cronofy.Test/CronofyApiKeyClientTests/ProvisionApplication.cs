using NUnit.Framework;

namespace Cronofy.Test.CronofyApiKeyClientTests
{
    [TestFixture]
    public sealed class ProvisionApplication
    {
        private const string apiKey = "my-api-key";
        private const string clientId = "abcdef123456";
        private const string clientSecret = "s3cr3t1v3";

        private CronofyApiKeyClient client;
        private StubHttpClient http;

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyApiKeyClient(apiKey);
            this.http = new StubHttpClient();

            client.HttpClient = http;
        }

        [Test]
        public void CanProvisionApplication()
        {
            this.http.Stub(HttpPost
                .Url("https://api.cronofy.com/v1/applications")
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestHeader("Authorization", $"Bearer {apiKey}")
                .RequestBody(@"{""name"":""Initech Scheduler"",""url"":""https://initech.com""}")
                .ResponseCode(200)
                .ResponseBodyFormat(@"{{""oauth_client"":{{""client_id"":""{0}"",""client_secret"":""{1}""}}}}", clientId, clientSecret));

            var response = this.client.ProvisionApplication(new Requests.ProvisionApplicationRequest
            {
                Name = "Initech Scheduler",
                Url = "https://initech.com"
            });

            Assert.AreEqual(response.OAuthClient.ClientId, clientId);
            Assert.AreEqual(response.OAuthClient.ClientSecret, clientSecret);
        }
    }
}
