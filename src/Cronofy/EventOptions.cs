namespace Cronofy
{
    /// <summary>
    /// Class for the deserialization of the options for a read event
    /// response.
    /// </summary>
    public sealed class EventOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether this event can be deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if the event can be deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Delete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this event can be updated.
        /// </summary>
        /// <value>
        /// <c>true</c> if the event can be updated; otherwise, <c>false</c>.
        /// </value>
        public bool Update { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether permission exists to change the user’s participation status with regard to this event.
        /// </summary>
        /// <value>
        /// <c>true</c> if permission to update participation status exists; otherwise, <c>false</c>.
        /// </value>
        public bool ChangeParticipationStatus { get; set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is EventOptions && this.Equals((EventOptions)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Delete.GetHashCode() * 397) ^ this.Update.GetHashCode();
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="Cronofy.EventOptions"/>
        /// is equal to the current <see cref="Cronofy.EventOptions"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Cronofy.EventOptions"/> to compare with the current
        /// <see cref="Cronofy.EventOptions"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Cronofy.EventOptions"/> is
        /// equal to the current <see cref="Cronofy.EventOptions"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        private bool Equals(EventOptions other)
        {
            return this.Delete == other.Delete && this.Update == other.Update;
        }
    }
}