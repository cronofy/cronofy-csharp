using System;
using Cronofy.Requests;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Cronofy
{
	public sealed class UpsertEventRequestBuilder
	{
		private string eventId;
		private string summary;
		private string description;
		private DateTimeOffset startTime;
		private DateTimeOffset endTime;
		private string locationDescription;

		public UpsertEventRequestBuilder EventId(string value)
		{
			this.eventId = value;
			return this;
		}

		public UpsertEventRequestBuilder Summary(string value)
		{
			this.summary = value;
			return this;
		}

		public UpsertEventRequestBuilder Description(string value)
		{
			this.description = value;
			return this;
		}

		public UpsertEventRequestBuilder Start(DateTimeOffset value)
		{
			this.startTime = value;
			return this;
		}

		public UpsertEventRequestBuilder Start(int year, int month, int day, int hour, int minute)
		{
			return Start(new DateTimeOffset(year, month, day, hour, minute, 0, new TimeSpan(0)));
		}

		public UpsertEventRequestBuilder End(DateTimeOffset value)
		{
			this.endTime = value;
			return this;
		}

		public UpsertEventRequestBuilder End(int year, int month, int day, int hour, int minute)
		{
			return End(new DateTimeOffset(year, month, day, hour, minute, 0, new TimeSpan(0)));
		}

		public UpsertEventRequestBuilder Location(string value)
		{
			this.locationDescription = value;
			return this;
		}

		public UpsertEventRequest Build()
		{
			return new UpsertEventRequest {
				EventId = this.eventId,
				Summary = this.summary,
				Description = this.description,
				Start = new EventTime(this.startTime.ToUniversalTime(), "Etc/UTC"),
				End = new EventTime(this.endTime.ToUniversalTime(), "Etc/UTC"),
				Location = new UpsertEventRequest.RequestLocation {
					Description = this.locationDescription,
				},
			};
		}
	}
}
