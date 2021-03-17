namespace Cronofy.Test.PushNotificationTests
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using Cronofy.Requests;
    using Newtonsoft.Json;
    using NUnit.Framework;

    [TestFixture]
    public sealed class JsonParsing
    {
        private const string VerificationRequestBody = @"{
    ""notification"": {
        ""type"": ""verification""
    },
    ""channel"": {
        ""channel_id"": ""chn_54cf7c7cb4ad4c1027000001"",
        ""callback_url"": ""https://example.com/callback"",
        ""filters"": {
            ""calendar_ids"": [""cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw""]
        }
    }
}";

        private static readonly PushNotificationRequest ExpectedVerification
            = new PushNotificationRequest
            {
                Notification = new PushNotificationRequest.NotificationDetail
                {
                    Type = "verification",
                },
                Channel = new PushNotificationRequest.ChannelDetail
                {
                    Id = "chn_54cf7c7cb4ad4c1027000001",
                    CallbackUrl = "https://example.com/callback",
                    Filters = new PushNotificationRequest.ChannelDetail.ChannelFilters
                    {
                        CalendarIds = new[] { "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw" },
                    },
                },
            };

        private const string ChangeRequestBody = @"{
    ""notification"": {
        ""type"": ""change"",
        ""changes_since"": ""2015-11-13T10:39:12Z""
    },
    ""channel"": {
        ""channel_id"": ""chn_54cf7c7cb4ad4c1027000001"",
        ""callback_url"": ""https://example.com/callback"",
        ""filters"": {
            ""calendar_ids"": [""cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw""]
        }
    }
}";

        private static readonly PushNotificationRequest ExpectedChange
            = new PushNotificationRequest
            {
                Notification = new PushNotificationRequest.NotificationDetail
                {
                    Type = "change",
                    ChangesSince = new DateTime(2015, 11, 13, 10, 39, 12, DateTimeKind.Utc),
                },
                Channel = new PushNotificationRequest.ChannelDetail
                {
                    Id = "chn_54cf7c7cb4ad4c1027000001",
                    CallbackUrl = "https://example.com/callback",
                    Filters = new PushNotificationRequest.ChannelDetail.ChannelFilters
                    {
                        CalendarIds = new[] { "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw" },
                    },
                },
            };

        [Test]
        public void CanParseVerificationWithJsonNet()
        {
            var actualVerification = JsonConvert.DeserializeObject<PushNotificationRequest>(VerificationRequestBody);
            Assert.AreEqual(ExpectedVerification, actualVerification);
        }

        [Test]
        public void CanParseChangeWithJsonNet()
        {
            var actualChange = JsonConvert.DeserializeObject<PushNotificationRequest>(ChangeRequestBody);
            Assert.AreEqual(ExpectedChange, actualChange);
        }

        [Test]
        public void CanParseVerificationWithCoreSerialization()
        {
            var serializer = new DataContractJsonSerializer(typeof(PushNotificationRequest));

            var verificationBytes = Encoding.UTF8.GetBytes(VerificationRequestBody);
            var verificationStream = new MemoryStream(verificationBytes);

            var actualVerification = (PushNotificationRequest)serializer.ReadObject(verificationStream);
            Assert.AreEqual(ExpectedVerification, actualVerification);
        }

        [Test]
        public void CanParseChangeWithCoreSerialization()
        {
            var serializer = new DataContractJsonSerializer(typeof(PushNotificationRequest));

            var changeBytes = Encoding.UTF8.GetBytes(ChangeRequestBody);
            var changeStream = new MemoryStream(changeBytes);

            var actualChange = (PushNotificationRequest)serializer.ReadObject(changeStream);
            Assert.AreEqual(ExpectedChange, actualChange);
        }
    }
}
