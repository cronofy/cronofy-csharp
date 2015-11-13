namespace Cronofy.Requests
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of push notification requests.
    /// </summary>
    public sealed class PushNotificationRequest
    {
        /// <summary>
        /// Gets or sets the details of the notification.
        /// </summary>
        /// <value>
        /// The details of the notification.
        /// </value>
        [JsonProperty("notification")]
        public NotificationDetail Notification { get; set; }

        /// <summary>
        /// Gets or sets the details of the channel.
        /// </summary>
        /// <value>
        /// The details of the channel.
        /// </value>
        [JsonProperty("channel")]
        public ChannelDetail Channel { get; set; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.Notification.GetHashCode() ^ this.Channel.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var notification = obj as PushNotificationRequest;

            if (notification == null)
            {
                return false;
            }

            return this.Equals(notification);
        }

        /// <summary>
        /// Determines whether the specified
        /// <see cref="PushNotificationRequest"/> is equal to the current
        /// <see cref="PushNotificationRequest"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="PushNotificationRequest"/> to compare with the
        /// current <see cref="PushNotificationRequest"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="PushNotificationRequest"/>
        /// is equal to the current <see cref="PushNotificationRequest"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(PushNotificationRequest other)
        {
            return other != null
                && object.Equals(this.Notification, other.Notification)
                && object.Equals(this.Channel, other.Channel);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} Notification={1}, Channel={2}>",
                this.GetType(),
                this.Notification,
                this.Channel);
        }

        /// <summary>
        /// Class for the deserialization of push notification details.
        /// </summary>
        public sealed class NotificationDetail
        {
            /// <summary>
            /// Gets or sets the type of the notification.
            /// </summary>
            /// <value>
            /// The type of the notification.
            /// </value>
            [JsonProperty("type")]
            public string Type { get; set; }

            /// <summary>
            /// Gets or sets the time that there have been changes since.
            /// </summary>
            /// <value>
            /// The time that there have been changes since.
            /// </value>
            [JsonProperty("changes_since")]
            public DateTime? ChangesSince { get; set; }

            /// <inheritdoc/>
            public override int GetHashCode()
            {
                return this.Type.GetHashCode() ^ this.ChangesSince.GetHashCode();
            }

            /// <inheritdoc/>
            public override bool Equals(object obj)
            {
                var notification = obj as NotificationDetail;

                if (notification == null)
                {
                    return false;
                }

                return this.Equals(notification);
            }

            /// <summary>
            /// Determines whether the specified
            /// <see cref="NotificationDetail"/> is equal to the current
            /// <see cref="NotificationDetail"/>.
            /// </summary>
            /// <param name="other">
            /// The <see cref="NotificationDetail"/> to compare with the
            /// current <see cref="NotificationDetail"/>.
            /// </param>
            /// <returns>
            /// <c>true</c> if the specified <see cref="NotificationDetail"/>
            /// is equal to the current <see cref="NotificationDetail"/>;
            /// otherwise, <c>false</c>.
            /// </returns>
            public bool Equals(NotificationDetail other)
            {
                return other != null
                    && this.Type.Equals(other.Type)
                    && object.Equals(this.ChangesSince, other.ChangesSince);
            }

            /// <inheritdoc/>
            public override string ToString()
            {
                return string.Format(
                    "<{0} Type={1}, ChangesSince={2}>",
                    this.GetType(),
                    this.Type,
                    this.ChangesSince);
            }
        }

        /// <summary>
        /// Class for the deserialization of channel details.
        /// </summary>
        public sealed class ChannelDetail
        {
            /// <summary>
            /// Gets or sets the ID of the channel.
            /// </summary>
            /// <value>
            /// The ID of the channel.
            /// </value>
            [JsonProperty("channel_id")]
            public string Id { get; set; }

            /// <summary>
            /// Gets or sets the callback URL of the channel.
            /// </summary>
            /// <value>
            /// The callback URL of the channel.
            /// </value>
            [JsonProperty("callback_url")]
            public string CallbackUrl { get; set; }

            /// <summary>
            /// Gets or sets the filters for the channel.
            /// </summary>
            /// <value>
            /// The filters for the channel.
            /// </value>
            [JsonProperty("filters")]
            public ChannelFilters Filters { get; set; }

            /// <inheritdoc/>
            public override int GetHashCode()
            {
                return this.Id.GetHashCode() ^ this.CallbackUrl.GetHashCode() ^ this.Filters.GetHashCode();
            }

            /// <inheritdoc/>
            public override bool Equals(object obj)
            {
                var channel = obj as ChannelDetail;

                if (channel == null)
                {
                    return false;
                }

                return this.Equals(channel);
            }

            /// <summary>
            /// Determines whether the specified <see cref="ChannelDetail"/> is
            /// equal to the current <see cref="ChannelDetail"/>.
            /// </summary>
            /// <param name="other">
            /// The <see cref="ChannelDetail"/> to compare with the current
            /// <see cref="ChannelDetail"/>.
            /// </param>
            /// <returns>
            /// <c>true</c> if the specified <see cref="ChannelDetail"/> is
            /// equal to the current <see cref="ChannelDetail"/>; otherwise,
            /// <c>false</c>.
            /// </returns>
            public bool Equals(ChannelDetail other)
            {
                return other != null
                    && this.Id.Equals(other.Id)
                    && this.CallbackUrl.Equals(other.CallbackUrl)
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
            /// Class for the deserialization of the filtering options of a channel
            /// response.
            /// </summary>
            public sealed class ChannelFilters
            {
                /// <summary>
                /// Gets or sets the only managed flag.
                /// </summary>
                /// <value>
                /// The only managed flag.
                /// </value>
                [JsonProperty("only_managed")]
                public bool? OnlyManaged { get; set; }

                /// <summary>
                /// Gets or sets the calendar ID filters.
                /// </summary>
                /// <value>
                /// The calendar ID filters.
                /// </value>
                [JsonProperty("calendar_ids")]
                public string[] CalendarIds { get; set; }

                /// <inheritdoc/>
                public override int GetHashCode()
                {
                    return this.OnlyManaged.GetHashCode();
                }

                /// <inheritdoc/>
                public override bool Equals(object obj)
                {
                    var channel = obj as ChannelFilters;

                    if (channel == null)
                    {
                        return false;
                    }

                    return this.Equals(channel);
                }

                /// <summary>
                /// Determines whether the specified
                /// <see cref="ChannelFilters"/> is equal to the current
                /// <see cref="ChannelDetail"/>.
                /// </summary>
                /// <param name="other">
                /// The <see cref="ChannelDetail"/> to compare with the current
                /// <see cref="ChannelDetail"/>.
                /// </param>
                /// <returns>
                /// <c>true</c> if the specified <see cref="ChannelDetail"/> is
                /// equal to the current <see cref="ChannelDetail"/>; otherwise,
                /// <c>false</c>.
                /// </returns>
                public bool Equals(ChannelFilters other)
                {
                    return other != null
                        && object.Equals(this.OnlyManaged, other.OnlyManaged)
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
}
