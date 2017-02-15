using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyEnterpriseConnectAccountClientTests
{
    internal sealed class AuthorizeUser : Base
    {
        [Test]
        public void CanAuthorizeUser()
        {
            const string email = "test@cronofy.com";
            const string callbackUrl = "https://cronofy.com/test-callback";
            const string scopes = "read_account list_calendars read_events create_event delete_event read_free_busy";

            Http.Stub(
                HttpPost
                .Url("https://api.cronofy.com/v1/service_account_authorizations")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"email\":\"{0}\"," +
                    "\"callback_url\":\"{1}\"," +
                    "\"scope\":\"{2}\"" +
                    "}}",
                    email,
                    callbackUrl,
                    scopes)
                .ResponseCode(202)
            );

            Client.AuthorizeUser(email, callbackUrl, scopes);
        }

        [Test]
        public void CanAuthorizeUserForSpecificDataCentre()
        {
            this.Client = new CronofyEnterpriseConnectAccountClient(AccessToken, "de");
            Client.HttpClient = Http;

            const string email = "test@cronofy.com";
            const string callbackUrl = "https://cronofy.com/test-callback";
            const string scopes = "read_account list_calendars read_events create_event delete_event read_free_busy";

            Http.Stub(
                HttpPost
                .Url("https://api-de.cronofy.com/v1/service_account_authorizations")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"email\":\"{0}\"," +
                    "\"callback_url\":\"{1}\"," +
                    "\"scope\":\"{2}\"" +
                    "}}",
                    email,
                    callbackUrl,
                    scopes)
                .ResponseCode(202)
            );

            Client.AuthorizeUser(email, callbackUrl, scopes);
        }

        [Test]
        public void CanAuthorizeUserWithEnumerableScopes()
        {
            const string email = "test@test.com";
            const string callbackUrl = "https://test.com/test-callback";
            string[] scopes = { "read_account", "list_calendars", "read_events", "create_event", "delete_event", "read_free_busy" };

            Http.Stub(
                HttpPost
                .Url("https://api.cronofy.com/v1/service_account_authorizations")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"email\":\"{0}\"," +
                    "\"callback_url\":\"{1}\"," +
                    "\"scope\":\"{2}\"" +
                    "}}",
                    email,
                    callbackUrl,
                    String.Join(" ", scopes))
                .ResponseCode(202)
            );

            Client.AuthorizeUser(email, callbackUrl, scopes);
        }
    }
}
