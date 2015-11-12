namespace Cronofy
{
    using System;
    using System.Collections.Generic;

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
            /// <summary>
            /// Gets or sets a value indicating whether the channel is filtered
            /// to only managed events.
            /// </summary>
            /// <value>
            /// <c>true</c> if the channel is filtered to only managed events;
            /// otherwise, <c>false</c>.
            /// </value>
            public bool? OnlyManaged { get; set; }

            /// <summary>
            /// Gets or sets the calendar IDs the channel is filtered to.
            /// </summary>
            /// <value>
            /// The calendar IDs the channel is filtered to.
            /// </value>
            public IEnumerable<string> CalendarIds { get; set; }

            /// <inheritdoc/>
            public override int GetHashCode()
            {
                return this.OnlyManaged.GetHashCode();
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
                return other != null
                    && this.OnlyManaged == other.OnlyManaged
                    && EnumerableUtils.NullTolerantSequenceEqual(this.CalendarIds, other.CalendarIds);
            }

            /// <inheritdoc/>
            public override string ToString()
            {
                return string.Format(
                    "<{0} OnlyManaged={1}, CalendarIds={2}>",
                    this.GetType(),
                    this.OnlyManaged,
                    this.CalendarIds);
            }
        }
    }
}
