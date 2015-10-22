namespace Cronofy
{
    using System;

    /// <summary>
    /// Class representing a profile for an account.
    /// </summary>
    public sealed class Profile
    {
        /// <summary>
        /// Gets or sets the name of the provider of the profile.
        /// </summary>
        /// <value>
        /// The name of the provider for the profile.
        /// </value>
        public string ProviderName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the profile.
        /// </summary>
        /// <value>
        /// The ID of the profile.
        /// </value>
        public string ProfileId { get; set; }

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
            return this.ProfileId.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var other = obj as Profile;

            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Cronofy.Profile"/> is
        /// equal to the current <see cref="Cronofy.Profile"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Cronofy.Profile"/> to compare with the current
        /// <see cref="Cronofy.Profile"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Cronofy.Profile"/> is equal
        /// to the current <see cref="Cronofy.Profile"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public bool Equals(Profile other)
        {
            return other != null
                && this.ProfileId == other.ProfileId
                && this.Name == other.Name
                && this.ProviderName == other.ProviderName;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} ProviderName={1}, ProfileId={2}, Name={3}>",
                this.GetType(),
                this.ProviderName,
                this.ProfileId,
                this.Name);
        }
    }
}
