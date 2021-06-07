namespace Cronofy
{
    /// <summary>
    /// Class for representing a calendar.
    /// </summary>
    public sealed class Calendar
    {
        /// <summary>
        /// Gets or sets the profile the calendar belongs to.
        /// </summary>
        /// <value>
        /// The profile the calendar belongs to.
        /// </value>
        public ProfileSummary Profile { get; set; }

        /// <summary>
        /// Gets or sets the ID of the calendar.
        /// </summary>
        /// <value>
        /// The ID of the calendar.
        /// </value>
        public string CalendarId { get; set; }

        /// <summary>
        /// Gets or sets the name of the calendar.
        /// </summary>
        /// <value>
        /// The name of the calendar.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="Cronofy.Calendar"/> is read only.
        /// </summary>
        /// <value>
        /// <c>true</c> if read only; otherwise, <c>false</c>.
        /// </value>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="Cronofy.Calendar"/> has been deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="Cronofy.Calendar"/> is the primary calendar for the
        /// profile.
        /// </summary>
        /// <value>
        /// <c>true</c> if the primary calendar for the profile; otherwise,
        /// <c>false</c>.
        /// </value>
        public bool Primary { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this
        /// <see cref="Cronofy.Calendar"/> supports "integrated" conferencing
        /// mode.
        /// </summary>
        /// <value>
        /// <c>true</c> if the calendar supports integrated conferencing; otherwise,
        /// <c>false</c>.
        /// </value>
        public bool IntegratedConferencingAvailable { get; set; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.CalendarId.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var other = obj as Calendar;

            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Cronofy.Calendar"/> is
        /// equal to the current <see cref="Cronofy.Calendar"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Cronofy.Calendar"/> to compare with the current
        /// <see cref="Cronofy.Calendar"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Cronofy.Calendar"/> is equal
        /// to the current <see cref="Cronofy.Calendar"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public bool Equals(Calendar other)
        {
            return other != null
                && this.CalendarId == other.CalendarId
                && this.Name == other.Name
                && this.ReadOnly == other.ReadOnly
                && this.Deleted == other.Deleted
                && this.Primary == other.Primary
                && this.IntegratedConferencingAvailable == other.IntegratedConferencingAvailable
                && object.Equals(this.Profile, other.Profile);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} Profile={1}, CalendarId={2}, Name={3}, ReadOnly={4}, Deleted={5}, Primary={6}, IntegratedConferencing={7}>",
                this.GetType(),
                this.Profile,
                this.CalendarId,
                this.Name,
                this.ReadOnly,
                this.Deleted,
                this.Primary,
                this.IntegratedConferencingAvailable);
        }

        /// <summary>
        /// Class representing a profile for an account.
        /// </summary>
        public sealed class ProfileSummary
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
                var other = obj as ProfileSummary;

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
            public bool Equals(ProfileSummary other)
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
}
