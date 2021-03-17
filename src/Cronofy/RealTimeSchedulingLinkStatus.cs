namespace Cronofy
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The status of a Real-Time Scheduling link.
    /// </summary>
    public class RealTimeSchedulingLinkStatus
    {
        /// <summary>
        /// An enumeration of Real-Time Scheduling statuses.
        /// </summary>
        public enum LinkStatus
        {
            /// <summary>
            /// A slot hasn't been chosen.
            /// </summary>
            Open,

            /// <summary>
            /// A slot has been chosen.
            /// </summary>
            Completed,

            /// <summary>
            /// The link has been disabled before a slot was chosen.
            /// </summary>
            Disabled,
        }

        /// <summary>
        /// Gets the Cronofy identifier for the Real-Time Scheduling link.
        /// </summary>
        /// <value>The Cronofy identifier for the Real-Time Scheduling link.</value>
        public string RealTimeSchedulingId { get; internal set; }

        /// <summary>
        /// Gets the URL to direct the user to in order for them to select a time slot.
        /// </summary>
        /// <value>The URL to direct the user to in order for them to select a time slot.</value>
        public string Url { get; internal set; }

        /// <summary>
        /// Gets the current state of the event associated with the link. Once completed the event will have start and end values.
        /// </summary>
        /// <value>The current state of the event associated with the link. Once completed the event will have start and end values.</value>
        public Event Event { get; internal set; }

        /// <summary>
        /// Gets the current status of the link.
        /// </summary>
        /// <value>The current status of the link.</value>
        public LinkStatus Status { get; internal set; }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return obj is RealTimeSchedulingLinkStatus status &&
                   this.RealTimeSchedulingId == status.RealTimeSchedulingId &&
                   this.Url == status.Url &&
                   this.Event.Equals(status.Event) &&
                   this.Status == status.Status;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            int hashCode = -549959893;
            hashCode = (hashCode * -1521134295) + this.RealTimeSchedulingId.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.Url.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.Event.GetHashCode();
            hashCode = (hashCode * -1521134295) + this.Status.GetHashCode();
            return hashCode;
        }
    }
}
