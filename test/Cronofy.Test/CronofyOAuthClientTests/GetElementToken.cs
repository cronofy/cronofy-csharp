using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyOAuthClientTests
{
    [TestFixture]
    public sealed class GetElementToken
    {
        private const string clientId = "abcdef123456";
        private const string clientSecret = "s3cr3t1v3";

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
        public void CanGetToken()
        {
            this.http.Stub(HttpPost
                .Url("https://api.cronofy.com/v1/element_tokens")
                .RequestHeader("Authorization", $"Bearer {clientSecret}")
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBody(@"{""permissions"":[""agenda"",""availability""],""subs"":[""acc_123"",""acc_456""],""origin"":""https://evenitron.com"",""version"":""1""}")
                .ResponseCode(200)
                .ResponseBody(@"{""element_token"":{""permissions"":[""agenda"",""availability""],""origin"":""https://evenitron.com"",""token"":""thetoken"",""expires_in"":64800}}"));

            var actualToken = this.client.GetElementToken(new ElementTokenRequest
            {
                Origin = "https://evenitron.com",
                Permissions = new[] { "agenda", "availability" },
                Subs = new[] { "acc_123", "acc_456" },
            });

            var expectedToken = new ElementToken("thetoken", "https://evenitron.com", new[] { "agenda", "availability" }, 64800);

            Assert.AreEqual(expectedToken, actualToken);
        }

        [Test]
        public void ExceptionWhenBadRequest()
        {
            http.Stub(
                HttpPost
                .Url("https://api.cronofy.com/v1/element_tokens")
                .RequestHeader("Authorization", $"Bearer {clientSecret}")
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBody(@"{""permissions"":[""all_the_things""],""subs"":[""acc_123"",""acc_456""],""origin"":""https://evenitron.com"",""version"":""1""}")
                .ResponseCode(400)
                .ResponseBody("{\"permissions\":\"not_recognized\"}")
            );

            Assert.Throws<CronofyResponseException>(() => client.GetElementToken(new ElementTokenRequest
            {
                Origin = "https://evenitron.com",
                Permissions = new[] { "all_the_things" },
                Subs = new[] { "acc_123", "acc_456" },
            }));
        }
    }
}
