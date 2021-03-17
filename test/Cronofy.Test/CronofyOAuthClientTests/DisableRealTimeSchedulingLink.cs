namespace Cronofy.Test.CronofyOAuthClientTests
{
    using NUnit.Framework;

    [TestFixture]
    public sealed class DisableRealTimeSchedulingLink
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
        public void CanDisableAnRTSLink()
        {
            var rtsId = "sch_123";
            var rtsUrl = "https://app.cronofy.com/add_to_calendar/scheduling/rtsToken";
            var displayMessage = "disabled";

            this.http.Stub(HttpPost
                .Url("https://api.cronofy.com/v1/real_time_scheduling/sch_123/disable")
                .RequestHeader("Authorization", string.Format("Bearer {0}", ClientSecret))
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    @"{{""display_message"":""{0}""}}", displayMessage)
                .ResponseCode(200)
                .ResponseBodyFormat(
                    @"{{""real_time_scheduling"":{{""real_time_scheduling_id"":""{0}"",""url"":""{1}"",""status"":""disabled"",""event"":{{""summary"":""event summary"",""event_id"":""event id"",""event_private"":false}}}}}}", rtsId, rtsUrl));

            var response = this.client.DisableRealTimeSchedulingLink(rtsId, displayMessage);
            var expectedResponse = new RealTimeSchedulingLinkStatus
            {
                RealTimeSchedulingId = rtsId,
                Status = RealTimeSchedulingLinkStatus.LinkStatus.Disabled,
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
