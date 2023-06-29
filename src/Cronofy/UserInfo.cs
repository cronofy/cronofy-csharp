namespace Cronofy
{
    using System.Linq;

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
        /// Gets or sets the account's Cronofy account type.
        /// </summary>
        /// <value>
        /// The type of the account.
        /// </value>
        public Data CronofyData { get; set; }

        /// <summary>
        /// Gets or sets the profiles.
        /// </summary>
        /// <value>The profiles.</value>
        public Profile[] Profiles { get; set; }

        /// <summary>
        /// Gets or sets the account's email.
        /// </summary>
        /// <value>
        /// The email of the account.
        /// </value>
        public string Email { get; set; }

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
                && EnumerableUtils.NullTolerantSequenceEqual(this.Profiles, other.Profiles);
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

        /// <summary>
        /// Class representing Cronofy Data for an account.
        /// </summary>
        public sealed class Data
        {
            /// <summary>
            /// Gets or sets the authorization of the account.
            /// </summary>
            /// <value>
            /// The authorization for the account.
            /// </value>
            public Authorization Authorization { get; set; }

            /// <summary>
            /// Gets or sets the Service Account data.
            /// </summary>
            /// <value>
            /// The Service Account data.
            /// </value>
            public ServiceAccount ServiceAccount { get; set; }
        }

        /// <summary>
        /// Class representing an authorization for an account.
        /// </summary>
        public sealed class ServiceAccount
        {
            /// <summary>
            /// Gets or sets the provider name of the Service Account.
            /// </summary>
            /// <value>
            /// The provider name of the Service Account.
            /// </value>
            public string ProviderName { get; set; }

            /// <inheritdoc/>
            public override int GetHashCode()
            {
                return this.ProviderName.GetHashCode();
            }

            /// <inheritdoc/>
            public override bool Equals(object obj)
            {
                var other = obj as ServiceAccount;

                if (other == null)
                {
                    return false;
                }

                return this.Equals(other);
            }

            /// <summary>
            /// Determines whether the specified <see cref="Cronofy.UserInfo.ServiceAccount"/> is
            /// equal to the current <see cref="Cronofy.UserInfo.ServiceAccount"/>.
            /// </summary>
            /// <param name="other">
            /// The <see cref="Cronofy.UserInfo.ServiceAccount"/> to compare with the current
            /// <see cref="Cronofy.UserInfo.ServiceAccount"/>.
            /// </param>
            /// <returns>
            /// <c>true</c> if the specified <see cref="Cronofy.UserInfo.ServiceAccount"/> is equal
            /// to the current <see cref="Cronofy.UserInfo.ServiceAccount"/>; otherwise,
            /// <c>false</c>.
            /// </returns>
            public bool Equals(ServiceAccount other)
            {
                return other != null
                    && this.ProviderName == other.ProviderName;
            }

            /// <inheritdoc/>
            public override string ToString()
            {
                return string.Format(
                    "<{0} ProviderName={1}>",
                    this.GetType(),
                    this.ProviderName);
            }
        }

        /// <summary>
        /// Class representing an authorization for an account.
        /// </summary>
        public sealed class Authorization
        {
            /// <summary>
            /// Gets or sets the scope of the authorization of the account.
            /// </summary>
            /// <value>
            /// The scope of the authorization for the account.
            /// </value>
            public string Scope { get; set; }

            /// <summary>
            /// Gets or sets the status of the authorization of the account.
            /// </summary>
            /// <value>
            /// The status of the authorization for the account.
            /// </value>
            public string Status { get; set; }

            /// <summary>
            /// Gets or sets the delegated scope of the account.
            /// </summary>
            /// <value>
            /// The delegated scope of the account.
            /// </value>
            public string DelegatedScope { get; set; }

            /// <inheritdoc/>
            public override int GetHashCode()
            {
                return this.Scope.GetHashCode();
            }

            /// <inheritdoc/>
            public override bool Equals(object obj)
            {
                var other = obj as Authorization;

                if (other == null)
                {
                    return false;
                }

                return this.Equals(other);
            }

            /// <summary>
            /// Determines whether the specified <see cref="Cronofy.UserInfo.Authorization"/> is
            /// equal to the current <see cref="Cronofy.UserInfo.Authorization"/>.
            /// </summary>
            /// <param name="other">
            /// The <see cref="Cronofy.UserInfo.Authorization"/> to compare with the current
            /// <see cref="Cronofy.UserInfo.Authorization"/>.
            /// </param>
            /// <returns>
            /// <c>true</c> if the specified <see cref="Cronofy.UserInfo.Authorization"/> is equal
            /// to the current <see cref="Cronofy.UserInfo.Authorization"/>; otherwise,
            /// <c>false</c>.
            /// </returns>
            public bool Equals(Authorization other)
            {
                return other != null
                    && this.Scope == other.Scope
                    && this.Status == other.Status
                    && this.DelegatedScope == other.DelegatedScope;
            }

            /// <inheritdoc/>
            public override string ToString()
            {
                return string.Format(
                    "<{0} Scope={1}, Status={2}, DelegatedScope={3}>",
                    this.GetType(),
                    this.Scope,
                    this.Status,
                    this.DelegatedScope);
            }
        }

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

            /// <summary>
            /// Gets or sets a value indicating whether this profile is connected.
            /// </summary>
            /// <value>
            /// <c>true</c> if this profile is connected; otherwise, <c>false</c>.
            /// </value>
            public bool Connected { get; set; }

            /// <summary>
            /// Gets or sets the relink URL for the profile.
            /// </summary>
            /// <value>
            /// The relink URL for the profile.
            /// </value>
            /// <remarks>
            /// <c>null</c> unless <see cref="Connected"/> is false.
            /// </remarks>
            public string RelinkUrl { get; set; }

            /// <summary>
            /// Gets or sets the profile's calendars.
            /// </summary>
            /// <value>The profile's calendars.</value>
            public Calendar[] Calendars { get; set; }

            /// <inheritdoc/>
            public override int GetHashCode()
            {
                return this.Id.GetHashCode();
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
            /// Determines whether the specified <see cref="Cronofy.UserInfo.Profile"/> is
            /// equal to the current <see cref="Cronofy.UserInfo.Profile"/>.
            /// </summary>
            /// <param name="other">
            /// The <see cref="Cronofy.UserInfo.Profile"/> to compare with the current
            /// <see cref="Cronofy.UserInfo.Profile"/>.
            /// </param>
            /// <returns>
            /// <c>true</c> if the specified <see cref="Cronofy.UserInfo.Profile"/> is equal
            /// to the current <see cref="Cronofy.UserInfo.Profile"/>; otherwise,
            /// <c>false</c>.
            /// </returns>
            public bool Equals(Profile other)
            {
                return other != null
                    && this.Id == other.Id
                    && this.Name == other.Name
                    && this.ProviderName == other.ProviderName
                    && this.ProviderService == other.ProviderService
                    && this.Connected == other.Connected
                    && this.RelinkUrl == other.RelinkUrl
                    && EnumerableUtils.NullTolerantSequenceEqual(this.Calendars, other.Calendars);
            }

            /// <inheritdoc/>
            public override string ToString()
            {
                return string.Format(
                    "<{0} ProviderName={1}, ProviderService={2}, Id={3}, Name={4}, Connected={5}, RelinkUrl={6}>",
                    this.GetType(),
                    this.ProviderName,
                    this.ProviderService,
                    this.Id,
                    this.Name,
                    this.Connected,
                    this.RelinkUrl);
            }
        }
    }
}
