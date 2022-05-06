namespace Cronofy
{
    /// <summary>
    /// Class representing a free-busy period.
    /// </summary>
    public sealed class FreeBusy
    {
        /// <summary>
        /// Gets or sets the ID of the calendar the event is within.
        /// </summary>
        /// <value>
        /// The ID of the calendar the event is within.
        /// </value>
        public string CalendarId { get; set; }

        /// <summary>
        /// Gets or sets the start time of the free-busy period.
        /// </summary>
        /// <value>
        /// The start time of the  free-busy period.
        /// </value>
        public EventTime Start { get; set; }

        /// <summary>
        /// Gets or sets the end time of the  free-busy period.
        /// </summary>
        /// <value>
        /// The end time of the  free-busy period.
        /// </value>
        public EventTime End { get; set; }

        /// <summary>
        /// Gets or sets the status of the  free-busy period.
        /// </summary>
        /// <value>
        /// The period's free-busy status.
        /// </value>
        /// <remarks>
        /// See <see cref="Cronofy.FreeBusyStatus"/> for potential values.
        /// </remarks>
        public string FreeBusyStatus { get; set; }

        /// <summary>
        /// Gets or sets the OAuth application's ID for the event, if it is
        /// an event the OAuth application is managing.
        /// </summary>
        /// <value>
        /// The OAuth application's ID for the event, <c>null</c> if the
        /// OAuth application is not managing this event.
        /// </value>
        public string EventId { get; set; }

        /// <summary>
        /// Gets or sets the UID of the event.
        /// </summary>
        /// <value>
        /// The UID of the event.
        /// </value>
        public string EventUid { get; set; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.CalendarId.GetHashCode() ^ this.Start.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var other = obj as FreeBusy;

            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Cronofy.FreeBusy"/> is
        /// equal to the current <see cref="Cronofy.FreeBusy"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Cronofy.FreeBusy"/> to compare with the current
        /// <see cref="Cronofy.FreeBusy"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Cronofy.FreeBusy"/> is equal
        /// to the current <see cref="Cronofy.FreeBusy"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public bool Equals(FreeBusy other)
        {
            return other != null
                && this.CalendarId == other.CalendarId
                && object.Equals(this.Start, other.Start)
                && object.Equals(this.End, other.End)
                && object.Equals(this.FreeBusyStatus, other.FreeBusyStatus);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} CalendarId={1}, Start={2}, End={3}, FreeBusyStatus={4}>",
                this.GetType(),
                this.CalendarId,
                this.Start,
                this.End,
                this.FreeBusyStatus);
        }
    }
}
