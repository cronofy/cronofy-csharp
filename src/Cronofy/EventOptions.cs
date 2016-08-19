namespace Cronofy
{
    /// <summary>
    /// Class for the deserialization of the options for a read event
    /// response.
    /// </summary>
    public sealed class EventOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether this event can be deleted
        /// </summary>
        /// <value>
        /// <c>true</c> if the event can be deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Delete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this event can be updated
        /// </summary>
        /// <value>
        /// <c>true</c> if the event can be updated; otherwise, <c>false</c>.
        /// </value>
        public bool Update { get; set; }

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

            return obj is EventOptions && Equals((EventOptions) obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Delete.GetHashCode()*397) ^ Update.GetHashCode();
            }
        }

        private bool Equals(EventOptions other)
        {
            return Delete == other.Delete && Update == other.Update;
        }
    }
}