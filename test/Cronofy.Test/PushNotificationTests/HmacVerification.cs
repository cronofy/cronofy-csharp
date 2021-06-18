namespace Cronofy.Test.PushNotificationTests
{
    using System.Text;
    using NUnit.Framework;

    [TestFixture]
    public sealed class HmacVerification
    {
        private const string ClientId = "abcdef123456";
        private const string ClientSecret = "pDY0Oi7TJSP2hfNmZNkm5";

        private const string RequestBody = "{\"example\":\"well-known\"}";

        private byte[] requestBytes = Encoding.UTF8.GetBytes(RequestBody);

        private CronofyOAuthClient client;

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyOAuthClient(ClientId, ClientSecret);
        }

        [Test]
        public void CanVerifyValidHmac()
        {
            var validHmac = "6r2/HjBkqymGegX0wOfifieeUXbbHwtV/LohHS+jv6c=";
            var result = this.client.HmacMatches(validHmac, this.requestBytes);
            Assert.IsTrue(result);
        }

        [Test]
        public void CanRejectInvalidHmac()
        {
            var invalidHmac = "PNNVJ9J2e8N174koxcKDzktZ9Qkt8YUzP+V+l5lG8F4=";
            var result = this.client.HmacMatches(invalidHmac, this.requestBytes);

            Assert.IsFalse(result);
        }

        [Test]
        public void CanVerifyMultiValueHmac()
        {
            var multiValueHmac = "PNNVJ9J2e8N174koxcKDzktZ9Qkt8YUzP+V+l5lG8F4=,6r2/HjBkqymGegX0wOfifieeUXbbHwtV/LohHS+jv6c=";
            var result = this.client.HmacMatches(multiValueHmac, this.requestBytes);

            Assert.IsTrue(result);
        }
    }
}
