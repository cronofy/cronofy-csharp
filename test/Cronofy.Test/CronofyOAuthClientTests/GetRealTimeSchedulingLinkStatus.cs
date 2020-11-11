using System;
using NUnit.Framework;
using Cronofy.Requests;

namespace Cronofy.Test.CronofyOAuthClientTests
{
    [TestFixture]
    public sealed class GetRealTimeSchedulingLinkStatus
    {
        private const string clientId = "abcdef123456";
        private const string clientSecret = "s3cr3t1v3";

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
        public void CanGetRTSLinkStatus()
        {
            var rtsToken = "rtsToken";
            var rtsUrl = string.Format("https://app.cronofy.com/add_to_calendar/scheduling/{0}", rtsToken);

            http.Stub(HttpGet
                .Url(string.Format("https://api.cronofy.com/v1/real_time_scheduling?token={0}", rtsToken))
                .RequestHeader("Authorization", string.Format("Bearer {0}", clientSecret))
                .ResponseCode(200)
                .ResponseBodyFormat(
                    @"{{""real_time_scheduling"":{{""real_time_scheduling_id"":""sch_123"",""url"":""{0}"",""status"":""open"",""event"":{{""summary"":""event summary"",""event_id"":""event id"",""event_private"":false}}}}}}", rtsUrl)
            );

            var response = client.GetRealTimeSchedulingLinkStatus(rtsToken);
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
                }
            };

            Assert.AreEqual(response, expectedResponse);
        }
    }
}
