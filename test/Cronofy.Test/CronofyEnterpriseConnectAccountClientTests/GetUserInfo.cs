using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyEnterpriseConnectAccountClientTests
{
    internal sealed class GetUserInfo : Base
    {
        [Test]
        public void CanGetUserInfo()
        {
            Http.Stub(
                HttpGet
                    .Url("https://api.cronofy.com/v1/userinfo")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .ResponseCode(200)
                    .ResponseBody(
                    @"{
  ""sub"": ""acc_567236000909002"",
  ""cronofy.type"": ""janed@company.com"",
}")
            );

            var actualUserInfo = Client.GetUserInfo();
            var expectedUserInfo = new UserInfo
            {
                Sub = "acc_567236000909002",
                CronofyType = "janed@company.com"
            };

            Assert.AreEqual(expectedUserInfo, actualUserInfo);
        }
    }
}
