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

        public UpsertEventRequestBuilder EventId(string eventId)
        {
            Preconditions.NotEmpty("eventId", eventId);

            this.eventId = eventId;
            return this;
        }

        public UpsertEventRequestBuilder Summary(string summary)
        {
            this.summary = summary;
            return this;
        }

        public UpsertEventRequestBuilder Description(string description)
        {
            this.description = description;
            return this;
        }

        public UpsertEventRequestBuilder Start(DateTimeOffset start)
        {
            this.startTime = start;
            return this;
        }

        public UpsertEventRequestBuilder Start(int year, int month, int day, int hour, int minute)
        {
            return Start(new DateTimeOffset(year, month, day, hour, minute, 0, new TimeSpan(0)));
        }

        public UpsertEventRequestBuilder End(DateTimeOffset end)
        {
            this.endTime = end;
            return this;
        }

        public UpsertEventRequestBuilder End(int year, int month, int day, int hour, int minute)
        {
            return End(new DateTimeOffset(year, month, day, hour, minute, 0, new TimeSpan(0)));
        }

        public UpsertEventRequestBuilder Location(string location)
        {
            this.locationDescription = location;
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
