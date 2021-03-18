namespace Cronofy.Test.CronofyOAuthClientTests
{
    using NUnit.Framework;

    [TestFixture]
    public sealed class SubmitApplicationVerification
    {
        private const string ClientId = "clientid123";
        private const string ClientSecret = "s3cr31v3";

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
        public void CanSendCompleteRequest()
        {
            this.http.Stub(HttpPost
                .Url("https://api.cronofy.com/v1/application_verification")
                .RequestHeader("Authorization", $"Bearer {ClientSecret}")
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBody(@"{""redirect_uris"":[""https://app.pinpoint.com""],""contact"":{""email"":""kenneth.calloway@pinpoint.com"",""display_name"":""Kenneth Calloway""}}")
                .ResponseCode(202)
                .ResponseBody("{}"));

            this.client.SubmitApplicationVerification(new ApplicationVerificationRequest
            {
                Contact = new ApplicationVerificationRequest.ContactDetails
                {
                    DisplayName = "Kenneth Calloway",
                    Email = "kenneth.calloway@pinpoint.com",
                },
                RedirectUris = new[]
                {
                    "https://app.pinpoint.com",
                },
            });
        }

        [Test]
        public void CanOmitDisplayName()
        {
            this.http.Stub(HttpPost
                .Url("https://api.cronofy.com/v1/application_verification")
                .RequestHeader("Authorization", $"Bearer {ClientSecret}")
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBody(@"{""redirect_uris"":[""https://app.pinpoint.com""],""contact"":{""email"":""kenneth.calloway@pinpoint.com""}}")
                .ResponseCode(202)
                .ResponseBody("{}"));

            this.client.SubmitApplicationVerification(new ApplicationVerificationRequest
            {
                Contact = new ApplicationVerificationRequest.ContactDetails
                {
                    Email = "kenneth.calloway@pinpoint.com",
                },
                RedirectUris = new[]
                {
                    "https://app.pinpoint.com",
                },
            });
        }

        [Test]
        public void CanSendMultipleRedirectUris()
        {
            this.http.Stub(HttpPost
                .Url("https://api.cronofy.com/v1/application_verification")
                .RequestHeader("Authorization", $"Bearer {ClientSecret}")
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBody(@"{""redirect_uris"":[""https://app.pinpoint.com"",""https://app2.pinpoint.com"",""https://app3.pinpoint.com""],""contact"":{""email"":""kenneth.calloway@pinpoint.com"",""display_name"":""Kenneth Calloway""}}")
                .ResponseCode(202)
                .ResponseBody("{}"));

            this.client.SubmitApplicationVerification(new ApplicationVerificationRequest
            {
                Contact = new ApplicationVerificationRequest.ContactDetails
                {
                    DisplayName = "Kenneth Calloway",
                    Email = "kenneth.calloway@pinpoint.com",
                },
                RedirectUris = new[]
                {
                    "https://app.pinpoint.com",
                    "https://app2.pinpoint.com",
                    "https://app3.pinpoint.com",
                },
            });
        }
    }
}
