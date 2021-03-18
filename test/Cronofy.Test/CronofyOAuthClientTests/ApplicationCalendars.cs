namespace Cronofy.Test.CronofyOAuthClientTests
{
    using NUnit.Framework;

    [TestFixture]
    public sealed class ApplicationCalendars
    {
        private const string ClientId = "abcdef123456";
        private const string ClientSecret = "s3cr3t1v3";
        private const string ApplicationCalendarId = "example-calendar-id";

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
        public void CanCreateApplicationCalendar()
        {
            const string accessToken = "asdnakjsdnas";
            const int expiresIn = 3600;
            const string refreshToken = "jerwpmsdkjngvdsk";
            const string scope = "read_events create_event delete_event";

            this.http.Stub(
                HttpPost
                    .Url("https://api.cronofy.com/v1/application_calendars")
                    .RequestHeader("Content-Type", "application/json; charset=utf-8")
                    .RequestBodyFormat(
                    "{{\"client_id\":\"{0}\",\"client_secret\":\"{1}\",\"application_calendar_id\":\"{2}\"}}",
                    ClientId, ClientSecret, ApplicationCalendarId)
                    .ResponseCode(200)
                    .ResponseBodyFormat(
                        "{{\"token_type\":\"bearer\",\"access_token\":\"{0}\",\"expires_in\":{1},\"refresh_token\":\"{2}\",\"scope\":\"{3}\"}}",
                        accessToken, expiresIn, refreshToken, scope));

            var actualToken = this.client.ApplicationCalendar(ApplicationCalendarId);
            var expectedToken = new OAuthToken(accessToken, refreshToken, expiresIn, scope.Split(new[] { ' ' }));

            Assert.AreEqual(expectedToken, actualToken);
        }
    }
}
