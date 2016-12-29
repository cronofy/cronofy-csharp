namespace Cronofy
{
    using System;

    /// <summary>
    /// Class for representing an available period.
    /// </summary>
    public sealed class AvailablePeriod
    {
        /// <summary>
        /// Gets or sets the start time of the period.
        /// </summary>
        /// <value>
        /// The start time of the period.
        /// </value>
        public DateTimeOffset Start { get; set; }

        /// <summary>
        /// Gets or sets the end time of the period.
        /// </summary>
        /// <value>
        /// The end time of the period.
        /// </value>
        public DateTimeOffset End { get; set; }

        /// <summary>
        /// Gets or sets the participants for the period.
        /// </summary>
        /// <value>
        /// The participants for the period.
        /// </value>
        public Participant[] Participants { get; set; }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.Start.GetHashCode() ^ this.End.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var other = obj as AvailablePeriod;

            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="AvailablePeriod"/> is
        /// equal to the current <see cref="AvailablePeriod"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="AvailablePeriod"/> to compare with the current
        /// <see cref="AvailablePeriod"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="AvailablePeriod"/> is equal
        /// to the current <see cref="AvailablePeriod"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public bool Equals(AvailablePeriod other)
        {
            return other != null
                   && object.Equals(this.Start, other.Start)
                   && object.Equals(this.End, other.End)
                   && EnumerableUtils.NullTolerantSequenceEqual(this.Participants, other.Participants);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format(
                "<{0} Start={1}, End={2}, Participants={3}>",
                this.GetType(),
                this.Start,
                this.End,
                this.Participants);
        }

        /// <summary>
        /// Class for representing a participant for an available period.
        /// </summary>
        public sealed class Participant
        {
            /// <summary>
            /// Gets or sets the subject the participant relates to.
            /// </summary>
            /// <value>
            /// The subject the participant relates to.
            /// </value>
            public string Sub { get; set; }

            /// <inheritdoc />
            public override int GetHashCode()
            {
                return this.Sub.GetHashCode();
            }

            /// <inheritdoc />
            public override bool Equals(object obj)
            {
                var other = obj as Participant;

                if (other == null)
                {
                    return false;
                }

                return this.Equals(other);
            }

            /// <summary>
            /// Determines whether the specified <see cref="Participant"/> is
            /// equal to the current <see cref="Participant"/>.
            /// </summary>
            /// <param name="other">
            /// The <see cref="Participant"/> to compare with the current
            /// <see cref="Participant"/>.
            /// </param>
            /// <returns>
            /// <c>true</c> if the specified <see cref="Participant"/> is equal
            /// to the current <see cref="Participant"/>; otherwise,
            /// <c>false</c>.
            /// </returns>
            public bool Equals(Participant other)
            {
                return string.Equals(this.Sub, other.Sub);
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return string.Format(
                    "<{0} Sub={1}>",
                    this.GetType(),
                    this.Sub);
            }
        }
    }
}