namespace Cronofy
{
    /// <summary>
    /// Class for representing a calendar.
    /// </summary>
    public sealed class Calendar
    {
        /// <summary>
        /// Gets or sets the profile the calendar belongs to.
        /// </summary>
        /// <value>
        /// The profile the calendar belongs to.
        /// </value>
        public Profile Profile { get; set; }

        /// <summary>
        /// Gets or sets the ID of the calendar.
        /// </summary>
        /// <value>
        /// The ID of the calendar.
        /// </value>
        public string CalendarId { get; set; }

        /// <summary>
        /// Gets or sets the name of the calendar.
        /// </summary>
        /// <value>
        /// The name of the calendar.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="Cronofy.Calendar"/> is read only.
        /// </summary>
        /// <value>
        /// <c>true</c> if read only; otherwise, <c>false</c>.
        /// </value>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="Cronofy.Calendar"/> has been deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted { get; set; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.CalendarId.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var other = obj as Calendar;

            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Cronofy.Calendar"/> is
        /// equal to the current <see cref="Cronofy.Calendar"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Cronofy.Calendar"/> to compare with the current
        /// <see cref="Cronofy.Calendar"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Cronofy.Calendar"/> is equal
        /// to the current <see cref="Cronofy.Calendar"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public bool Equals(Calendar other)
        {
            return other != null
                && this.CalendarId == other.CalendarId
                && this.Name == other.Name
                && this.ReadOnly == other.ReadOnly
                && this.Deleted == other.Deleted
                && object.Equals(this.Profile, other.Profile);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} Profile={1}, CalendarId={2}, Name={3}, ReadOnly={4}, Deleted={5}>",
                this.GetType(),
                this.Profile,
                this.CalendarId,
                this.Name,
                this.ReadOnly,
                this.Deleted);
        }
    }
}
