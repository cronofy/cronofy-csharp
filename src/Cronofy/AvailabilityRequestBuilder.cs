namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Requests;

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
        /// Thrown if <paramref name="builder"/> is <code>null</code>.
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
        /// Thrown if <paramref name="participantGroup"/> is <code>null</code>.
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
            var request = new AvailabilityRequest();

            request.RequiredDuration = this.GetRequiredDuration();

            request.AvailablePeriods = this.availablePeriods.ToArray();

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