namespace Cronofy
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class for representing an available sequence.
    /// </summary>
    public sealed class AvailableSequences
    {
        /// <summary>
        /// Gets or sets the sequences.
        /// </summary>
        /// <value>The sequences.</value>
        public IEnumerable<IEnumerable<SequenceItem>> Sequences { get; set; }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.Sequences.GetHashCode();
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var other = obj as AvailableSequences;

            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="AvailableSequences"/> is
        /// equal to the current <see cref="AvailableSequences"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="AvailableSequences"/> to compare with the current
        /// <see cref="AvailableSequences"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="AvailableSequences"/> is equal
        /// to the current <see cref="AvailableSequences"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public bool Equals(AvailableSequences other)
        {
            return other != null && EnumerableUtils.NullTolerantSequenceEqual(this.Sequences, other.Sequences);
        }

        /// <summary>
        /// Class for representing an sequence item.
        /// </summary>
        public sealed class SequenceItem
        {
            /// <summary>
            /// Gets or sets sequence id.
            /// </summary>
            /// <value>
            /// The sequence id.
            /// </value>
            public string SequenceId { get; set; }

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
            public AvailablePeriod.Participant[] Participants { get; set; }

            /// <inheritdoc />
            public override int GetHashCode()
            {
                return this.Start.GetHashCode() ^ this.End.GetHashCode() ^ this.SequenceId.GetHashCode();
            }

            /// <inheritdoc />
            public override bool Equals(object obj)
            {
                var other = obj as SequenceItem;

                if (other == null)
                {
                    return false;
                }

                return this.Equals(other);
            }

            /// <summary>
            /// Determines whether the specified <see cref="SequenceItem"/> is
            /// equal to the current <see cref="SequenceItem"/>.
            /// </summary>
            /// <param name="other">
            /// The <see cref="SequenceItem"/> to compare with the current
            /// <see cref="SequenceItem"/>.
            /// </param>
            /// <returns>
            /// <c>true</c> if the specified <see cref="SequenceItem"/> is equal
            /// to the current <see cref="SequenceItem"/>; otherwise,
            /// <c>false</c>.
            /// </returns>
            public bool Equals(SequenceItem other)
            {
                return other != null
                       && object.Equals(this.Start, other.Start)
                       && object.Equals(this.End, other.End)
                       && object.Equals(this.SequenceId, other.SequenceId)
                       && EnumerableUtils.NullTolerantSequenceEqual(this.Participants, other.Participants);
            }

            /// <inheritdoc />
            public override string ToString()
            {
                return string.Format(
                    "<{0} Start={1}, End={2}, SequenceId={3}, Participants={4}>",
                    this.GetType(),
                    this.Start,
                    this.End,
                    this.SequenceId,
                    this.Participants);
            }
        }
    }
}