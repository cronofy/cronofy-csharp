using System;
using NUnit.Framework;
using System.Collections.Generic;

namespace Cronofy.Test.CronofyOAuthClientTests
{
    [TestFixture]
    public sealed class GetAuthorizationUrl
    {
        private const string clientId = "abcdef123456";
        private const string clientSecret = "s3cr3t1v3";
        private const string redirectUri = "http://example.com/redirectUri";

        private CronofyOAuthClient client;

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyOAuthClient(clientId, clientSecret);
        }

        [Test]
        public void SpecifiedDataCentre()
        {
            this.client = new CronofyOAuthClient(clientId, clientSecret, "de");

            var authUrl = client.GetAuthorizationUrlBuilder(redirectUri).Build();
            var expectedAuthUrl = string.Format(
                "https://app-de.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                    "&redirect_uri={1}" +
                    "&scope=read_account%20read_events%20create_event%20delete_event",
                UrlBuilder.EncodeParameter(clientId),
                UrlBuilder.EncodeParameter(redirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void ExplicitDefaultDataCentre()
        {
            this.client = new CronofyOAuthClient(clientId, clientSecret, "us");

            var authUrl = client.GetAuthorizationUrlBuilder(redirectUri).Build();
            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                    "&redirect_uri={1}" +
                    "&scope=read_account%20read_events%20create_event%20delete_event",
                UrlBuilder.EncodeParameter(clientId),
                UrlBuilder.EncodeParameter(redirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void AlteredDefaultDataCentre()
        {
            var defaultDataCentre = Configuration.DefaultDataCentre;

            try
            {
                Configuration.DefaultDataCentre = DataCentre.German;

                this.client = new CronofyOAuthClient(clientId, clientSecret);

                var authUrl = client.GetAuthorizationUrlBuilder(redirectUri).Build();
                var expectedAuthUrl = string.Format(
                    "https://app-de.cronofy.com/oauth/authorize" +
                        "?client_id={0}" +
                        "&response_type=code" +
                        "&redirect_uri={1}" +
                        "&scope=read_account%20read_events%20create_event%20delete_event",
                    UrlBuilder.EncodeParameter(clientId),
                    UrlBuilder.EncodeParameter(redirectUri));

                Assert.AreEqual(expectedAuthUrl, authUrl);
            }
            finally
            {
                Configuration.DefaultDataCentre = defaultDataCentre;
            }
        }

        [Test]
        public void HasDefaultScope()
        {
            var authUrl = client.GetAuthorizationUrlBuilder(redirectUri).Build();
            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                    "&redirect_uri={1}" +
                    "&scope=read_account%20read_events%20create_event%20delete_event",
                UrlBuilder.EncodeParameter(clientId),
                UrlBuilder.EncodeParameter(redirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void CanOverrideScope()
        {
            var authUrl = client.GetAuthorizationUrlBuilder(redirectUri)
                .Scope("read_account", "read_events")
                .Build();

            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                    "&redirect_uri={1}" +
                    "&scope=read_account%20read_events",
                UrlBuilder.EncodeParameter(clientId),
                UrlBuilder.EncodeParameter(redirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void CanOverrideScopeWithEnumerable()
        {
            IEnumerable<string> scope = new List<string> {
                "read_account",
                "read_events"
            };

            var authUrl = client.GetAuthorizationUrlBuilder(redirectUri)
                .Scope(scope)
                .Build();

            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                    "&redirect_uri={1}" +
                    "&scope=read_account%20read_events",
                UrlBuilder.EncodeParameter(clientId),
                UrlBuilder.EncodeParameter(redirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void CanProvideState()
        {
            const string someState = "xyz789";

            var authUrl = client.GetAuthorizationUrlBuilder(redirectUri)
                .State(someState)
                .Build();

            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                    "&redirect_uri={1}" +
                    "&scope=read_account%20read_events%20create_event%20delete_event" +
                    "&state={2}",
                UrlBuilder.EncodeParameter(clientId),
                UrlBuilder.EncodeParameter(redirectUri),
                someState);

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void CanSetAvoidLinkingToTrue()
        {
            var authUrl = client.GetAuthorizationUrlBuilder(redirectUri)
                .AvoidLinking(true)
                .Build();

            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                    "&redirect_uri={1}" +
                    "&scope=read_account%20read_events%20create_event%20delete_event" +
                    "&avoid_linking=true",
                UrlBuilder.EncodeParameter(clientId),
                UrlBuilder.EncodeParameter(redirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void CanSetAvoidLinkingToFalse()
        {
            var authUrl = client.GetAuthorizationUrlBuilder(redirectUri)
                .AvoidLinking(false)
                .Build();

            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                     "&redirect_uri={1}" +
                    "&scope=read_account%20read_events%20create_event%20delete_event" +
                    "&avoid_linking=false",
                UrlBuilder.EncodeParameter(clientId),
                UrlBuilder.EncodeParameter(redirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void ToStringGeneratesUrl()
        {
            var builder = client.GetAuthorizationUrlBuilder(redirectUri);

            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                    "&redirect_uri={1}" +
                    "&scope=read_account%20read_events%20create_event%20delete_event",
                UrlBuilder.EncodeParameter(clientId),
                UrlBuilder.EncodeParameter(redirectUri));

            Assert.AreEqual(expectedAuthUrl, builder.ToString());
        }

        [Test]
        public void CanSetUrlAsEnterpriseConnect()
        {
            var authUrl = client.GetAuthorizationUrlBuilder(redirectUri)
                      .EnterpriseConnect()
                      .Build();

            var expectedAuthUrl = string.Format(
              "https://app.cronofy.com/enterprise_connect/oauth/authorize" +
              "?client_id={0}" +
              "&response_type=code" +
              "&redirect_uri={1}" +
              "&delegated_scope=read_account%20read_events%20create_event%20delete_event" +
              "&scope=service_account%2Faccounts%2Fmanage%20service_account%2Fresources%2Fmanage",
              UrlBuilder.EncodeParameter(clientId),
              UrlBuilder.EncodeParameter(redirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void CanOverrideEnterpriseConnectScope()
        {
            var authUrl = client.GetAuthorizationUrlBuilder(redirectUri)
                .EnterpriseConnect()
              .EnterpriseConnectScope("service_account/accounts/unrestricted_access", "service_account/resources/unrestricted_access")
              .Build();

            var expectedAuthUrl = string.Format(
              "https://app.cronofy.com/enterprise_connect/oauth/authorize" +
              "?client_id={0}" +
              "&response_type=code" +
              "&redirect_uri={1}" +
              "&delegated_scope=read_account%20read_events%20create_event%20delete_event" +
              "&scope=service_account%2Faccounts%2Funrestricted_access%20service_account%2Fresources%2Funrestricted_access",
              UrlBuilder.EncodeParameter(clientId),
              UrlBuilder.EncodeParameter(redirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void CanOverrideEnterpriseConnectScopeWithEnumerable()
        {
            IEnumerable<string> scope = new List<string> {
                "service_account/accounts/unrestricted_access",
                "service_account/resources/unrestricted_access"
            };

            var authUrl = client.GetAuthorizationUrlBuilder(redirectUri)
              .EnterpriseConnectScope(scope)
              .EnterpriseConnect()
              .Build();

            var expectedAuthUrl = string.Format(
              "https://app.cronofy.com/enterprise_connect/oauth/authorize" +
              "?client_id={0}" +
              "&response_type=code" +
              "&redirect_uri={1}" +
              "&delegated_scope=read_account%20read_events%20create_event%20delete_event" +
              "&scope=service_account%2Faccounts%2Funrestricted_access%20service_account%2Fresources%2Funrestricted_access",
              UrlBuilder.EncodeParameter(clientId),
              UrlBuilder.EncodeParameter(redirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void CanSetLinkToken()
        {
            const string LinkToken = "LegendOfZelda";

            var authUrl = client.GetAuthorizationUrlBuilder(redirectUri)
                .LinkToken(LinkToken)
                .Build();

            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                     "&redirect_uri={1}" +
                    "&scope=read_account%20read_events%20create_event%20delete_event" +
                    "&link_token={2}",
                UrlBuilder.EncodeParameter(clientId),
                UrlBuilder.EncodeParameter(redirectUri),
                UrlBuilder.EncodeParameter(LinkToken));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }
    }
}
