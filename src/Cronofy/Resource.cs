namespace Cronofy
{
    using System;

    /// <summary>
    /// Class representing a resource for an Enterprise Connect account.
    /// </summary>
    public sealed class Resource
    {
        /// <summary>
        /// Gets or sets the email of the resource.
        /// </summary>
        /// <value>
        /// The resource's email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the name of the resource.
        /// </summary>
        /// <value>
        /// The resource's name.
        /// </value>
        public string Name { get; set; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Email.GetHashCode() ^ this.Name.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var other = obj as Resource;

            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Cronofy.Resource"/> is
        /// equal to the current <see cref="Cronofy.Resource"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Cronofy.Resource"/> to compare with the current
        /// <see cref="Cronofy.Resource"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Cronofy.Resource"/> is equal
        /// to the current <see cref="Cronofy.Resource"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public bool Equals(Resource other)
        {
            return other != null
                && this.Email == other.Email
                && this.Name == other.Name;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} Email={1}, Name={2}>",
                this.GetType(),
                this.Email,
                this.Name);
        }
    }
}
