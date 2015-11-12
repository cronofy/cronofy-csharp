namespace Cronofy
{
    using System;

    /// <summary>
    /// Class for representing a channel.
    /// </summary>
    public sealed class Channel
    {
        /// <summary>
        /// Gets or sets the ID of the channel.
        /// </summary>
        /// <value>
        /// The ID of the channel.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the callback URL of the channel.
        /// </summary>
        /// <value>
        /// The callback URL of the channel.
        /// </value>
        public string CallbackUrl { get; set; }

        /// <summary>
        /// Gets or sets the active filters of the channel.
        /// </summary>
        /// <value>
        /// The active filters of the channel.
        /// </value>
        public ChannelFilters Filters { get; set; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var other = obj as Channel;

            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Cronofy.Channel"/> is
        /// equal to the current <see cref="Cronofy.Channel"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Cronofy.Channel"/> to compare with the current
        /// <see cref="Cronofy.Channel"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Cronofy.Channel"/> is equal
        /// to the current <see cref="Cronofy.Channel"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public bool Equals(Channel other)
        {
            return other != null
                && this.Id == other.Id
                && this.CallbackUrl == other.CallbackUrl
                && object.Equals(this.Filters, other.Filters);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} Id={1}, CallbackUrl={2}, Filters={3}>",
                this.GetType(),
                this.Id,
                this.CallbackUrl,
                this.Filters);
        }

        /// <summary>
        /// Class for representing the active filters of a channel.
        /// </summary>
        public sealed class ChannelFilters
        {
            /// <inheritdoc/>
            public override int GetHashCode()
            {
                // TODO Incorporate filters
                return 1;
            }

            /// <inheritdoc/>
            public override bool Equals(object obj)
            {
                var other = obj as ChannelFilters;

                if (other == null)
                {
                    return false;
                }

                return this.Equals(other);
            }

            /// <summary>
            /// Determines whether the specified <see cref="ChannelFilters"/>
            /// are equal to the current <see cref="ChannelFilters"/>.
            /// </summary>
            /// <param name="other">
            /// The <see cref="ChannelFilters"/> to compare with the current
            /// <see cref="ChannelFilters"/>.
            /// </param>
            /// <returns>
            /// <c>true</c> if the specified <see cref="ChannelFilters"/> are
            /// equal to the current <see cref="ChannelFilters"/>; otherwise,
            /// <c>false</c>.
            /// </returns>
            public bool Equals(ChannelFilters other)
            {
                // TODO Incorporate filters
                return other != null;
            }

            /// <inheritdoc/>
            public override string ToString()
            {
                return string.Format(
                    "<{0}>",
                    this.GetType());
            }
        }
    }
}
