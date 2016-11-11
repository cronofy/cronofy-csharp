namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class representing an event.
    /// </summary>
    public sealed class Event
    {
        /// <summary>
        /// Gets or sets the ID of the calendar the event is within.
        /// </summary>
        /// <value>
        /// The ID of the calendar the event is within.
        /// </value>
        public string CalendarId { get; set; }

        /// <summary>
        /// Gets or sets the OAuth application's ID for the event, if it is
        /// an event the OAuth application is managing.
        /// </summary>
        /// <value>
        /// The OAuth application's ID for the event, <c>null</c> if the
        /// OAuth application is not managing this event.
        /// </value>
        public string EventId { get; set; }

        /// <summary>
        /// Gets or sets the UID of the event.
        /// </summary>
        /// <value>
        /// The UID of the event.
        /// </value>
        public string EventUid { get; set; }

        /// <summary>
        /// Gets or sets the event's summary.
        /// </summary>
        /// <value>
        /// The event's summary.
        /// </value>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the event's description.
        /// </summary>
        /// <value>
        /// The event's description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the start time of the event.
        /// </summary>
        /// <value>
        /// The start time of the event.
        /// </value>
        public EventTime Start { get; set; }

        /// <summary>
        /// Gets or sets the end time of the event.
        /// </summary>
        /// <value>
        /// The end time of the event.
        /// </value>
        public EventTime End { get; set; }

        /// <summary>
        /// Gets or sets the location of the event.
        /// </summary>
        /// <value>
        /// The location of the event.
        /// </value>
        public Location Location { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this event has been
        /// deleted.
        /// </summary>
        /// <value>
        /// <c>true</c> if the event has been deleted; otherwise,
        /// <c>false</c>.
        /// </value>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the account's participation status.
        /// </summary>
        /// <value>
        /// The account's participation status.
        /// </value>
        /// <remarks>
        /// See <see cref="Cronofy.AttendeeStatus"/> for potential values.
        /// </remarks>
        public string ParticipationStatus { get; set; }

        /// <summary>
        /// Gets or sets the transparency of the event.
        /// </summary>
        /// <value>
        /// The transparency of the event.
        /// </value>
        /// <remarks>
        /// See <see cref="Cronofy.Transparency"/> for potential values.
        /// </remarks>
        public string Transparency { get; set; }

        /// <summary>
        /// Gets or sets the status of the event.
        /// </summary>
        /// <value>
        /// The status of the event.
        /// </value>
        /// <remarks>
        /// See <see cref="Cronofy.EventStatus"/> for potential values.
        /// </remarks>
        public string EventStatus { get; set; }

        /// <summary>
        /// Gets or sets the categories assigned to the event.
        /// </summary>
        /// <value>
        /// The categories assigned to the event.
        /// </value>
        public string[] Categories { get; set; }

        /// <summary>
        /// Gets or sets the time the event was created.
        /// </summary>
        /// <value>
        /// The time the event was created.
        /// </value>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the time the event was last updated.
        /// </summary>
        /// <value>
        /// The time the event was last updated.
        /// </value>
        public DateTime Updated { get; set; }

        /// <summary>
        /// Gets or sets the event's attendees.
        /// </summary>
        /// <value>
        /// The event's attendees.
        /// </value>
        public Attendee[] Attendees { get; set; }

        /// <summary>
        /// Gets or sets the event's organizer.
        /// </summary>
        /// <value>
        /// The event's Organizer
        /// </value>
        public Organizer Organizer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this event is a
        /// recurring event.
        /// </summary>
        /// <value>
        /// <c>true</c> if the event is a recurring event; otherwise,
        /// <c>false</c>.
        /// </value>
        public bool Recurring { get; set; }

        /// <summary>
        /// Gets or sets the permissable options for this event.
        /// </summary>
        /// <value>
        /// The event's options.
        /// </value>
        public EventOptions Options { get; set; }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.EventUid.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var other = obj as Event;

            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Cronofy.Event"/> is
        /// equal to the current <see cref="Cronofy.Event"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Cronofy.Event"/> to compare with the current
        /// <see cref="Cronofy.Event"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Cronofy.Event"/> is equal to
        /// the current <see cref="Cronofy.Event"/>; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Event other)
        {
            return other != null
                && this.EventUid == other.EventUid
                && this.EventId == other.EventId
                && this.CalendarId == other.CalendarId
                && this.Created == other.Created
                && this.Updated == other.Updated
                && this.Deleted == other.Deleted
                && this.Summary == other.Summary
                && this.Description == other.Description
                && this.ParticipationStatus == other.ParticipationStatus
                && this.Transparency == other.Transparency
                && this.EventStatus == other.EventStatus
                && this.Recurring == other.Recurring
                && object.Equals(this.Location, other.Location)
                && object.Equals(this.Start, other.Start)
                && object.Equals(this.End, other.End)
                && EnumerableUtils.NullTolerantSequenceEqual(this.Attendees, other.Attendees)
                && EnumerableUtils.NullTolerantSequenceEqual(this.Categories, other.Categories);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} CalendarId={1}, EventId={2}, EventUid={3}, Summary={4}, Start={5}, End={6}, Deleted={7}, Recurring={8}, Attendees={9}>",
                this.GetType(),
                this.CalendarId,
                this.EventId,
                this.EventUid,
                this.Summary,
                this.Start,
                this.End,
                this.Deleted,
                this.Recurring,
                this.Attendees);
        }
    }
}
