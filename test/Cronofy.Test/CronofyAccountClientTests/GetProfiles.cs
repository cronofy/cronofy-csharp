using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Cronofy.Test.CronofyAccountClientTests
{
    [TestFixture]
    public sealed class GetProfiles
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
        public void CanGetCalendars()
        {
            http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/profiles")
                .RequestHeader("Authorization", "Bearer " + accessToken)
                .ResponseCode(200)
                .ResponseBody(
                    @"{
  ""profiles"": [
    {
      ""provider_name"": ""google"",
      ""profile_id"": ""pro_n23kjnwrw2"",
      ""profile_name"": ""example@cronofy.com"",
      ""profile_connected"": true
    },
    {
      ""provider_name"": ""apple"",
      ""profile_id"": ""pro_n23kjnwrw2"",
      ""profile_name"": ""example@cronofy.com"",
      ""profile_connected"": false,
      ""profile_relink_url"": ""http://to.cronofy.com/RaNggYu""
    }
  ]
}")
        );

            var profiles = client.GetProfiles();

            CollectionAssert.AreEqual(
                new List<Profile> {
                    new Profile {
                        ProviderName = "google",
                        Id = "pro_n23kjnwrw2",
                        Name = "example@cronofy.com",
                        Connected = true,
                    },
                    new Profile {
                        ProviderName = "apple",
                        Id = "pro_n23kjnwrw2",
                        Name = "example@cronofy.com",
                        Connected = false,
                        RelinkUrl = "http://to.cronofy.com/RaNggYu",
                    },
                },
                profiles.ToList());
        }
    }
}
