using System;
namespace Cronofy
{
    /// <summary>
    /// Class representing a user's information.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// Gets or sets the user's Cronofy account ID.
        /// </summary>
        public string Sub { get; set; }

        /// <summary>
        /// Gets or sets the user's Cronofy account type.
        /// </summary>
		public string CronofyType { get; set; }

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
                       && this.CronofyType == other.CronofyType;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} Sub={1}, CronofyType={2}>",
                this.GetType(),
                this.Sub,
                this.CronofyType);
        }
    }
}
