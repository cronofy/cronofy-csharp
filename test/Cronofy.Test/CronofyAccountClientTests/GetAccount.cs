namespace Cronofy.Test.CronofyAccountClientTests
{
    using NUnit.Framework;

    internal sealed class GetAccount : Base
    {
        [Test]
        public void CanGetAccount()
        {
            this.Http.Stub(
                HttpGet
                    .Url("https://api.cronofy.com/v1/account")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .ResponseCode(200)
                    .ResponseBody(
                    @"{
  ""account"": {
    ""account_id"": ""acc_567236000909002"",
    ""email"": ""janed@company.com"",
    ""name"": ""Jane Doe"",
    ""default_tzid"": ""Europe/London"",
    ""scope"": ""read_events create_event delete_event""
  }
}"));

            var actualAccount = this.Client.GetAccount();
            var expectedAccount = new Account
            {
                Id = "acc_567236000909002",
                Email = "janed@company.com",
                Name = "Jane Doe",
                DefaultTimeZoneId = "Europe/London",
                Scope = new[] { "read_events", "create_event", "delete_event" },
            };

            Assert.AreEqual(expectedAccount, actualAccount);
        }

        [Test]
        public void CanGetAccountForSpecifiedDataCenter()
        {
            this.Client = new CronofyAccountClient(AccessToken, "de");
            this.Client.HttpClient = this.Http;

            this.Http.Stub(
                HttpGet
                    .Url("https://api-de.cronofy.com/v1/account")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .ResponseCode(200)
                    .ResponseBody(
                    @"{
  ""account"": {
    ""account_id"": ""acc_567236000909002"",
    ""email"": ""janed@company.com"",
    ""name"": ""Jane Doe"",
    ""default_tzid"": ""Europe/London"",
    ""scope"": ""read_events create_event delete_event""
  }
}"));

            var actualAccount = this.Client.GetAccount();
            var expectedAccount = new Account
            {
                Id = "acc_567236000909002",
                Email = "janed@company.com",
                Name = "Jane Doe",
                DefaultTimeZoneId = "Europe/London",
                Scope = new[] { "read_events", "create_event", "delete_event" },
            };

            Assert.AreEqual(expectedAccount, actualAccount);
        }
    }
}
