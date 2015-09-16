using NUnit.Framework;
using System;

namespace Cronofy.Test
{
	[TestFixture()]
	public class SmokeTest
	{
		[Test()]
		public void TestCase()
		{
			var client = new CronofyClient();

			Assert.IsTrue(client.SmokeTest());
		}
	}
}