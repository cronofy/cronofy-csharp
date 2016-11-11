namespace Cronofy
{
    /// <summary>
    /// Class for representing an event organizer.
    /// </summary>
    public sealed class Organizer
    {
        /// <summary>
        /// Gets or sets the organizer's email.
        /// </summary>
        /// <value>
        /// The attendee's email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the organizer's display name.
        /// </summary>
        /// <value>
        /// The organizer's display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Email.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var other = obj as Organizer;

            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Cronofy.Organizer"/> is
        /// equal to the current <see cref="Cronofy.Organizer"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Cronofy.Organizer"/> to compare with the current
        /// <see cref="Cronofy.Organizer"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Cronofy.Organizer"/> is equal
        /// to the current <see cref="Cronofy.Organizer"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public bool Equals(Organizer other)
        {
            return other != null
                && this.Email == other.Email
                && this.DisplayName == other.DisplayName;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} Email={1}, DisplayName={2}>",
                this.GetType(),
                this.Email,
                this.DisplayName);
        }
    }
}
