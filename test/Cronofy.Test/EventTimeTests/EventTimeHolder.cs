using System;
using Newtonsoft.Json;

namespace Cronofy.Test.EventTimeTests
{
	internal sealed class EventTimeHolder
	{
		[JsonProperty("event_time")]
		[JsonConverter(typeof(EventTimeConverter))]
		public EventTime EventTime { get; set; }
	}
}
