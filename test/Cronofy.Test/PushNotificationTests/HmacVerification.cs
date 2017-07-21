using System;
using NUnit.Framework;
using Cronofy.Requests;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace Cronofy.Test.PushNotificationTests
{
    [TestFixture]
    public sealed class HmacVerification
    {
        private const string clientId = "abcdef123456";
        private const string clientSecret = "pDY0Oi7TJSP2hfNmZNkm5";

        private CronofyOAuthClient client;
        private StubHttpClient http;

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyOAuthClient(clientId, clientSecret);
            this.http = new StubHttpClient();

            client.HttpClient = http;
        }

        [Test]
        public void CanVerifyHmac()
        {
            var encoding = Encoding.UTF8;
            var requestBytes = Encoding.UTF8.GetBytes("{\"example\":\"well-known\"}");

            Assert.IsTrue(client.HmacMatches("6r2/HjBkqymGegX0wOfifieeUXbbHwtV/LohHS+jv6c=", requestBytes));
        }
    }
}

