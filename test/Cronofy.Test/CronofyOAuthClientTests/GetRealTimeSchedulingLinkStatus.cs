namespace Cronofy.Test.CronofyOAuthClientTests
{
    using NUnit.Framework;

    [TestFixture]
    public sealed class GetRealTimeSchedulingLinkStatus
    {
        private const string ClientId = "abcdef123456";
        private const string ClientSecret = "s3cr3t1v3";

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
        public void CanGetRTSLinkStatus()
        {
            var rtsToken = "rtsToken";
            var rtsUrl = string.Format("https://app.cronofy.com/add_to_calendar/scheduling/{0}", rtsToken);

            this.http.Stub(HttpGet
                .Url(string.Format("https://api.cronofy.com/v1/real_time_scheduling?token={0}", rtsToken))
                .RequestHeader("Authorization", string.Format("Bearer {0}", ClientSecret))
                .ResponseCode(200)
                .ResponseBodyFormat(
                    @"{{""real_time_scheduling"":{{""real_time_scheduling_id"":""sch_123"",""url"":""{0}"",""status"":""open"",""event"":{{""summary"":""event summary"",""event_id"":""event id"",""event_private"":false}}}}}}", rtsUrl));

            var response = this.client.GetRealTimeSchedulingLinkStatus(rtsToken);
            var expectedResponse = new RealTimeSchedulingLinkStatus
            {
                RealTimeSchedulingId = "sch_123",
                Status = RealTimeSchedulingLinkStatus.LinkStatus.Open,
                Url = rtsUrl,
                Event = new Event
                {
                    Summary = "event summary",
                    EventId = "event id",
                    EventPrivate = false,
                },
            };

            Assert.AreEqual(response, expectedResponse);
        }

        [Test]
        public void CanGetRTSLinkStatusById()
        {
            var rtsId = "sch_123";
            var rtsUrl = "https://app.cronofy.com/add_to_calendar/scheduling/abc123";

            this.http.Stub(HttpGet
                .Url(string.Format("https://api.cronofy.com/v1/real_time_scheduling/{0}", rtsId))
                .RequestHeader("Authorization", string.Format("Bearer {0}", ClientSecret))
                .ResponseCode(200)
                .ResponseBodyFormat(
                    @"{{""real_time_scheduling"":{{""real_time_scheduling_id"":""{0}"",""url"":""{1}"",""status"":""open"",""event"":{{""summary"":""event summary"",""event_id"":""event id"",""event_private"":false}}}}}}", rtsId, rtsUrl));

            var response = this.client.GetRealTimeSchedulingLinkStatusById(rtsId);
            var expectedResponse = new RealTimeSchedulingLinkStatus
            {
                RealTimeSchedulingId = rtsId,
                Status = RealTimeSchedulingLinkStatus.LinkStatus.Open,
                Url = rtsUrl,
                Event = new Event
                {
                    Summary = "event summary",
                    EventId = "event id",
                    EventPrivate = false,
                },
            };

            Assert.AreEqual(response, expectedResponse);
        }
    }
}
