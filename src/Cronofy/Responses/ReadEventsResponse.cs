using System;
using Newtonsoft.Json;
using System.Threading;
using System.Linq;

namespace Cronofy.Responses
{
	internal sealed class ReadEventsResponse
	{
		[JsonProperty("events")]
		public EventResponse[] Events { get; set; }

		[JsonProperty("pages")]
		public PagesResponse Pages { get; set; }

		internal sealed class PagesResponse
		{
			[JsonProperty("current")]
			public int CurrentPage { get; set; }

			[JsonProperty("total")]
			public int TotalPages { get; set; }

			[JsonProperty("next_page")]
			public string NextPageUrl { get; set; }
		}

		internal sealed class EventResponse
		{
			[JsonProperty("calendar_id")]
			public string CalendarId { get; set; }

			[JsonProperty("event_id")]
			public string EventId { get; set; }

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

			[JsonProperty("attendees")]
			public AttendeeResponse[] Attendees { get; set; }

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

			internal sealed class AttendeeResponse
			{
				[JsonProperty("email")]
				public string Email { get; set; }

				[JsonProperty("display_name")]
				public string DisplayName { get; set; }

				[JsonProperty("status")]
				public string Status { get; set; }

				public Attendee ToAttendee()
				{
					return new Attendee {
						Email = Email,
						DisplayName = DisplayName,
						Status = Status,
					};
				}
			}

			public Event ToEvent()
			{
				var evt = new Event {
					CalendarId = CalendarId,
					EventId = EventId,
					EventUid = EventUid,
					Summary = Summary,
					Description = Description,
					Start = Start,
					End = End,
					Deleted = Deleted,
					ParticipationStatus = ParticipationStatus,
					Transparency = Transparency,
					EventStatus = EventStatus,
					Created = Created,
					Updated = Updated,
				};

				if (Location != null)
				{
					evt.Location = Location.ToLocation();
				}

				if (Attendees != null)
				{
					evt.Attendees = Attendees.Select(a => a.ToAttendee()).ToArray();
				}

				return evt;
			}
		}
	}
}
