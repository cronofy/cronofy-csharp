using System;
using System.Collections.Generic;
using System.Linq;
using Cronofy.Requests;

namespace Cronofy
{
    /// <summary>
    /// Class to build a batch request.
    /// </summary>
    public sealed class BatchRequestBuilder : IBuilder<BatchRequest>
    {
        private readonly IList<IBuilder<BatchRequest.Entry>> batchEntryBuilders;

        /// <summary>
        /// Creates a new instance of <see cref="BatchRequestBuilder"/>.
        /// </summary>
        public BatchRequestBuilder()
        {
            this.batchEntryBuilders = new List<IBuilder<BatchRequest.Entry>>();
        }

        /// <summary>
        /// Add an event upsert to the batch.
        /// </summary>
        /// <param name="calendarId">
        /// The ID of the calendar the event should be upserted to, must not be
        /// empty.
        /// </param>
        /// <param name="eventBuilder">
        /// The builder from which to create a <see cref="UpsertEventRequest"/>
        /// entry, must not be null.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="calendarId"/> is empty or if
        /// <paramref name="eventBuilder"/> is null.
        /// </exception>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public BatchRequestBuilder UpsertEvent(string calendarId, IBuilder<UpsertEventRequest> eventBuilder)
        {
            return this.WithEntry(new UpsertEventEntryBuilder(calendarId, eventBuilder));
        }

        /// <summary>
        /// Add an event upsert to the batch.
        /// </summary>
        /// <param name="calendarId">
        /// The ID of the calendar the event should be upserted to, must not be
        /// empty.
        /// </param>
        /// <param name="eventRequest">
        /// The builder from which to create a <see cref="UpsertEventRequest"/>
        /// entry, must not be null.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="calendarId"/> is empty or if
        /// <paramref name="eventRequest"/> is null.
        /// </exception>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public BatchRequestBuilder UpsertEvent(string calendarId, UpsertEventRequest eventRequest)
        {
            Preconditions.NotNull("eventRequest", eventRequest);

            return this.WithEntry(new UpsertEventEntryBuilder(calendarId, Builder.Wrap(eventRequest)));
        }

        /// <summary>
        /// Adds an event delete to the batch.
        /// </summary>
        /// <param name="calendarId">
        /// The ID of the calendar the event should be deleted from, must not be
        /// empty.
        /// </param>
        /// <param name="eventId">
        /// The ID of the event to delete, must not be empty.
        /// </param>
        /// <returns></returns>
        public BatchRequestBuilder DeleteEvent(string calendarId, string eventId)
        {
            return this.WithEntry(new DeleteEventEntryBuilder(calendarId, eventId));
        }

        private class UpsertEventEntryBuilder : IBuilder<BatchRequest.Entry>
        {
            private readonly string calendarId;
            private readonly IBuilder<UpsertEventRequest> eventBuilder;

            public UpsertEventEntryBuilder(string calendarId, IBuilder<UpsertEventRequest> eventBuilder)
            {
                Preconditions.NotEmpty("calendarId", calendarId);
                Preconditions.NotNull("eventBuilder", eventBuilder);

                this.calendarId = calendarId;
                this.eventBuilder = eventBuilder;
            }

            public BatchRequest.Entry Build()
            {
                return new BatchRequest.EntryBuilder()
                    .Method("POST")
                    .RelativeUrlFormat("/v1/calendars/{0}/events", this.calendarId)
                    .Data(this.eventBuilder)
                    .Build();
            }
        }

        private class DeleteEventEntryBuilder : IBuilder<BatchRequest.Entry>
        {
            private readonly string calendarId;
            private readonly string eventId;

            public DeleteEventEntryBuilder(string calendarId, string eventId)
            {
                Preconditions.NotEmpty("calendarId", calendarId);
                Preconditions.NotEmpty("eventId", eventId);

                this.calendarId = calendarId;
                this.eventId = eventId;
            }

            public BatchRequest.Entry Build()
            {
                return new BatchRequest.EntryBuilder()
                    .Method("DELETE")
                    .RelativeUrlFormat("/v1/calendars/{0}/events", this.calendarId)
                    .Data(new { event_id = this.eventId })
                    .Build();
            }
        }

        /// <inheritdoc />
        public BatchRequest Build()
        {
            var request = new BatchRequest();

            request.Batch = this.batchEntryBuilders.Select(builder => builder.Build()).ToArray();

            return request;
        }

        private BatchRequestBuilder WithEntry(IBuilder<BatchRequest.Entry> entryBuilder)
        {
            this.batchEntryBuilders.Add(entryBuilder);

            return this;
        }
    }
}
