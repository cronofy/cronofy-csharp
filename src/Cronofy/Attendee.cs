namespace Cronofy
{
    /// <summary>
    /// Class for representing an event attendee.
    /// </summary>
    public sealed class Attendee
    {
        /// <summary>
        /// Gets or sets the attendee's email.
        /// </summary>
        /// <value>
        /// The attendee's email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the attendee's display name.
        /// </summary>
        /// <value>
        /// The attendee's display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the attendee's status.
        /// </summary>
        /// <value>
        /// The attendee's status.
        /// </value>
        /// <remarks>
        /// See <see cref="AttendeeStatus"/> for potential values.
        /// </remarks>
        public string Status { get; set; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Email.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var other = obj as Attendee;

            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Cronofy.Attendee"/> is
        /// equal to the current <see cref="Cronofy.Attendee"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Cronofy.Attendee"/> to compare with the current
        /// <see cref="Cronofy.Attendee"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Cronofy.Attendee"/> is equal
        /// to the current <see cref="Cronofy.Attendee"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public bool Equals(Attendee other)
        {
            return other != null
                && this.Email == other.Email
                && this.DisplayName == other.DisplayName
                && this.Status == other.Status;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} Email={1}, DisplayName={2}, Status={3}>",
                this.GetType(),
                this.Email,
                this.DisplayName,
                this.Status);
        }
    }
}
