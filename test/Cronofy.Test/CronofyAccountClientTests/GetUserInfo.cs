﻿namespace Cronofy.Test.CronofyAccountClientTests
{
    using NUnit.Framework;

    internal sealed class GetUserInfo : Base
    {
        [Test]
        public void CanGetUserInfo()
        {
            this.Http.Stub(
                HttpGet
                    .Url("https://api.cronofy.com/v1/userinfo")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .ResponseCode(200)
                    .ResponseBody(
                    @"{
  ""sub"": ""acc_567236000909002"",
  ""cronofy.type"": ""janed@company.com"",
  ""cronofy.data"": {
    ""profiles"": [
            {
                ""provider_name"": ""google"",
                ""profile_calendars"": [
                    {
                        ""calendar_integrated_conferencing_available"": ""true"",
                    }
                ]
            },
            {
                ""provider_name"": ""apple"",
                ""profile_calendars"": [
                    {
                        ""calendar_integrated_conferencing_available"": ""false"",
                    }
                ]
            }
        ]
    }
}"));

            var actualUserInfo = this.Client.GetUserInfo();
            var expectedUserInfo = new UserInfo
            {
                Sub = "acc_567236000909002",
                CronofyType = "janed@company.com",
                CalendarIntegratedConferencingAvailable = true,
                ProviderName = "google",
            };

            Assert.AreEqual(expectedUserInfo, actualUserInfo);
        }
    }
}
