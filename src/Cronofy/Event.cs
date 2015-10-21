using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;

namespace Cronofy
{
	public sealed class Event
	{
		public string CalendarId { get; set; }
		public string EventId { get; set; }
		public string EventUid { get; set; }
		public string Summary { get; set; }
		public string Description { get; set; }
		public EventTime Start { get; set; }
		public EventTime End { get; set; }
		public bool Deleted { get; set; }
		public Location Location { get; set; }
		public string ParticipationStatus { get; set; }
		public string Transparency { get; set; }
		public string EventStatus { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public Attendee[] Attendees { get; set; }

		public override int GetHashCode()
		{
			return this.EventUid.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var other = obj as Event;

			if (other == null)
			{
				return false;
			}

			return Equals(other);
		}

		public bool Equals(Event other)
		{
			return other != null
				&& this.EventUid == other.EventUid
				&& this.EventId == other.EventId
				&& this.CalendarId == other.CalendarId
				&& this.Created == other.Created
				&& this.Updated == other.Updated
				&& this.Deleted == other.Deleted
				&& this.Summary == other.Summary
				&& this.Description == other.Description
				&& this.ParticipationStatus == other.ParticipationStatus
				&& this.Transparency == other.Transparency
				&& this.EventStatus == other.EventStatus
				&& object.Equals(this.Location, other.Location)
				&& object.Equals(this.Start, other.Start)
				&& object.Equals(this.End, other.End)
				&& NullTolerantSequenceEqual(this.Attendees, other.Attendees);
		}

		public override string ToString()
		{
			return string.Format(
				"<{0} CalendarId={1}, EventId={2}, EventUid={3}, Summary={4}, Start={5}, End={6}, Deleted={7}, Attendees={8}>",
				GetType(), CalendarId, EventId, EventUid, Summary, Start, End, Deleted, Attendees);
		}

		private static bool NullTolerantSequenceEqual<T>(IEnumerable<T> left, IEnumerable<T> right)
		{
			if (left == null)
			{
				return right == null;
			}

			if (right == null)
			{
				return false;
			}

			return left.SequenceEqual(right);
		}
	}
}
