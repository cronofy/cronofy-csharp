using System;

namespace Cronofy
{
	public sealed class EventTime
	{
		private readonly Date date;
		private readonly DateTimeOffset dateTimeOffset;
		private readonly string timeZoneId;
		private readonly bool hasTime;

		public EventTime(Date date, string timeZoneId)
		{
			this.date = date;
			this.timeZoneId = timeZoneId;
			this.hasTime = false;
		}

		public EventTime(DateTimeOffset dateTimeOffset, string timeZoneId)
		{
			this.dateTimeOffset = dateTimeOffset;
			this.date = Date.From(dateTimeOffset);
			this.timeZoneId = timeZoneId;
			this.hasTime = true;
		}

		public bool HasTime
		{
			get
			{
				return this.hasTime;
			}
		}

		public Date Date
		{
			get
			{
				return this.date;
			}
		}

		public DateTimeOffset DateTimeOffset
		{
			get
			{
				Preconditions.True(this.hasTime, "This EventTime has no time component");
				return this.dateTimeOffset;
			}
		}

		public string TimeZoneId
		{
			get
			{
				return this.timeZoneId;
			}
		}

		public override int GetHashCode()
		{
			return this.date.GetHashCode() ^ this.dateTimeOffset.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var other = obj as EventTime;

			if (other == null)
			{
				return false;
			}

			return Equals(other);
		}

		public bool Equals(EventTime other)
		{
			return other != null
				&& this.dateTimeOffset == other.dateTimeOffset
				&& this.Date == other.Date
				&& this.TimeZoneId == other.TimeZoneId;
		}

		public override string ToString()
		{
			if (this.HasTime)
			{
				return string.Format("<EventTime DateTimeOffset={0}, TimeZoneId={1}>", DateTimeOffset, TimeZoneId);
			}

			return string.Format("<EventTime Date={0}, TimeZoneId={1}>", Date, TimeZoneId);
		}
	}
}
