namespace Cronofy.Test.CronofyOAuthClientTests
{
    using System;
    using Cronofy.Requests;
    using NUnit.Framework;

    [TestFixture]
    public sealed class AddToCalendar
    {
        private const string ClientId = "abcdef123456";
        private const string ClientSecret = "s3cr3t1v3";

        private string redirectUrl = "http://example.com/redirectUri";
        private string scope = "test_scope";
        private string state = "test_state";

        private string eventId = "testEventId";
        private string summary = "Test Summary";
        private string startString = "2014-08-05 15:30:00Z";
        private string endString = "2014-08-05 16:30:00Z";
        private DateTimeOffset start = new DateTime(2014, 8, 5, 15, 30, 0, DateTimeKind.Utc);
        private DateTimeOffset end = new DateTime(2014, 8, 5, 16, 30, 0, DateTimeKind.Utc);

        private UpsertEventRequest upsertEventRequest;

        private CronofyOAuthClient client;
        private StubHttpClient http;

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyOAuthClient(ClientId, ClientSecret);
            this.http = new StubHttpClient();

            this.client.HttpClient = this.http;

            this.upsertEventRequest = new UpsertEventRequestBuilder()
                .EventId(this.eventId)
                .Summary(this.summary)
                .Start(this.start)
                .End(this.end)
                .Build();
        }

        [Test]
        public void CanGetOAuthUrl()
        {
            var expectedUrl = "http://test.com";

            this.http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/add_to_calendar")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"oauth\":{{\"redirect_uri\":\"{2}\",\"scope\":\"{3}\",\"state\":\"{4}\"}},\"event\":{{\"event_id\":\"{5}\",\"summary\":\"{6}\",\"start\":{{\"time\":\"{7}\",\"tzid\":\"Etc/UTC\"}},\"end\":{{\"time\":\"{8}\",\"tzid\":\"Etc/UTC\"}}}}}}",
                        ClientId, ClientSecret, this.redirectUrl, this.scope, this.state, this.eventId, this.summary, this.startString, this.endString)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        "{{\"url\":\"{0}\"}}", expectedUrl));

            var addToCalendarRequest = new AddToCalendarRequestBuilder()
                .OAuthDetails(this.redirectUrl, this.scope, this.state)
                .UpsertEventRequest(this.upsertEventRequest)
                .Build();

            var actualUrl = this.client.AddToCalendar(addToCalendarRequest);

            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void CanGetOAuthUrlWithoutState()
        {
            var expectedUrl = "http://test.com";

            this.http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/add_to_calendar")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"oauth\":{{\"redirect_uri\":\"{2}\",\"scope\":\"{3}\"}},\"event\":{{\"event_id\":\"{4}\",\"summary\":\"{5}\",\"start\":{{\"time\":\"{6}\",\"tzid\":\"Etc/UTC\"}},\"end\":{{\"time\":\"{7}\",\"tzid\":\"Etc/UTC\"}}}}}}",
                        ClientId, ClientSecret, this.redirectUrl, this.scope, this.eventId, this.summary, this.startString, this.endString)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        "{{\"url\":\"{0}\"}}", expectedUrl));

            var addToCalendarRequest = new AddToCalendarRequestBuilder()
                .OAuthDetails(this.redirectUrl, this.scope)
                .UpsertEventRequest(this.upsertEventRequest)
                .Build();

            var actualUrl = this.client.AddToCalendar(addToCalendarRequest);

            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void CanGetUrlWithHourFormatting()
        {
            var expectedUrl = "http://test.com";
            var hourFormat = "h";

            this.http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/add_to_calendar")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"oauth\":{{\"redirect_uri\":\"{2}\",\"scope\":\"{3}\"}},\"event\":{{\"event_id\":\"{4}\",\"summary\":\"{5}\",\"start\":{{\"time\":\"{6}\",\"tzid\":\"Etc/UTC\"}},\"end\":{{\"time\":\"{7}\",\"tzid\":\"Etc/UTC\"}}}},\"formatting\":{{\"hour_format\":\"{8}\"}}}}",
                        ClientId, ClientSecret, this.redirectUrl, this.scope, this.eventId, this.summary, this.startString, this.endString, hourFormat)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        "{{\"url\":\"{0}\"}}", expectedUrl));

            var addToCalendarRequest = new AddToCalendarRequestBuilder()
                .OAuthDetails(this.redirectUrl, this.scope)
                .UpsertEventRequest(this.upsertEventRequest)
                .HourFormat("h")
                .Build();

            var actualUrl = this.client.AddToCalendar(addToCalendarRequest);

            Assert.AreEqual(expectedUrl, actualUrl);
        }
    }
}
