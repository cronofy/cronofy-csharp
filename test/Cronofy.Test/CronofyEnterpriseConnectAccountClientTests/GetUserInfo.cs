namespace Cronofy.Test.CronofyEnterpriseConnectAccountClientTests
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
}"));

            var actualUserInfo = this.Client.GetUserInfo();
            var expectedUserInfo = new UserInfo
            {
                Sub = "acc_567236000909002",
                CronofyType = "janed@company.com",
            };

            Assert.AreEqual(expectedUserInfo, actualUserInfo);
        }
    }
}
