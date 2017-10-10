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

        private class BatchEntryBuilder : IBuilder<BatchRequest.Entry>
        {
            public BatchRequest.Entry Build()
            {
                throw new NotImplementedException();
            }
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
        public BatchRequestBuilder UpsertEvent(string calendarId, UpsertEventRequestBuilder eventBuilder)
        {
            this.batchEntryBuilders.Add(new UpsertEventEntryBuilder(calendarId, eventBuilder));

            return this;
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
            this.batchEntryBuilders.Add(new UpsertEventEntryBuilder(calendarId, eventRequest));

            return this;
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

            public UpsertEventEntryBuilder(string calendarId, UpsertEventRequest eventRequest)
            {
                Preconditions.NotEmpty("calendarId", calendarId);
                Preconditions.NotNull("eventRequest", eventRequest);

                this.calendarId = calendarId;
                this.eventBuilder = Builder.Wrap(eventRequest);
            }

            public BatchRequest.Entry Build()
            {
                var entry = new BatchRequest.Entry();

                entry.Method = "POST";
                entry.RelativeUrl = string.Format("/v1/calendars/{0}/events", this.calendarId);
                entry.Data = this.eventBuilder.Build();

                return entry;
            }
        }

        /// <inheritdoc />
        public BatchRequest Build()
        {
            var request = new BatchRequest();

            request.Batch = this.batchEntryBuilders.Select(builder => builder.Build()).ToArray();

            return request;
        }
    }
}
