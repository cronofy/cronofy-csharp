namespace Cronofy
{
    /// <summary>
    /// Class for the deserialization of the options for a read event
    /// response.
    /// </summary>
    public sealed class Conferencing
    {
        /// <summary>
        /// Gets or sets the conferencing provider name.
        /// </summary>
        /// <value>
        /// The conferencing provider name.
        /// </value>
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the conferencing URL of the event.
        /// </summary>
        /// <value>
        /// The conferencing URL of the event.
        /// </value>
        public string JoinUrl { get; set; }

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

            return obj is Conferencing && this.Equals((Conferencing)obj);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                return (this.ProviderName.GetHashCode() * 397) ^ this.JoinUrl.GetHashCode();
            }
        }

        /// <summary>
        /// Determines whether the specified <see cref="Cronofy.Conferencing"/>
        /// is equal to the current <see cref="Cronofy.Conferencing"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Cronofy.Conferencing"/> to compare with the current
        /// <see cref="Cronofy.Conferencing"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Cronofy.Conferencing"/> is
        /// equal to the current <see cref="Cronofy.Conferencing"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        private bool Equals(Conferencing other)
        {
            return this.ProviderName == other.ProviderName && this.JoinUrl == other.JoinUrl;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} ProviderName={1}, JoinUrl={2}>",
                this.GetType(),
                this.ProviderName,
                this.JoinUrl);
        }
    }
}