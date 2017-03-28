using System;
using NUnit.Framework;
using Cronofy.Requests;

namespace Cronofy.Test.CronofyOAuthClientTests
{
    [TestFixture]
    public sealed class AddToCalendar
    {
        private const string clientId = "abcdef123456";
        private const string clientSecret = "s3cr3t1v3";

        private string redirectUrl = "http://example.com/redirectUri";
        private string scope = "test_scope";

        private string eventId = "testEventId";
        private string summary = "Test Summary";
        private string startString = "2014-08-05 15:30:00Z";
        private string endString = "2014-08-05 16:30:00Z";
        private DateTimeOffset start = new DateTime(2014, 8, 5, 15, 30, 0, DateTimeKind.Utc);
        private DateTimeOffset end = new DateTime(2014, 8, 5, 16, 30, 0, DateTimeKind.Utc);

        private UpsertEventRequest @event;

        private CronofyOAuthClient client;
        private StubHttpClient http;

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyOAuthClient(clientId, clientSecret);
            this.http = new StubHttpClient();

            client.HttpClient = http;

            this.@event = new UpsertEventRequestBuilder()
                .EventId(eventId)
                .Summary(summary)
                .Start(start)
                .End(end)
                .Build();
        }

        [Test]
        public void CanGetOAuthUrl()
        {
            var expectedUrl = "http://test.com";

            http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/add_to_calendar")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                    "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"oauth\":{{\"redirect_url\":\"{2}\",\"scope\":\"{3}\"}},\"event\":{{\"event_id\":\"{4}\",\"summary\":\"{5}\",\"start\":{{\"time\":\"{6}\",\"tzid\":\"Etc/UTC\"}},\"end\":{{\"time\":\"{7}\",\"tzid\":\"Etc/UTC\"}}}}}}",
                        clientId, clientSecret, redirectUrl, scope, eventId, summary, startString, endString)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        "{{\"oauth_url\":\"{0}\"}}", expectedUrl)
            );

            var addToCalendarRequest = new AddToCalendarRequestBuilder()
                .OAuth(redirectUrl, scope)
                .Event(@event)
                .Build();

            var actualUrl = client.AddToCalendar(addToCalendarRequest);

            Assert.AreEqual(expectedUrl, actualUrl);
        }
    }
}
