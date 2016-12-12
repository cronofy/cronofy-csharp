namespace Cronofy
{
    using System;

    /// <summary>
    /// Class for representing an account.
    /// </summary>
    public sealed class Account
    {
        /// <summary>
        /// Gets or sets the ID of the account.
        /// </summary>
        /// <value>
        /// The ID of the account.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the email address of the account.
        /// </summary>
        /// <value>
        /// The email address of the account.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the name of the account.
        /// </summary>
        /// <value>
        /// The name of the account.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the default time zone identifier of the account.
        /// </summary>
        /// <value>
        /// The default time zone identifier of the account.
        /// </value>
        public string DefaultTimeZoneId { get; set; }

        /// <summary>
        /// Gets or sets the scopes granted for the account.
        /// </summary>
        /// <value>
        /// The scopes granted for the account.
        /// </value>
        public string[] Scope { get; set; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var other = obj as Account;

            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Cronofy.Account"/> is
        /// equal to the current <see cref="Cronofy.Account"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Cronofy.Account"/> to compare with the current
        /// <see cref="Cronofy.Account"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Cronofy.Account"/> is equal
        /// to the current <see cref="Cronofy.Account"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public bool Equals(Account other)
        {
            return other != null
                && this.Id == other.Id
                && this.Email == other.Email
                && this.Name == other.Name
                && this.DefaultTimeZoneId == other.DefaultTimeZoneId
                && string.Join(" ", this.Scope) == string.Join(" ", other.Scope);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} Id={1}, Email={2}, Name={3}, DefaultTimeZoneId={4}, Scope={5}>",
                this.GetType(),
                this.Id,
                this.Email,
                this.Name,
                this.DefaultTimeZoneId,
                string.Join(" ", this.Scope));
        }
    }
}
