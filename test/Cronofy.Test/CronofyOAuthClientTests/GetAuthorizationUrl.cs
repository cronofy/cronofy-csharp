namespace Cronofy.Test.CronofyOAuthClientTests
{
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public sealed class GetAuthorizationUrl
    {
        private const string ClientId = "abcdef123456";
        private const string ClientSecret = "s3cr3t1v3";
        private const string RedirectUri = "http://example.com/redirectUri";

        private CronofyOAuthClient client;

        [SetUp]
        public void SetUp()
        {
            this.client = new CronofyOAuthClient(ClientId, ClientSecret);
        }

        [Test]
        public void SpecifiedDataCenter()
        {
            this.client = new CronofyOAuthClient(ClientId, ClientSecret, "de");

            var authUrl = this.client.GetAuthorizationUrlBuilder(RedirectUri).Build();
            var expectedAuthUrl = string.Format(
                "https://app-de.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                    "&redirect_uri={1}" +
                    "&scope=read_account%20read_events%20create_event%20delete_event",
                UrlBuilder.EncodeParameter(ClientId),
                UrlBuilder.EncodeParameter(RedirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void ExplicitDefaultDataCenter()
        {
            this.client = new CronofyOAuthClient(ClientId, ClientSecret, "us");

            var authUrl = this.client.GetAuthorizationUrlBuilder(RedirectUri).Build();
            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                    "&redirect_uri={1}" +
                    "&scope=read_account%20read_events%20create_event%20delete_event",
                UrlBuilder.EncodeParameter(ClientId),
                UrlBuilder.EncodeParameter(RedirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void AlteredDefaultDataCenter()
        {
            var defaultDataCenter = Configuration.DefaultDataCenter;

            try
            {
                Configuration.DefaultDataCenter = "de";

                this.client = new CronofyOAuthClient(ClientId, ClientSecret);

                var authUrl = this.client.GetAuthorizationUrlBuilder(RedirectUri).Build();
                var expectedAuthUrl = string.Format(
                    "https://app-de.cronofy.com/oauth/authorize" +
                        "?client_id={0}" +
                        "&response_type=code" +
                        "&redirect_uri={1}" +
                        "&scope=read_account%20read_events%20create_event%20delete_event",
                    UrlBuilder.EncodeParameter(ClientId),
                    UrlBuilder.EncodeParameter(RedirectUri));

                Assert.AreEqual(expectedAuthUrl, authUrl);
            }
            finally
            {
                Configuration.DefaultDataCenter = defaultDataCenter;
            }
        }

        [Test]
        public void HasDefaultScope()
        {
            var authUrl = this.client.GetAuthorizationUrlBuilder(RedirectUri).Build();
            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                    "&redirect_uri={1}" +
                    "&scope=read_account%20read_events%20create_event%20delete_event",
                UrlBuilder.EncodeParameter(ClientId),
                UrlBuilder.EncodeParameter(RedirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void CanOverrideScope()
        {
            var authUrl = this.client.GetAuthorizationUrlBuilder(RedirectUri)
                .Scope("read_account", "read_events")
                .Build();

            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                    "&redirect_uri={1}" +
                    "&scope=read_account%20read_events",
                UrlBuilder.EncodeParameter(ClientId),
                UrlBuilder.EncodeParameter(RedirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void CanOverrideScopeWithEnumerable()
        {
            IEnumerable<string> scope = new List<string>
            {
                "read_account",
                "read_events",
            };

            var authUrl = this.client.GetAuthorizationUrlBuilder(RedirectUri)
                .Scope(scope)
                .Build();

            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                    "&redirect_uri={1}" +
                    "&scope=read_account%20read_events",
                UrlBuilder.EncodeParameter(ClientId),
                UrlBuilder.EncodeParameter(RedirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void CanProvideState()
        {
            const string someState = "xyz789";

            var authUrl = this.client.GetAuthorizationUrlBuilder(RedirectUri)
                .State(someState)
                .Build();

            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                    "&redirect_uri={1}" +
                    "&scope=read_account%20read_events%20create_event%20delete_event" +
                    "&state={2}",
                UrlBuilder.EncodeParameter(ClientId),
                UrlBuilder.EncodeParameter(RedirectUri),
                someState);

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void CanSetAvoidLinkingToTrue()
        {
            var authUrl = this.client.GetAuthorizationUrlBuilder(RedirectUri)
                .AvoidLinking(true)
                .Build();

            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                    "&redirect_uri={1}" +
                    "&scope=read_account%20read_events%20create_event%20delete_event" +
                    "&avoid_linking=true",
                UrlBuilder.EncodeParameter(ClientId),
                UrlBuilder.EncodeParameter(RedirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void CanSetAvoidLinkingToFalse()
        {
            var authUrl = this.client.GetAuthorizationUrlBuilder(RedirectUri)
                .AvoidLinking(false)
                .Build();

            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                     "&redirect_uri={1}" +
                    "&scope=read_account%20read_events%20create_event%20delete_event" +
                    "&avoid_linking=false",
                UrlBuilder.EncodeParameter(ClientId),
                UrlBuilder.EncodeParameter(RedirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void ToStringGeneratesUrl()
        {
            var builder = this.client.GetAuthorizationUrlBuilder(RedirectUri);

            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                    "&redirect_uri={1}" +
                    "&scope=read_account%20read_events%20create_event%20delete_event",
                UrlBuilder.EncodeParameter(ClientId),
                UrlBuilder.EncodeParameter(RedirectUri));

            Assert.AreEqual(expectedAuthUrl, builder.ToString());
        }

        [Test]
        public void CanSetUrlAsEnterpriseConnect()
        {
            var authUrl = this.client.GetAuthorizationUrlBuilder(RedirectUri)
                      .EnterpriseConnect()
                      .Build();

            var expectedAuthUrl = string.Format(
              "https://app.cronofy.com/enterprise_connect/oauth/authorize" +
              "?client_id={0}" +
              "&response_type=code" +
              "&redirect_uri={1}" +
              "&delegated_scope=read_account%20read_events%20create_event%20delete_event" +
              "&scope=service_account%2Faccounts%2Fmanage%20service_account%2Fresources%2Fmanage",
              UrlBuilder.EncodeParameter(ClientId),
              UrlBuilder.EncodeParameter(RedirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void CanOverrideEnterpriseConnectScope()
        {
            var authUrl = this.client.GetAuthorizationUrlBuilder(RedirectUri)
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
              UrlBuilder.EncodeParameter(ClientId),
              UrlBuilder.EncodeParameter(RedirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void CanOverrideEnterpriseConnectScopeWithEnumerable()
        {
            IEnumerable<string> scope = new List<string>
            {
                "service_account/accounts/unrestricted_access",
                "service_account/resources/unrestricted_access",
            };

            var authUrl = this.client.GetAuthorizationUrlBuilder(RedirectUri)
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
              UrlBuilder.EncodeParameter(ClientId),
              UrlBuilder.EncodeParameter(RedirectUri));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void CanSetLinkToken()
        {
            const string LinkToken = "LegendOfZelda";

            var authUrl = this.client.GetAuthorizationUrlBuilder(RedirectUri)
                .LinkToken(LinkToken)
                .Build();

            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                     "&redirect_uri={1}" +
                    "&scope=read_account%20read_events%20create_event%20delete_event" +
                    "&link_token={2}",
                UrlBuilder.EncodeParameter(ClientId),
                UrlBuilder.EncodeParameter(RedirectUri),
                UrlBuilder.EncodeParameter(LinkToken));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }

        [Test]
        public void CanSetProviderName()
        {
            const string ProviderName = "exchange";

            var authUrl = this.client.GetAuthorizationUrlBuilder(RedirectUri)
                .ProviderName(ProviderName)
                .Build();

            var expectedAuthUrl = string.Format(
                "https://app.cronofy.com/oauth/authorize" +
                    "?client_id={0}" +
                    "&response_type=code" +
                     "&redirect_uri={1}" +
                    "&scope=read_account%20read_events%20create_event%20delete_event" +
                    "&provider_name={2}",
                UrlBuilder.EncodeParameter(ClientId),
                UrlBuilder.EncodeParameter(RedirectUri),
                UrlBuilder.EncodeParameter(ProviderName));

            Assert.AreEqual(expectedAuthUrl, authUrl);
        }
    }
}
