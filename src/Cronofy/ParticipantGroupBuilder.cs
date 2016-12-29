namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Requests;

    /// <summary>
    /// Class to build <see cref="AvailabilityRequest.ParticipantGroup"/>s.
    /// </summary>
    public sealed class ParticipantGroupBuilder : IBuilder<AvailabilityRequest.ParticipantGroup>
    {
        /// <summary>
        /// The subjects of the participants for the group.
        /// </summary>
        private readonly List<string> subs = new List<string>();

        /// <summary>
        /// The required attribute for the group.
        /// </summary>
        private object required;

        /// <summary>
        /// Adds a participant to the group.
        /// </summary>
        /// <param name="sub">
        /// The sub of the participant, must not be blank.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="ParticipantGroupBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="sub"/> is blank.
        /// </exception>
        public ParticipantGroupBuilder AddParticipant(string sub)
        {
            Preconditions.NotBlank("sub", sub);

            this.subs.Add(sub);

            return this;
        }

        /// <summary>
        /// Adds participants to the group.
        /// </summary>
        /// <param name="subs">
        /// The subs of the participants, must not be <code>null</code>.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="ParticipantGroupBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="subs"/> is <code>null</code>.
        /// </exception>
        public ParticipantGroupBuilder AddParticipants(IEnumerable<string> subs)
        {
            Preconditions.NotNull("subs", subs);

            this.subs.AddRange(subs);

            return this;
        }

        /// <summary>
        /// Sets the group as requiring all participants.
        /// </summary>
        /// <returns>
        /// A reference to the <see cref="ParticipantGroupBuilder"/>.
        /// </returns>
        public ParticipantGroupBuilder AllRequired()
        {
            this.required = "all";

            return this;
        }

        /// <summary>
        /// Sets the group as requiring a specific number of participants.
        /// </summary>
        /// <param name="number">
        /// The number of participants from the group that are required, must be greater than zero.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="ParticipantGroupBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="number"/> is not greater than zero.
        /// </exception>
        public ParticipantGroupBuilder Required(int number)
        {
            Preconditions.True(number > 0, "number must be greater than zero");

            this.required = number;

            return this;
        }

        /// <inheritdoc />
        public AvailabilityRequest.ParticipantGroup Build()
        {
            return new AvailabilityRequest.ParticipantGroup
            {
                Required = this.required,
                Members = this.subs.Select(sub => new AvailabilityRequest.Member { Sub = sub }),
            };
        }
    }
}