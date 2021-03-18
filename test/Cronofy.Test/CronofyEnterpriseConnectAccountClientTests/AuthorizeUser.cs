namespace Cronofy.Test.CronofyEnterpriseConnectAccountClientTests
{
    using System;
    using NUnit.Framework;

    internal sealed class AuthorizeUser : Base
    {
        [Test]
        public void CanAuthorizeUser()
        {
            const string email = "test@cronofy.com";
            const string callbackUrl = "https://cronofy.com/test-callback";
            const string scopes = "read_account list_calendars read_events create_event delete_event read_free_busy";

            this.Http.Stub(
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
                .ResponseCode(202));

            this.Client.AuthorizeUser(email, callbackUrl, scopes);
        }

        [Test]
        public void CanAuthorizeUserWithState()
        {
            const string email = "test@cronofy.com";
            const string callbackUrl = "https://cronofy.com/test-callback";
            const string scopes = "read_account list_calendars read_events create_event delete_event read_free_busy";
            const string state = "customers-state-value";

            this.Http.Stub(
                HttpPost
                .Url("https://api.cronofy.com/v1/service_account_authorizations")
                .RequestHeader("Authorization", "Bearer " + AccessToken)
                .RequestHeader("Content-Type", "application/json; charset=utf-8")
                .RequestBodyFormat(
                    "{{\"email\":\"{0}\"," +
                    "\"callback_url\":\"{1}\"," +
                    "\"scope\":\"{2}\"," +
                    "\"state\":\"{3}\"" +
                    "}}",
                    email,
                    callbackUrl,
                    scopes,
                    state)
                .ResponseCode(202));

            this.Client.AuthorizeUser(email, callbackUrl, scopes, state);
        }

        [Test]
        public void CanAuthorizeUserForSpecificDataCenter()
        {
            this.Client = new CronofyEnterpriseConnectAccountClient(AccessToken, "de");
            this.Client.HttpClient = this.Http;

            const string email = "test@cronofy.com";
            const string callbackUrl = "https://cronofy.com/test-callback";
            const string scopes = "read_account list_calendars read_events create_event delete_event read_free_busy";

            this.Http.Stub(
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
                .ResponseCode(202));

            this.Client.AuthorizeUser(email, callbackUrl, scopes);
        }

        [Test]
        public void CanAuthorizeUserWithEnumerableScopes()
        {
            const string email = "test@test.com";
            const string callbackUrl = "https://test.com/test-callback";
            string[] scopes = { "read_account", "list_calendars", "read_events", "create_event", "delete_event", "read_free_busy" };

            this.Http.Stub(
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
                    string.Join(" ", scopes))
                .ResponseCode(202));

            this.Client.AuthorizeUser(email, callbackUrl, scopes);
        }
    }
}
