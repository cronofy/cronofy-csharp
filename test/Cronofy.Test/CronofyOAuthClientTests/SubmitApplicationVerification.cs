using NUnit.Framework;

namespace Cronofy.Test.CronofyOAuthClientTests
{
    [TestFixture]
    public sealed class SubmitApplicationVerification
    {
        private const string clientId = "clientid123";
        private const string clientSecret = "s3cr31v3";

        private CronofyOAuthClient client;
        private StubHttpClient http;

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyOAuthClient(clientId, clientSecret);
            this.http = new StubHttpClient();

            client.HttpClient = this.http;
        }

        [Test]
        public void CanSendCompleteRequest()
        {
            this.http.Stub(HttpPost
                .Url("https://api.cronofy.com/v1/application_verification")
                .RequestHeader("Authorization", $"Bearer {clientSecret}")
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBody(@"{""redirect_uris"":[""https://app.samson.com""],""contact"":{""email"":""brock@samson.com"",""display_name"":""Brock Samson""}}")
                .ResponseCode(200)
                .ResponseBody("{}"));

            this.client.SubmitApplicationVerification(new ApplicationVerificationRequest
            {
                Contact = new ApplicationVerificationRequest.ContactDetails
                {
                    DisplayName = "Brock Samson",
                    Email = "brock@samson.com"
                },
                RedirectUris = new[]
                {
                    "https://app.samson.com"
                }
            });
        }

        [Test]
        public void CanSendNullDisplayName()
        {
            this.http.Stub(HttpPost
                .Url("https://api.cronofy.com/v1/application_verification")
                .RequestHeader("Authorization", $"Bearer {clientSecret}")
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBody(@"{""redirect_uris"":[""https://app.samson.com""],""contact"":{""email"":""brock@samson.com""}}")
                .ResponseCode(200)
                .ResponseBody("{}"));

            this.client.SubmitApplicationVerification(new ApplicationVerificationRequest
            {
                Contact = new ApplicationVerificationRequest.ContactDetails
                {
                    Email = "brock@samson.com"
                },
                RedirectUris = new[]
                {
                    "https://app.samson.com"
                }
            });
        }

        [Test]
        public void CanSendMultipleRedirectUris()
        {
            this.http.Stub(HttpPost
                .Url("https://api.cronofy.com/v1/application_verification")
                .RequestHeader("Authorization", $"Bearer {clientSecret}")
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBody(@"{""redirect_uris"":[""https://app.samson.com"",""https://app2.samson.com"",""https://app3.samson.com""],""contact"":{""email"":""brock@samson.com"",""display_name"":""Brock Samson""}}")
                .ResponseCode(200)
                .ResponseBody("{}"));

            this.client.SubmitApplicationVerification(new ApplicationVerificationRequest
            {
                Contact = new ApplicationVerificationRequest.ContactDetails
                {
                    DisplayName = "Brock Samson",
                    Email = "brock@samson.com"
                },
                RedirectUris = new[]
                {
                    "https://app.samson.com",
                    "https://app2.samson.com",
                    "https://app3.samson.com",
                }
            });
        }
    }
}
