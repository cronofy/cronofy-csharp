namespace Cronofy
{
    /// <summary>
    /// Class representing the linking profile for an authorization.
    /// </summary>
    public sealed class LinkingProfile
    {
        /// <summary>
        /// Gets or sets the name of the provider of the profile.
        /// </summary>
        /// <value>
        /// The name of the provider for the profile.
        /// </value>
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the service name of the provider of the profile.
        /// </summary>
        /// <value>
        /// The service name of the provider for the profile.
        /// </value>
        public string ProviderService { get; set; }

        /// <summary>
        /// Gets or sets the ID of the profile.
        /// </summary>
        /// <value>
        /// The ID of the profile.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the profile.
        /// </summary>
        /// <value>
        /// The name of the profile.
        /// </value>
        public string Name { get; set; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var other = obj as LinkingProfile;

            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="LinkingProfile"/> is
        /// equal to the current <see cref="LinkingProfile"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="LinkingProfile"/> to compare with the current
        /// <see cref="LinkingProfile"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="LinkingProfile"/> is equal
        /// to the current <see cref="LinkingProfile"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public bool Equals(LinkingProfile other)
        {
            return other != null
                && this.ProviderName == other.ProviderName
                && this.ProviderService == other.ProviderService
                && this.Id == other.Id
                && this.Name == other.Name;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} ProviderName={1}, ProviderService={2}, Id={3}, Name={4}>",
                this.GetType(),
                this.ProviderName,
                this.ProviderService,
                this.Id,
                this.Name);
        }
    }
}
