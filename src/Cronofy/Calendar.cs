using System;

namespace Cronofy
{
	public sealed class Calendar
	{
		public Profile Profile { get; set; }
		public string CalendarId { get; set; }
		public string Name { get; set; }
		public bool ReadOnly { get; set; }
		public bool Deleted { get; set; }

		public override int GetHashCode()
		{
			return this.CalendarId.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var other = obj as Calendar;

			if (other == null)
			{
				return false;
			}

			return Equals(other);
		}

		public bool Equals(Calendar other)
		{
			return other != null
				&& this.CalendarId == other.CalendarId
				&& this.Name == other.Name
				&& this.ReadOnly == other.ReadOnly
				&& this.Deleted == other.Deleted
				&& object.Equals(this.Profile, other.Profile);
		}

		public override string ToString()
		{
			return string.Format(
				"<{0} Profile={1}, CalendarId={2}, Name={3}, ReadOnly={4}, Deleted={5}>",
				GetType(), Profile, CalendarId, Name, ReadOnly, Deleted);
		}
	}
}
