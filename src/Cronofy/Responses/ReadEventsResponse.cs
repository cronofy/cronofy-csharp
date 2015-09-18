using System;
using Newtonsoft.Json;

namespace Cronofy.Responses
{
	internal sealed class ReadEventsResponse
	{
		[JsonProperty("events")]
		public EventResponse[] Events { get; set; }

		internal sealed class EventResponse
		{
			[JsonProperty("calendar_id")]
			public string CalendarId { get; set; }

			[JsonProperty("event_uid")]
			public string EventUid { get; set; }

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
			public LocationResponse Location { get; set; }

			[JsonProperty("deleted")]
			public bool Deleted { get; set; }

			[JsonProperty("participation_status")]
			public string ParticipationStatus { get; set; }

			[JsonProperty("transparency")]
			public string Transparency { get; set; }

			[JsonProperty("event_status")]
			public string EventStatus { get; set; }

			[JsonProperty("created")]
			public DateTime Created { get; set; }

			[JsonProperty("updated")]
			public DateTime Updated { get; set; }

			internal sealed class LocationResponse
			{
				[JsonProperty("description")]
				public string Description { get; set; }

				public Location ToLocation()
				{
					return new Location {
						Description = Description,
					};
				}
			}

			public Event ToEvent()
			{
				return new Event {
					CalendarId = CalendarId,
					EventUid = EventUid,
					Summary = Summary,
					Description = Description,
					Start = Start,
					End = End,
					Location = Location.ToLocation(),
					Deleted = Deleted,
					ParticipationStatus = ParticipationStatus,
					Transparency = Transparency,
					EventStatus = EventStatus,
					Created = Created,
					Updated = Updated,
				};
			}
		}
	}
}
