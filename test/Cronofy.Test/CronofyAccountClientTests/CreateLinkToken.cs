namespace Cronofy.Test.CronofyAccountClientTests
{
    using NUnit.Framework;

    internal sealed class CreateLinkToken : Base
    {
        [Test]
        public void CanCreateLinkToken()
        {
            const string LinkToken = "LegendOfZelda";

            this.Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/link_tokens")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .ResponseCode(200)
                .ResponseBodyFormat(@"{{""link_token"": ""{0}""}}", LinkToken));

            var actual = this.Client.CreateLinkToken();

            Assert.AreEqual(LinkToken, actual);
        }
    }
}
