using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyAccountClientTests
{
    [TestFixture]
    public sealed class DeleteEvent
    {
        private const string clientId = "abcdef123456";
        private const string clientSecret = "s3cr3t1v3";
        private const string accessToken = "zyxvut987654";

        private const string calendarId = "cal_123456_abcdef";

        private CronofyAccountClient client;
        private StubHttpClient http;

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyAccountClient(accessToken);
            this.http = new StubHttpClient();

            client.HttpClient = http;
        }

        [Test]
        public void CanDeleteEvent()
        {
            const string eventId = "qTtZdczOccgaPncGJaCiLg";

            http.Stub(
                HttpDelete
                    .Url("https://api.cronofy.com/v1/calendars/" + calendarId + "/events")
                    .RequestHeader("Authorization", "Bearer " + accessToken)
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(@"{{""event_id"":""{0}""}}", eventId)
                    .ResponseCode(202)
            );

            client.DeleteEvent(calendarId, eventId);
        }
    }
}
