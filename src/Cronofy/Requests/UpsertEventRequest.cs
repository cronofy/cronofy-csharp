using System;
using Newtonsoft.Json;

namespace Cronofy.Requests
{
	public sealed class UpsertEventRequest
	{
		[JsonProperty("event_id")]
		public string EventId { get; set; }

		[JsonProperty("summary")]
		public string Summary { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("start")]
		[JsonConverter(typeof(EventTimeConverter))]
		public EventTime Start { get; set; }

		[JsonProperty("end")]
		[JsonConverter(typeof(EventTimeConverter))]
		public EventTime End { get; set; }

		[JsonProperty("location")]
		public RequestLocation Location { get; set; }

		public sealed class RequestLocation
		{
			[JsonProperty("description")]
			public string Description { get; set; }
		}
	}
}
