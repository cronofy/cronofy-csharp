using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyClient
{
	[TestFixture]
	public sealed class GetAuthorizationUrlTest
	{
		private const string clientId = "abcdef123456";
		private const string redirectUri = "http://example.com/redirectUri";

		[Test]
		public void HasDefaultScope()
		{
			var client = new Cronofy.CronofyClient(clientId);

			var authUrl = client.GetAuthorizationUrlBuilder(redirectUri).Build();
			var expectedAuthUrl = string.Format(
				"https://app.cronofy.com/oauth/authorize" +
					"?client_id={0}" +
					"&response_type=code" +
					"&scope=read_account%20read_events%20create_event%20delete_event" +
					"&redirect_uri={1}",
				clientId,
				redirectUri);

			Assert.AreEqual(expectedAuthUrl, authUrl);
		}

		[Test]
		public void CanOverrideScope()
		{
			var client = new Cronofy.CronofyClient(clientId);

			var authUrl = client.GetAuthorizationUrlBuilder(redirectUri)
				.Scope("read_account", "read_events")
				.Build();

			var expectedAuthUrl = string.Format(
				"https://app.cronofy.com/oauth/authorize" +
					"?client_id={0}" +
					"&response_type=code" +
					"&scope=read_account%20read_events" +
					"&redirect_uri={1}",
				clientId,
				redirectUri);

			Assert.AreEqual(expectedAuthUrl, authUrl);
		}

		[Test]
		public void CanProvideState()
		{
			const string someState = "xyz789";
			var client = new Cronofy.CronofyClient(clientId);

			var authUrl = client.GetAuthorizationUrlBuilder(redirectUri)
				.State(someState)
				.Build();

			var expectedAuthUrl = string.Format(
				"https://app.cronofy.com/oauth/authorize" +
					"?client_id={0}" +
					"&response_type=code" +
					"&scope=read_account%20read_events%20create_event%20delete_event" +
					"&redirect_uri={1}" +
					"&state={2}",
				clientId,
				redirectUri,
				someState);

			Assert.AreEqual(expectedAuthUrl, authUrl);
		}

		[Test]
		public void ToStringGeneratesUrl()
		{
			var client = new Cronofy.CronofyClient(clientId);

			var builder = client.GetAuthorizationUrlBuilder(redirectUri);

			var expectedAuthUrl = string.Format(
				"https://app.cronofy.com/oauth/authorize" +
					"?client_id={0}" +
					"&response_type=code" +
					"&scope=read_account%20read_events%20create_event%20delete_event" +
					"&redirect_uri={1}",
				clientId,
				redirectUri);

			Assert.AreEqual(expectedAuthUrl, builder.ToString());
		}
	}
}
