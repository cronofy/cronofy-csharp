using System;
using NUnit.Framework;
using Cronofy.Requests;
using Newtonsoft.Json;

namespace Cronofy.Test.PushNotificationTests
{
    [TestFixture]
    public sealed class JsonParsing
    {
        private static readonly PushNotificationRequest expectedVerification
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
                }
            };

        private static readonly PushNotificationRequest expectedChange
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
                }
            };

        [Test]
        public void CanParseWithJsonNet()
        {
            const string verificationRequest = @"{
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

            var actualVerification = JsonConvert.DeserializeObject<PushNotificationRequest>(verificationRequest);

            Assert.AreEqual(expectedVerification, actualVerification);

            const string changeRequest = @"{
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

            var actualChange = JsonConvert.DeserializeObject<PushNotificationRequest>(changeRequest);

            Assert.AreEqual(expectedChange, actualChange);
        }
    }
}
