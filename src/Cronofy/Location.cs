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

        /// <summary>
        /// Gets or sets the latitude of the location.
        /// </summary>
        /// <value>
        /// The latitude of the location.
        /// </value>
        public string Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the location.
        /// </summary>
        /// <value>
        /// The longitude of the location.
        /// </value>
        public string Longitude { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cronofy.Location"/>
        /// class.
        /// </summary>
        /// <param name="description">
        /// The description for the location.
        /// </param>
        /// <param name="latitude">
        /// The latitude for the location.
        /// </param>
        /// <param name="longitude">
        /// The longitude for the location.
        /// </param>
        public Location(string description, string latitude, string longitude)
        {
            this.Description = description;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Description.GetHashCode() ^ this.Latitude.GetHashCode() ^ this.Longitude.GetHashCode();
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
                && this.Description == other.Description
                && this.Latitude == other.Latitude
                && this.Longitude == other.Longitude;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} Description={1}, Latitude={2}, Longitude={3}>",
                this.GetType(),
                this.Description,
                this.Latitude,
                this.Longitude);
        }
    }
}
