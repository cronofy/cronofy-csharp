namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using Cronofy.Requests;

    /// <summary>
    /// Class to build <see cref="AvailabilityRequest.ParticipantGroup"/>s.
    /// </summary>
    public sealed class ParticipantGroupBuilder : IBuilder<AvailabilityRequest.ParticipantGroup>
    {
        /// <summary>
        /// The members for the group.
        /// </summary>
        private readonly IList<AvailabilityRequest.Member> members = new List<AvailabilityRequest.Member>();

        /// <summary>
        /// The required attribute for the group.
        /// </summary>
        private object required;

        /// <summary>
        /// Adds a member to the group.
        /// </summary>
        /// <param name="sub">
        /// The sub of the member, must not be blank.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="ParticipantGroupBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="sub"/> is blank.
        /// </exception>
        public ParticipantGroupBuilder AddMember(string sub)
        {
            Preconditions.NotBlank("sub", sub);

            return this.AddMember(new AvailabilityMemberBuilder().Sub(sub));
        }

        /// <summary>
        /// Adds a member to the group.
        /// </summary>
        /// <param name="member">
        /// The member to add, must not be <c>null</c>.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="ParticipantGroupBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="member"/> is <c>null</c>.
        /// </exception>
        public ParticipantGroupBuilder AddMember(AvailabilityRequest.Member member)
        {
            Preconditions.NotNull("member", member);

            this.members.Add(member);

            return this;
        }

        /// <summary>
        /// Adds a member to the group.
        /// </summary>
        /// <param name="builder">
        /// A builder for the member to add, must not be <c>null</c>.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="ParticipantGroupBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="builder"/> is <c>null</c>.
        /// </exception>
        public ParticipantGroupBuilder AddMember(IBuilder<AvailabilityRequest.Member> builder)
        {
            Preconditions.NotNull("builder", builder);

            return this.AddMember(builder.Build());
        }

        /// <summary>
        /// Adds members to the group.
        /// </summary>
        /// <param name="subs">
        /// The subs of the members, must not be <c>null</c>.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="ParticipantGroupBuilder"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="subs"/> is <c>null</c>.
        /// </exception>
        public ParticipantGroupBuilder AddMembers(IEnumerable<string> subs)
        {
            Preconditions.NotNull("subs", subs);

            foreach (var sub in subs)
            {
                this.AddMember(sub);
            }

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
                Members = this.members,
            };
        }
    }
}
