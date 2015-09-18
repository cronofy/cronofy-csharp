using System;
using NUnit.Framework;
using Newtonsoft.Json;

namespace Cronofy.Test.EventTimeTests
{
	[TestFixture]
	public sealed class JsonParsing
	{
		internal sealed class EventTimeHolder
		{
			[JsonProperty("event_time")]
			[JsonConverter(typeof(EventTimeConverter))]
			public EventTime EventTime { get; set; }
		}

		[Test]
		public void CanParseBasicTimeString()
		{
			const string json = @"{ ""event_time"": ""2015-09-19T12:30:45Z"" }";

			var result = JsonConvert.DeserializeObject<EventTimeHolder>(json, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });

			var expected = new EventTime(new DateTimeOffset(new DateTime(2015, 9, 19, 12, 30, 45), new TimeSpan(0)), "Etc/UTC");

			Assert.AreEqual(expected, result.EventTime);
		}

		[Test]
		public void CanParseBasicDateString()
		{
			const string json = @"{ ""event_time"": ""2014-09-13"" }";

			var result = JsonConvert.DeserializeObject<EventTimeHolder>(json, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });

			var expected = new EventTime(new Date(2014, 9, 13), "Etc/UTC");

			Assert.AreEqual(expected, result.EventTime);
		}

		[Test]
		public void CanParseLocalizedTimeString()
		{
			const string json = @"{ ""event_time"": { ""time"": ""2014-09-13T20:00:00+01:00"", ""tzid"": ""Europe/London"" } }";

			var result = JsonConvert.DeserializeObject<EventTimeHolder>(json, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });

			var expected = new EventTime(new DateTimeOffset(new DateTime(2014, 9, 13, 20, 0, 0), new TimeSpan(1, 0, 0)), "Europe/London");

			Assert.AreEqual(expected, result.EventTime);
		}

		[Test]
		public void CanParseLocalizedUtcTimeString()
		{
			const string json = @"{ ""event_time"": { ""time"": ""2014-09-13T20:00:00Z"", ""tzid"": ""Etc/UTC"" } }";

			var result = JsonConvert.DeserializeObject<EventTimeHolder>(json, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });

			var expected = new EventTime(new DateTimeOffset(new DateTime(2014, 9, 13, 20, 0, 0), new TimeSpan(0)), "Etc/UTC");

			Assert.AreEqual(expected, result.EventTime);
		}

		[Test]
		public void CanParseLocalizedDateString()
		{
			const string json = @"{ ""event_time"": { ""time"": ""2014-09-13"", ""tzid"": ""Europe/London"" } }";

			var result = JsonConvert.DeserializeObject<EventTimeHolder>(json, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });

			var expected = new EventTime(new Date(2014, 9, 13), "Europe/London");

			Assert.AreEqual(expected, result.EventTime);
		}
	}
}
