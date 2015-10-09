using System;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Newtonsoft.Json;

namespace Cronofy.Test.EventTimeTests
{
	[TestFixture]
	public sealed class JsonGeneration
	{
		[Test]
		public void CanGenerateTimeString()
		{
			const string expected = @"{""event_time"":{""time"":""2015-09-19 12:30:45Z"",""tzid"":""Etc/UTC""}}";

			var source = new EventTimeHolder {
				EventTime = new EventTime(new DateTimeOffset(new DateTime(2015, 9, 19, 12, 30, 45), new TimeSpan(0)), "Etc/UTC")
			};

			var result = JsonConvert.SerializeObject(source);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void CanGenerateTimeStringWithOffset()
		{
			const string expected = @"{""event_time"":{""time"":""2015-09-19 11:30:45Z"",""tzid"":""Europe/London""}}";

			var source = new EventTimeHolder {
				EventTime = new EventTime(new DateTimeOffset(new DateTime(2015, 9, 19, 12, 30, 45), new TimeSpan(1, 0, 0)), "Europe/London")
			};

			var result = JsonConvert.SerializeObject(source);

			Assert.AreEqual(expected, result);
		}

		[Test]
		public void CanGenerateDateString()
		{
			const string expected = @"{""event_time"":{""time"":""2015-09-19"",""tzid"":""Etc/UTC""}}";

			var source = new EventTimeHolder {
				EventTime = new EventTime(new Date(2015, 9, 19), "Etc/UTC")
			};

			var result = JsonConvert.SerializeObject(source);

			Assert.AreEqual(expected, result);
		}
	}
}
