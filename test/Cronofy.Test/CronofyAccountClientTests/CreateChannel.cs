namespace Cronofy.Test.CronofyAccountClientTests
{
    using NUnit.Framework;

    internal sealed class CreateChannel : Base
    {
        [Test]
        public void CanCreateChannel()
        {
            const string callbackUrl = "https://example.com/callback";

            this.Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/channels")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat("{{\"callback_url\":\"{0}\",\"filters\":{{}}}}", callbackUrl)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        @"{{
  ""channel"": {{
    ""channel_id"": ""chn_54cf7c7cb4ad4c1027000001"",
    ""callback_url"": ""{0}"",
    ""filters"": {{}}
  }}
}}", callbackUrl));

            var channel = this.Client.CreateChannel(callbackUrl);

            Assert.AreEqual(
                new Channel
                {
                    Id = "chn_54cf7c7cb4ad4c1027000001",
                    CallbackUrl = callbackUrl,
                    Filters = new Channel.ChannelFilters(),
                },
                channel);
        }

        [Test]
        public void CanCreateChannelForOnlyManagedEvents()
        {
            const string callbackUrl = "https://example.com/callback";

            this.Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/channels")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat("{{\"callback_url\":\"{0}\",\"filters\":{{\"only_managed\":true}}}}", callbackUrl)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        @"{{
  ""channel"": {{
    ""channel_id"": ""chn_54cf7c7cb4ad4c1027000001"",
    ""callback_url"": ""{0}"",
    ""filters"": {{
        ""only_managed"": true
    }}
  }}
}}", callbackUrl));

            var builder = new CreateChannelBuilder()
                .CallbackUrl(callbackUrl)
                .OnlyManaged(true);

            var channel = this.Client.CreateChannel(builder);

            Assert.AreEqual(
                new Channel
                {
                    Id = "chn_54cf7c7cb4ad4c1027000001",
                    CallbackUrl = callbackUrl,
                    Filters = new Channel.ChannelFilters
                    {
                        OnlyManaged = true,
                    },
                },
                channel);
        }

        [Test]
        public void CanCreateChannelForSpecificCalendar()
        {
            const string callbackUrl = "https://example.com/callback";
            const string calendarId = "cal_U9uuErStTG@EAAAB_IsAsykA2DBTWqQTf-f0kJw";

            this.Http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/channels")
                    .RequestHeader("Authorization", "Bearer " + AccessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{\"callback_url\":\"{0}\",\"filters\":{{\"calendar_ids\":[\"{1}\"]}}}}",
                        callbackUrl,
                        calendarId)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        @"{{
  ""channel"": {{
    ""channel_id"": ""chn_54cf7c7cb4ad4c1027000001"",
    ""callback_url"": ""{0}"",
    ""filters"": {{
        ""calendar_ids"": [""{1}""]
    }}
  }}
}}", callbackUrl, calendarId));

            var builder = new CreateChannelBuilder()
                .CallbackUrl(callbackUrl)
                .CalendarId(calendarId);

            var channel = this.Client.CreateChannel(builder);

            Assert.AreEqual(
                new Channel
                {
                    Id = "chn_54cf7c7cb4ad4c1027000001",
                    CallbackUrl = callbackUrl,
                    Filters = new Channel.ChannelFilters
                    {
                        CalendarIds = new[] { calendarId },
                    },
                },
                channel);
        }
    }
}
