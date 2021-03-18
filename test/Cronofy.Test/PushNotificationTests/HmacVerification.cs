namespace Cronofy.Test.PushNotificationTests
{
    using System.Text;
    using NUnit.Framework;

    [TestFixture]
    public sealed class HmacVerification
    {
        private const string ClientId = "abcdef123456";
        private const string ClientSecret = "pDY0Oi7TJSP2hfNmZNkm5";

        private CronofyOAuthClient client;
        private StubHttpClient http;

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyOAuthClient(ClientId, ClientSecret);
            this.http = new StubHttpClient();

            this.client.HttpClient = this.http;
        }

        [Test]
        public void CanVerifyHmac()
        {
            var encoding = Encoding.UTF8;
            var requestBytes = Encoding.UTF8.GetBytes("{\"example\":\"well-known\"}");

            Assert.IsTrue(this.client.HmacMatches("6r2/HjBkqymGegX0wOfifieeUXbbHwtV/LohHS+jv6c=", requestBytes));
        }
    }
}
