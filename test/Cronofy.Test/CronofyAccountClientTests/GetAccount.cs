using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyAccountClientTests
{
    [TestFixture]
    public sealed class GetAccount
    {
        private const string accessToken = "zyxvut987654";

        private CronofyAccountClient client;
        private StubHttpClient http;

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyAccountClient(accessToken);
            this.http = new StubHttpClient();

            client.HttpClient = http;
        }

        [Test]
        public void CanGetAccount()
        {
            http.Stub(
                HttpGet
                    .Url("https://api.cronofy.com/v1/account")
                    .RequestHeader("Authorization", "Bearer " + accessToken)
                    .ResponseCode(200)
                    .ResponseBody(
                    @"{
  ""account"": {
    ""account_id"": ""acc_567236000909002"",
    ""email"": ""janed@company.com"",
    ""name"": ""Jane Doe"",
    ""default_tzid"": ""Europe/London""
  }
}")
            );

            var actualAccount = client.GetAccount();
            var expectedAccount = new Account
            {
                Id = "acc_567236000909002",
                Email = "janed@company.com",
                Name = "Jane Doe",
                DefaultTimeZoneId = "Europe/London",
            };

            Assert.AreEqual(expectedAccount, actualAccount);
        }
    }
}

