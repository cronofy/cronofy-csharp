using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyAccountClientTests
{
    internal sealed class RevokeProfileAuthorization : Base
    {
        private const string profileId = "pro_123456";

        [Test]
        public void CanRevokeProfileAuthorization()
        {
            Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/profiles/" + profileId + "/revoke")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .ResponseCode(202)
            );

            Client.RevokeProfileAuthorization(profileId);
        }
    }
}
