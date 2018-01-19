using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Cronofy.Test.CronofyOAuthClientTests
{
    [TestFixture]
    public sealed class ApplicationCalendar
    {
        private const string clientId = "abcdef123456";
        private const string clientSecret = "s3cr3t1v3";
        private const string applicationCalendarId = "example-calendar-id";

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
        public void CanCreateApplicationCalendar()
        {
            const string accessToken = "asdnakjsdnas";
            const int expiresIn = 3600;
            const string refreshToken = "jerwpmsdkjngvdsk";
            const string scope = "read_events create_event delete_event";

            http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/application_calendar")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                    "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"application_calendar_id\":\"{2}\"}}",
                    clientId, clientSecret, applicationCalendarId)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        "{{\"token_type\":\"bearer\",\"access_token\":\"{0}\",\"expires_in\":{1},\"refresh_token\":\"{2}\",\"scope\":\"{3}\"}}",
                        accessToken, expiresIn, refreshToken, scope)
            );

            var actualToken = client.ApplicationCalendar(applicationCalendarId);
            var expectedToken = new OAuthToken(accessToken, refreshToken, expiresIn, scope.Split(new[] { ' ' }));

            Assert.AreEqual(expectedToken, actualToken);
        }
    }
}
