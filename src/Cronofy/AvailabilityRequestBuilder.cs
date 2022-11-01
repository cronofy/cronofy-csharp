namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cronofy.Requests;

    /// <summary>
    /// Class to build an availability request.
    /// </summary>
    public sealed class AvailabilityRequestBuilder : IBuilder<AvailabilityRequest>
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
        /// The before buffer of the request.
        /// </summary>
        private int? beforeBuffer;

        /// <summary>
        /// The after buffer of the request.
        /// </summary>
        private int? afterBuffer;

        /// <summary>
        /// The start interval of the request.
        /// </summary>
        private int? startInterval;

        /// <summary>
        /// The maximum number of available periods or slots of the request.
        /// </summary>
        private int? maxResults;

        /// <summary>
        /// Sets the maximum number of available periods or slots of the request.
        /// </summary>
        /// <param name="maxResults">
        /// The maximum number of available periods or slots, must be greater than zero and less than or equal to 512.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="AvailabilityRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="maxResults"/> is not greater than zero.
        /// Thrown if <paramref name="maxResults"/> is not less than or equal to 512.
        /// </exception>
        public AvailabilityRequestBuilder MaxResults(int maxResults)
        {
            Preconditions.True(maxResults > 0, "maxResults must be greater than zero");
            Preconditions.True(maxResults <= 512, "maxResults must be less than or equal to 512");

            this.maxResults = maxResults;

            return this;
        }

        /// <summary>
        /// Sets the required duration of the request.
        /// </summary>
        /// <param name="minutes">
        /// The number of minutes for the required duration, must be greater than zero.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="AvailabilityRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="minutes"/> is not greater than zero.
        /// </exception>
        public AvailabilityRequestBuilder RequiredDuration(int minutes)
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
        /// A reference to the <see cref="AvailabilityRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="minutes"/> is not greater than zero.
        /// </exception>
        public AvailabilityRequestBuilder StartInterval(int minutes)
        {
            Preconditions.True(minutes > 0, "minutes must be greater than zero");

            this.startInterval = minutes;

            return this;
        }

        /// <summary>
        /// Sets the before buffer of the request.
        /// </summary>
        /// <param name="beforeBuffer">
        /// The number of minutes for the before buffer.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="AvailabilityRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="beforeBuffer"/> is not null and is negative.
        /// </exception>
        public AvailabilityRequestBuilder BeforeBuffer(int? beforeBuffer)
        {
            if (beforeBuffer.HasValue)
            {
                Preconditions.True(beforeBuffer.Value >= 0, "buffer must be postive");
            }

            this.beforeBuffer = beforeBuffer;

            return this;
        }

        /// <summary>
        /// Sets the after buffer of the request.
        /// </summary>
        /// <param name="afterBuffer">
        /// The number of minutes for the after buffer.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="AvailabilityRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="afterBuffer"/> is not null or is negative.
        /// </exception>
        public AvailabilityRequestBuilder AfterBuffer(int? afterBuffer)
        {
            if (afterBuffer.HasValue)
            {
                Preconditions.True(afterBuffer.Value >= 0, "buffer must be postive");
            }

            this.afterBuffer = afterBuffer;

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
        /// A reference to the <see cref="AvailabilityRequestBuilder"/>.
        /// </returns>
        public AvailabilityRequestBuilder AddAvailablePeriod(DateTimeOffset start, DateTimeOffset end)
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
        /// A reference to the <see cref="AvailabilityRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="sub"/> is blank.
        /// </exception>
        public AvailabilityRequestBuilder AddRequiredParticipant(string sub)
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
        /// A reference to the <see cref="AvailabilityRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public AvailabilityRequestBuilder AddParticipantGroup(IBuilder<AvailabilityRequest.ParticipantGroup> builder)
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
        /// A reference to the <see cref="AvailabilityRequestBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="participantGroup"/> is <c>null</c>.
        /// </exception>
        public AvailabilityRequestBuilder AddParticipantGroup(AvailabilityRequest.ParticipantGroup participantGroup)
        {
            Preconditions.NotNull("participantGroup", participantGroup);

            this.groups.Add(participantGroup);

            return this;
        }

        /// <inheritdoc />
        public AvailabilityRequest Build()
        {
            var request = new AvailabilityRequest
            {
                RequiredDuration = this.GetRequiredDuration(),
                AvailablePeriods = this.availablePeriods.ToArray(),
            };

            var participantGroups = new List<AvailabilityRequest.ParticipantGroup>();

            if (this.requiredSubs.Count > 0)
            {
                var requiredGroup = new ParticipantGroupBuilder()
                    .AddMembers(this.requiredSubs)
                    .AllRequired();

                participantGroups.Add(requiredGroup.Build());
            }

            if (this.groups.Count > 0)
            {
                participantGroups.AddRange(this.groups);
            }

            if (this.groupBuilders.Count > 0)
            {
                participantGroups.AddRange(this.groupBuilders.Select(gb => gb.Build()));
            }

            if (this.startInterval.HasValue)
            {
                request.StartInterval = new AvailabilityRequest.Duration
                {
                    Minutes = this.startInterval.Value,
                };
            }

            if (this.beforeBuffer.HasValue || this.afterBuffer.HasValue)
            {
                request.Buffer = new AvailabilityRequest.Buffers();

                if (this.beforeBuffer.HasValue)
                {
                    request.Buffer.Before = new AvailabilityRequest.BufferDefintion
                    {
                        Minimum = new AvailabilityRequest.Duration
                        {
                            Minutes = this.beforeBuffer.Value,
                        },
                    };
                }

                if (this.afterBuffer.HasValue)
                {
                    request.Buffer.After = new AvailabilityRequest.BufferDefintion
                    {
                        Minimum = new AvailabilityRequest.Duration
                        {
                            Minutes = this.afterBuffer.Value,
                        },
                    };
                }
            }

            if (this.maxResults.HasValue)
            {
                request.MaxResults = this.maxResults.Value;
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
