namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Requests;

    /// <summary>
    /// Class to build a batch request.
    /// </summary>
    public sealed class BatchRequestBuilder : IBuilder<BatchRequest>
    {
        /// <summary>
        /// The collected batch entry builders.
        /// </summary>
        private readonly IList<IBuilder<BatchRequest.Entry>> batchEntryBuilders;

        /// <summary>
        /// Initializes a new instance of the <see cref="BatchRequestBuilder"/>
        /// class.
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
            Preconditions.NotEmpty("calendarId", calendarId);
            Preconditions.NotNull("eventBuilder", eventBuilder);

            return this.WithEntry(
                new BatchRequest.EntryBuilder()
                    .Method("POST")
                    .RelativeUrlFormat("/v1/calendars/{0}/events", calendarId)
                    .Data(eventBuilder));
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

            return this.UpsertEvent(calendarId, Builder.Wrap(eventRequest));
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
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="calendarId"/> is empty or if
        /// <paramref name="eventId"/> is null.
        /// </exception>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public BatchRequestBuilder DeleteEvent(string calendarId, string eventId)
        {
            Preconditions.NotEmpty("calendarId", calendarId);
            Preconditions.NotEmpty("eventId", eventId);

            return this.WithEntry(
                new BatchRequest.EntryBuilder()
                    .Method("DELETE")
                    .RelativeUrlFormat("/v1/calendars/{0}/events", calendarId)
                    .Data(new DeleteEventRequest { EventId = eventId }));
        }

        /// <summary>
        /// Adds an external event delete to the batch.
        /// </summary>
        /// <param name="calendarId">
        /// The ID of the calendar the event should be deleted from, must not be
        /// empty.
        /// </param>
        /// <param name="eventUid">
        /// The UID of the event to delete, must not be empty.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="calendarId"/> is empty or if
        /// <paramref name="eventUid"/> is null.
        /// </exception>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public BatchRequestBuilder DeleteExternalEvent(string calendarId, string eventUid)
        {
            Preconditions.NotEmpty("calendarId", calendarId);
            Preconditions.NotEmpty("eventUid", eventUid);

            return this.WithEntry(
                new BatchRequest.EntryBuilder()
                    .Method("DELETE")
                    .RelativeUrlFormat("/v1/calendars/{0}/events", calendarId)
                    .Data(new DeleteExternalEventRequest { EventUid = eventUid }));
        }

        /// <inheritdoc />
        public BatchRequest Build()
        {
            var request = new BatchRequest();

            request.Batch = this.batchEntryBuilders.Select(builder => builder.Build()).ToArray();

            return request;
        }

        /// <summary>
        /// Adds an entry to the builder list.
        /// </summary>
        /// <param name="entryBuilder">
        /// The entry builder to add to the list.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        private BatchRequestBuilder WithEntry(IBuilder<BatchRequest.Entry> entryBuilder)
        {
            this.batchEntryBuilders.Add(entryBuilder);

            return this;
        }
    }
}
