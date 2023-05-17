namespace Cronofy
{
    /// <summary>
    /// Class representing a user's information.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Gets or sets the accounts's Cronofy account ID.
        /// </summary>
        /// <value>
        /// The sub of the account.
        /// </value>
        public string Sub { get; set; }

        /// <summary>
        /// Gets or sets the account's Cronofy account type.
        /// </summary>
        /// <value>
        /// The type of the account.
        /// </value>
        public string CronofyType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether integrated conferencing is available for an account.
        /// </summary>
        /// <value>
        /// True if conferencing available, otherwise false.
        /// </value>
        public bool CalendarIntegratedConferencingAvailable { get; set; }

        /// <summary>
        /// Gets or sets the calendar provider name.
        /// </summary>
        /// <value>
        /// The name of a calendar provider of a calendar profile.
        /// </value>
        public string ProviderName { get; set; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Sub.GetHashCode() ^ this.CronofyType.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var other = obj as UserInfo;

            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Cronofy.UserInfo"/> is
        /// equal to the current <see cref="Cronofy.UserInfo"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Cronofy.UserInfo"/> to compare with the current
        /// <see cref="Cronofy.UserInfo"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Cronofy.UserInfo"/> is equal
        /// to the current <see cref="Cronofy.UserInfo"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public bool Equals(UserInfo other)
        {
            return other != null
                && this.Sub == other.Sub
                       && this.CronofyType == other.CronofyType
                            && this.CalendarIntegratedConferencingAvailable == other.CalendarIntegratedConferencingAvailable
                                && this.ProviderName == other.ProviderName;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} Sub={1}, CronofyType={2}>, CalendarIntegratedConferencingAvailable={3}>, ProviderName={4}>",
                this.GetType(),
                this.Sub,
                this.CronofyType,
                this.CalendarIntegratedConferencingAvailable,
                this.ProviderName);
        }
    }
}
