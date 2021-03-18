namespace Cronofy.Test.CronofyAccountClientTests
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    internal sealed class GetProfiles : Base
    {
        [Test]
        public void CanGetCalendars()
        {
            this.Http.Stub(
                HttpGet
                .Url("https://api.cronofy.com/v1/profiles")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
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
}"));

            var profiles = this.Client.GetProfiles();

            CollectionAssert.AreEqual(
                new List<Profile>
                {
                    new Profile
                    {
                        ProviderName = "google",
                        Id = "pro_n23kjnwrw2",
                        Name = "example@cronofy.com",
                        Connected = true,
                    },
                    new Profile
                    {
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
