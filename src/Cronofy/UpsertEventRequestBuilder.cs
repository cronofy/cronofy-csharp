using System;
using Cronofy.Requests;

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

		public UpsertEventRequestBuilder End(DateTimeOffset value)
		{
			this.endTime = value;
			return this;
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
				Location = new Cronofy.Location {
					Description = this.locationDescription,
				},
			};
		}
	}
}
