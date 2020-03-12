namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Requests;

    /// <summary>
    /// Builder for Sequence request.
    /// </summary>
    public sealed class SequenceRequestBuilder : IBuilder<SequencedAvailabilityRequest.SequenceRequest>
    {
        /// <summary>
        /// The available periods for the request.
        /// </summary>
        private readonly IList<AvailabilityRequest.AvailablePeriod> availablePeriods =
            new List<AvailabilityRequest.AvailablePeriod>();

        /// <summary>
        /// The subjects of the required participants for the request.
        /// </summary>
        private readonly IList<string> requiredSubs = new List<string>();

        /// <summary>
        /// The builders for participant groups for the request.
        /// </summary>
        private readonly IList<IBuilder<AvailabilityRequest.ParticipantGroup>> groupBuilders =
            new List<IBuilder<AvailabilityRequest.ParticipantGroup>>();

        /// <summary>
        /// The groups for the request.
        /// </summary>
        private readonly IList<AvailabilityRequest.ParticipantGroup> groups =
            new List<AvailabilityRequest.ParticipantGroup>();

        /// <summary>
        /// The required duration of the request.
        /// </summary>
        private int requiredDuration;

        /// <summary>
        /// The ordinal of the request.
        /// </summary>
        private int? ordinal;

        /// <summary>
        /// The start interval of the request.
        /// </summary>
        private int? startInterval;

        /// <summary>
        /// The after buffer of the request.
        /// </summary>
        private AvailabilityRequest.BufferDefintion afterBuffer;

        /// <summary>
        /// The before buffer of the request.
        /// </summary>
        private AvailabilityRequest.BufferDefintion beforeBuffer;

        /// <summary>
        /// The sequence id of the request.
        /// </summary>
        private string sequenceId;

        /// <summary>
        /// The event request.
        /// </summary>
        private UpsertEventRequest eventRequest;

        /// <summary>
        /// Sets the required duration of the request.
        /// </summary>
        /// <param name="minutes">
        /// The number of minutes for the required duration, must be greater than zero.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="SequenceRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="minutes"/> is not greater than zero.
        /// </exception>
        public SequenceRequestBuilder RequiredDuration(int minutes)
        {
            Preconditions.True(minutes > 0, "minutes must be greater than zero");

            this.requiredDuration = minutes;

            return this;
        }

        /// <summary>
        /// Sets the start interval of the request.
        /// </summary>
        /// <param name="minutes">
        /// The number of minutes for the start interval, must be greater than zero.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="SequenceRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="minutes"/> is not greater than zero.
        /// </exception>
        public SequenceRequestBuilder StartInterval(int minutes)
        {
            Preconditions.True(minutes > 0, "minutes must be greater than zero");

            this.startInterval = minutes;

            return this;
        }

        /// <summary>
        /// Sets the before buffer of the request.
        /// </summary>
        /// <param name="minimum">
        /// The number of minutes for the minimum buffer.
        /// </param>
        /// <param name="maximum">
        /// The number of minutes for the maximum buffer.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="SequenceRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="minimum"/> is not null or is negative.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="maximum"/> is not null or is negative.
        /// </exception>
        public SequenceRequestBuilder BeforeBuffer(int? minimum, int? maximum = null)
        {
            if (minimum.HasValue)
            {
                Preconditions.True(minimum.Value >= 0, "minimum buffer must be postive");
            }

            if (maximum.HasValue)
            {
                Preconditions.True(maximum.Value >= 0, "maximum buffer must be postive");
            }

            this.beforeBuffer = new AvailabilityRequest.BufferDefintion();

            if (minimum.HasValue)
            {
                this.beforeBuffer.Minimum = new AvailabilityRequest.Duration
                {
                    Minutes = minimum.Value
                };
            }

            if (maximum.HasValue)
            {
                this.beforeBuffer.Maximum = new AvailabilityRequest.Duration
                {
                    Minutes = maximum.Value
                };
            }

            return this;
        }

        /// <summary>
        /// Sets the after buffer of the request.
        /// </summary>
        /// <param name="minimum">
        /// The number of minutes for the minimum buffer.
        /// </param>
        /// <param name="maximum">
        /// The number of minutes for the maximum buffer.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="SequenceRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="minimum"/> is not null or is negative.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="maximum"/> is not null or is negative.
        /// </exception>
        public SequenceRequestBuilder AfterBuffer(int? minimum, int? maximum = null)
        {
            if (minimum.HasValue)
            {
                Preconditions.True(minimum.Value >= 0, "minimum buffer must be postive");
            }

            if (maximum.HasValue)
            {
                Preconditions.True(maximum.Value >= 0, "maximum buffer must be postive");
            }

            this.afterBuffer = new AvailabilityRequest.BufferDefintion();

            if (minimum.HasValue)
            {
                this.afterBuffer.Minimum = new AvailabilityRequest.Duration
                {
                    Minutes = minimum.Value
                };
            }

            if (maximum.HasValue)
            {
                this.afterBuffer.Maximum = new AvailabilityRequest.Duration
                {
                    Minutes = maximum.Value
                };
            }

            return this;
        }

        /// <summary>
        /// Sets the ordinal of the request.
        /// </summary>
        /// <param name="ordinal">
        /// The ordinal for the request must be greater than zero.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="SequenceRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="ordinal"/> is not greater than zero.
        /// </exception>
        public SequenceRequestBuilder Ordinal(int ordinal)
        {
            Preconditions.True(ordinal > 0, "ordinal must be greater than zero");

            this.ordinal = ordinal;

            return this;
        }

        /// <summary>
        /// Sets the sequence id of the request.
        /// </summary>
        /// <param name="sequenceId">
        /// The sequenceId for the request.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="SequenceRequestBuilder"/>.
        /// </returns>
        public SequenceRequestBuilder SequenceId(string sequenceId)
        {
            this.sequenceId = sequenceId;
            return this;
        }

        /// <summary>
        /// Adds an available period to the request.
        /// </summary>
        /// <param name="start">
        /// The start of the available period.
        /// </param>
        /// <param name="end">
        /// The end of the available period.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="SequenceRequestBuilder"/>.
        /// </returns>
        public SequenceRequestBuilder AddAvailablePeriod(DateTimeOffset start, DateTimeOffset end)
        {
            var period = new AvailabilityRequest.AvailablePeriod
            {
                Start = start,
                End = end,
            };

            this.availablePeriods.Add(period);

            return this;
        }

        /// <summary>
        /// Adds a required participant to the request.
        /// </summary>
        /// <param name="sub">
        /// The sub of the required participant, must not be blank.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="SequenceRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="sub"/> is blank.
        /// </exception>
        public SequenceRequestBuilder AddRequiredParticipant(string sub)
        {
            Preconditions.NotBlank("sub", sub);

            this.requiredSubs.Add(sub);

            return this;
        }

        /// <summary>
        /// Adds a participant group to the request.
        /// </summary>
        /// <param name="builder">
        /// A builder for the group.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="SequenceRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="builder"/> is <code>null.</code>.
        /// </exception>
        public SequenceRequestBuilder AddParticipantGroup(IBuilder<AvailabilityRequest.ParticipantGroup> builder)
        {
            Preconditions.NotNull("builder", builder);

            this.groupBuilders.Add(builder);

            return this;
        }

        /// <summary>
        /// Adds a participant group to the request.
        /// </summary>
        /// <param name="participantGroup">
        /// A group.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="SequenceRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="participantGroup"/> is <code>null</code>.
        /// </exception>
        public SequenceRequestBuilder AddParticipantGroup(AvailabilityRequest.ParticipantGroup participantGroup)
        {
            Preconditions.NotNull("participantGroup", participantGroup);

            this.groups.Add(participantGroup);

            return this;
        }

        /// <summary>
        /// Sets the event on the builder.
        /// </summary>
        /// <returns>
        /// A reference to the <see cref="SequenceRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="eventRequest"/> is <code>null</code>.
        /// </exception>
        /// <param name="eventRequest">Event request.</param>
        public SequenceRequestBuilder Event(UpsertEventRequest eventRequest)
        {
            Preconditions.NotNull("eventRequest", eventRequest);

            this.eventRequest = eventRequest;

            return this;
        }

        /// <inheritdoc />
        public SequencedAvailabilityRequest.SequenceRequest Build()
        {
            var request = new SequencedAvailabilityRequest.SequenceRequest();

            request.RequiredDuration = this.GetRequiredDuration();
            request.AvailablePeriods = this.availablePeriods.ToArray();
            request.Ordinal = this.ordinal;
            request.SequenceId = this.sequenceId;

            var participantGroups = new List<AvailabilityRequest.ParticipantGroup>();

            if (this.requiredSubs.Count > 0)
            {
                var requiredGroup = new ParticipantGroupBuilder()
                    .AddMembers(this.requiredSubs)
                    .AllRequired();

                participantGroups.Add(requiredGroup.Build());
            }

            if (this.eventRequest != null)
            {
                request.Event = this.eventRequest;
            }

            if (this.groups.Count > 0)
            {
                participantGroups.AddRange(this.groups);
            }

            if (this.groupBuilders.Count > 0)
            {
                participantGroups.AddRange(this.groupBuilders.Select(gb => gb.Build()));
            }

            if (this.beforeBuffer != null || this.afterBuffer != null)
            {
                request.Buffer = new AvailabilityRequest.Buffers();

                if (this.beforeBuffer != null)
                {
                    request.Buffer.Before = this.beforeBuffer;
                }

                if (this.afterBuffer != null)
                {
                    request.Buffer.After = this.afterBuffer;
                }
            }

            if (this.startInterval.HasValue)
            {
                request.StartInterval = new AvailabilityRequest.Duration
                {
                    Minutes = this.startInterval.Value
                };
            }

            request.Participants = participantGroups;

            return request;
        }

        /// <summary>
        /// Gets the required duration as an
        /// <see cref="AvailabilityRequest.Duration"/>.
        /// </summary>
        /// <returns>
        /// The required duration as an
        /// <see cref="AvailabilityRequest.Duration"/>.
        /// </returns>
        private AvailabilityRequest.Duration GetRequiredDuration()
        {
            return new AvailabilityRequest.Duration { Minutes = this.requiredDuration };
        }
    }
}