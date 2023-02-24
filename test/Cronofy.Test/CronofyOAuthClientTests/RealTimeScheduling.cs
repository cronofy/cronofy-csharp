namespace Cronofy.Test.CronofyOAuthClientTests
{
    using System;
    using Cronofy.Requests;
    using NUnit.Framework;

    [TestFixture]
    public sealed class RealTimeScheduling
    {
        private const string ClientId = "abcdef123456";
        private const string ClientSecret = "s3cr3t1v3";

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
            this.client = new CronofyOAuthClient(ClientId, ClientSecret);
            this.http = new StubHttpClient();

            this.client.HttpClient = this.http;

            this.upsertEventRequestWithoutStartAndEnd = new UpsertEventRequestBuilder()
                .EventId(this.eventId)
                .Summary(this.summary)
                .Build();

            this.availabilityRequest = new AvailabilityRequestBuilder()
                .AddParticipantGroup(new ParticipantGroupBuilder().AddMember(this.sub))
                .AddAvailablePeriod(this.start, this.end)
                .RequiredDuration(60)
                .Build();
        }

        [Test]
        public void CanGetRTSUrlWithAvailabilityTargetCalendarsAndHourFormat()
        {
            var expectedUrl = "http://test.com";
            var hourFormat = "H";

            this.http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/real_time_scheduling")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{" +
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
                            "\"target_calendars\":[{{" +
                                "\"sub\":\"{9}\"," +
                                "\"calendar_id\":\"{10}\"" +
                            "}}]," +
                            "\"formatting\":{{" +
                                "\"hour_format\":\"{11}\"" +
                            "}}," +
                            "\"tzid\":\"Etc/UTC\"" +
                        "}}",
                        ClientId, ClientSecret, this.redirectUrl, this.scope, this.eventId, this.summary, this.sub, this.startString, this.endString, this.sub, this.calendarId, hourFormat)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        @"{{""url"": ""{0}"", ""real_time_scheduling"":{{""real_time_scheduling_id"":""sch_123"",""url"":""{0}"",""status"":""open"",""event"":{{""summary"":""{1}"",""event_id"":""{2}"",""event_private"":false}}}}}}", expectedUrl, this.summary, this.eventId));

            var request = new RealTimeSchedulingRequestBuilder()
                .OAuthDetails(this.redirectUrl, this.scope)
                .Timezone("Etc/UTC")
                .UpsertEventRequest(this.upsertEventRequestWithoutStartAndEnd)
                .AvailabilityRequest(this.availabilityRequest)
                .AddTargetCalendar(this.sub, this.calendarId)
                .HourFormat("H")
                .Build();

            var actualUrl = this.client.RealTimeScheduling(request);

            Assert.AreEqual(expectedUrl, actualUrl);
        }

        [Test]
        public void CanGetRTSUrlWithCallbackUrlAndCompletedUrl()
        {
            var expectedUrl = "http://test.com";
            var hourFormat = "H";
            var completedUrl = "https://test.com/completed_url";
            var callbackCompletedUrl = "https://test.com/callback_url";
            var noTimesSuitableUrl = "https://test.com/no_times_suitable_url";
            var noTimesDisplayedUrl = "http://test.com/no_times_displayed_url";

            this.http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/real_time_scheduling")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                        "{{" +
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
                            "\"redirect_urls\":{{" +
                                "\"completed_url\":\"{12}\"" +
                            "}}," +
                            "\"callback_urls\":{{" +
                                "\"completed_url\":\"{15}\"," +
                                "\"no_times_suitable_url\":\"{13}\"," +
                                "\"no_times_displayed_url\":\"{14}\"" +
                            "}}," +
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
                            "\"target_calendars\":[{{" +
                                "\"sub\":\"{9}\"," +
                                "\"calendar_id\":\"{10}\"" +
                            "}}]," +
                            "\"formatting\":{{" +
                                "\"hour_format\":\"{11}\"" +
                            "}}," +
                            "\"tzid\":\"Etc/UTC\"" +
                        "}}",
                        ClientId, ClientSecret, this.redirectUrl, this.scope, this.eventId, this.summary, this.sub, this.startString, this.endString, this.sub, this.calendarId, hourFormat, completedUrl, noTimesSuitableUrl, noTimesDisplayedUrl, callbackCompletedUrl)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        @"{{""url"": ""{0}"", ""real_time_scheduling"":{{""real_time_scheduling_id"":""sch_123"",""url"":""{0}"",""status"":""open"",""event"":{{""summary"":""{1}"",""event_id"":""{2}"",""event_private"":false}}}}}}", expectedUrl, this.summary, this.eventId));

            var request = new RealTimeSchedulingRequestBuilder()
                .OAuthDetails(this.redirectUrl, this.scope)
                .Timezone("Etc/UTC")
                .UpsertEventRequest(this.upsertEventRequestWithoutStartAndEnd)
                .AvailabilityRequest(this.availabilityRequest)
                .AddTargetCalendar(this.sub, this.calendarId)
                .HourFormat("H")
                .CallbackUrls(callbackCompletedUrl, noTimesSuitableUrl, noTimesDisplayedUrl)
                .RedirectUrls(completedUrl)
                .Build();

            var actualUrl = this.client.RealTimeScheduling(request);

            Assert.AreEqual(expectedUrl, actualUrl);
        }
    }
}
