using System;
using NUnit.Framework;

namespace Cronofy.Test.CronofyClient
{
	[TestFixture]
	public sealed class GetAuthorizationUrlTest
	{
		private const string clientId = "abcdef123456";

		[Test]
		public void HasDefaultScope()
		{
			var client = new Cronofy.CronofyClient(clientId);

			var authUrl = client.GetAuthorizationUrlBuilder().Build();
			var expectedAuthUrl = string.Format(
				"https://app.cronofy.com/oauth/authorize" +
					"?client_id={0}" +
					"&scope=read_account%20read_events%20create_event%20delete_event",
				clientId);

			Assert.AreEqual(expectedAuthUrl, authUrl);
		}

		[Test]
		public void CanOverrideScope()
		{
			var client = new Cronofy.CronofyClient(clientId);

			var authUrl = client.GetAuthorizationUrlBuilder()
				.Scope("read_account", "read_events")
				.Build();

			var expectedAuthUrl = string.Format(
				"https://app.cronofy.com/oauth/authorize" +
				"?client_id={0}" +
				"&scope=read_account%20read_events",
				clientId);

			Assert.AreEqual(expectedAuthUrl, authUrl);
		}

		[Test]
		public void CanProvideState()
		{
			const string someState = "xyz789";
			var client = new Cronofy.CronofyClient(clientId);

			var authUrl = client.GetAuthorizationUrlBuilder()
				.State(someState)
				.Build();

			var expectedAuthUrl = string.Format(
				"https://app.cronofy.com/oauth/authorize" +
				"?client_id={0}" +
				"&scope=read_account%20read_events%20create_event%20delete_event" +
				"&state={1}",
				clientId,
				someState);

			Assert.AreEqual(expectedAuthUrl, authUrl);
		}

		[Test]
		public void ToStringGeneratesUrl()
		{
			var client = new Cronofy.CronofyClient(clientId);

			var builder = client.GetAuthorizationUrlBuilder();

			var expectedAuthUrl = string.Format(
				"https://app.cronofy.com/oauth/authorize" +
				"?client_id={0}" +
				"&scope=read_account%20read_events%20create_event%20delete_event",
				clientId);

			Assert.AreEqual(expectedAuthUrl, builder.ToString());
		}
	}
}
