using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyAccountClientTests
{
    internal sealed class CreateLinkToken : Base
    {
        [Test]
        public void CanCreateLinkToken()
        {
            const string LinkToken = "LegendOfZelda";

            Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/link_tokens")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .ResponseCode(200)
                .ResponseBodyFormat(@"{{""link_token"": ""{0}""}}", LinkToken)
            );

            var actual = Client.CreateLinkToken();

            Assert.AreEqual(LinkToken, actual);
        }
    }
}
