namespace Cronofy.Test.CronofyAccountClientTests
{
    using NUnit.Framework;

    internal sealed class RevokeProfileAuthorization : Base
    {
        private const string ProfileId = "pro_123456";

        [Test]
        public void CanRevokeProfileAuthorization()
        {
            this.Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/profiles/" + ProfileId + "/revoke")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .ResponseCode(202));

            this.Client.RevokeProfileAuthorization(ProfileId);
        }
    }
}
