using System;
using NUnit.Framework;
using Cronofy.Requests;

namespace Cronofy.Test.CronofyOAuthClientTests
{
    [TestFixture]
    public sealed class RealTimeScheduling
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

        private string sub = "sub";
        private string calendarId = "calendarId";

        private UpsertEventRequest upsertEventRequestWithoutStartAndEnd;
        private AvailabilityRequest availabilityRequest;

        private CronofyOAuthClient client;
        private StubHttpClient http;

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyOAuthClient(clientId, clientSecret);
            this.http = new StubHttpClient();

            client.HttpClient = http;

            this.upsertEventRequestWithoutStartAndEnd = new UpsertEventRequestBuilder()
                .EventId(eventId)
                .Summary(summary)
                .Build();

            this.availabilityRequest = new AvailabilityRequestBuilder()
                .AddParticipantGroup(new ParticipantGroupBuilder().AddMember(sub))
                .AddAvailablePeriod(start, end)
                .RequiredDuration(60)
                .Build();
        }

        [Test]
        public void CanGetOAuthUrlWithAvailabilityTargetCalendarsAndHourFormat()
        {
            var expectedUrl = "http://test.com";
            var hourFormat = "H";

            http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/real_time_scheduling")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{" +
                            "\"client_id\":\"{0}\"," +
                            "\"client_secret\":\"{1}\"," +
                            "\"oauth\":{{" +
                                "\"redirect_uri\":\"{2}\"," +
                                "\"scope\":\"{3}\"" +
                            "}}," +
                            "\"event\":{{" +
                                "\"event_id\":\"{4}\"," +
                                "\"summary\":\"{5}\"" +
                            "}}," +
                            "\"availability\":{{" +
                                "\"participants\":[{{" +
                                    "\"members\":[{{" +
                                        "\"sub\":\"{6}\"" +
                                    "}}]" +
                                "}}]," +
                                "\"required_duration\":{{" +
                                    "\"minutes\":60" +
                                "}}," +
                                "\"available_periods\":[{{" +
                                    "\"start\":\"{7}\"," +
                                    "\"end\":\"{8}\"" +
                                "}}]" +
                            "}}," +
                            "\"target_calendars\":[{{" +
                                "\"sub\":\"{9}\"," +
                                "\"calendar_id\":\"{10}\"" +
                            "}}]," +
                            "\"formatting\":{{" +
                                "\"hour_format\":\"{11}\"" +
                            "}}," +
                            "\"tzid\":\"Etc/UTC\"" +
                        "}}",
                        clientId, clientSecret, redirectUrl, scope, eventId, summary, sub, startString, endString, sub, calendarId, hourFormat)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        "{{\"url\":\"{0}\"}}", expectedUrl)
            );

            var request = new RealTimeSchedulingRequestBuilder()
                .OAuthDetails(redirectUrl, scope)
                .Timezone("Etc/UTC")
                .UpsertEventRequest(upsertEventRequestWithoutStartAndEnd)
                .AvailabilityRequest(availabilityRequest)
                .AddTargetCalendar(sub, calendarId)
                .HourFormat("H")
                .Build();

            var actualUrl = client.RealTimeScheduling(request);

            Assert.AreEqual(expectedUrl, actualUrl);
        }
    }
}
