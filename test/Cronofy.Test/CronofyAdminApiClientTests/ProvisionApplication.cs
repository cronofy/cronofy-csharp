namespace Cronofy.Test.CronofyAdminApiClientTests
{
    using NUnit.Framework;

    [TestFixture]
    public sealed class ProvisionApplication
    {
        private const string ApiKey = "my-api-key";
        private const string ClientId = "abcdef123456";
        private const string ClientSecret = "s3cr3t1v3";

        private CronofyAdminApiClient client;
        private StubHttpClient http;

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyAdminApiClient(ApiKey);
            this.http = new StubHttpClient();

            this.client.HttpClient = this.http;
        }

        [Test]
        public void CanProvisionApplication()
        {
            this.http.Stub(HttpPost
                .Url("https://api.cronofy.com/v1/applications")
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestHeader("Authorization", $"Bearer {ApiKey}")
                .RequestBody(@"{""name"":""Initech Scheduler"",""url"":""https://initech.com""}")
                .ResponseCode(200)
                .ResponseBodyFormat(@"{{""oauth_client"":{{""client_id"":""{0}"",""client_secret"":""{1}""}}}}", ClientId, ClientSecret));

            var response = this.client.ProvisionApplication(new Requests.ProvisionApplicationRequest
            {
                Name = "Initech Scheduler",
                Url = "https://initech.com",
            });

            Assert.AreEqual(response.OAuthClient.ClientId, ClientId);
            Assert.AreEqual(response.OAuthClient.ClientSecret, ClientSecret);
        }
    }
}
