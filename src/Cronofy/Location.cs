namespace Cronofy
{
    /// <summary>
    /// Class representing a location.
    /// </summary>
    public sealed class Location
    {
        /// <summary>
        /// Gets or sets the description of the location.
        /// </summary>
        /// <value>
        /// The description of the location.
        /// </value>
        public string Description { get; set; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Description.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var other = obj as Location;

            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Cronofy.Location"/> is
        /// equal to the current <see cref="Cronofy.Location"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Cronofy.Location"/> to compare with the current
        /// <see cref="Cronofy.Location"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Cronofy.Location"/> is equal
        /// to the current <see cref="Cronofy.Location"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public bool Equals(Location other)
        {
            return other != null
                && this.Description == other.Description;
        }
    }
}
