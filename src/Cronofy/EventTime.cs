namespace Cronofy
{
    using System;

    /// <summary>
    /// Class representing a point in time.
    /// </summary>
    /// <remarks>
    /// In relation to events, times are polymorphic. They can refer to a date
    /// or a specific time. They also have some time zone information attached
    /// to them.
    /// </remarks>
    public sealed class EventTime
    {
        /// <summary>
        /// The date the current instance relates to.
        /// </summary>
        private readonly Date date;

        /// <summary>
        /// The date time offset the current instance relates to, if applicable.
        /// </summary>
        private readonly DateTimeOffset dateTimeOffset;

        /// <summary>
        /// The time zone of the current instance.
        /// </summary>
        private readonly string timeZoneId;

        /// <summary>
        /// Whether the current instance has a time component.
        /// </summary>
        private readonly bool hasTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cronofy.EventTime"/>
        /// class.
        /// </summary>
        /// <param name="date">
        /// The <see cref="Date"/> the instance relates to.
        /// </param>
        /// <param name="timeZoneId">
        /// The time zone identifier of the instance, must not be empty.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="timeZoneId"/> is empty.
        /// </exception>
        public EventTime(Date date, string timeZoneId)
        {
            Preconditions.NotEmpty("timeZoneId", timeZoneId);

            this.date = date;
            this.timeZoneId = timeZoneId;
            this.hasTime = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cronofy.EventTime"/> class.
        /// </summary>
        /// <param name="dateTimeOffset">
        /// The time the instance relates to.
        /// </param>
        /// <param name="timeZoneId">
        /// The time zone identifier of the instance, must not be empty.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="timeZoneId"/> is empty.
        /// </exception>
        public EventTime(DateTimeOffset dateTimeOffset, string timeZoneId)
        {
            Preconditions.NotEmpty("timeZoneId", timeZoneId);

            this.dateTimeOffset = dateTimeOffset;
            this.date = Date.From(dateTimeOffset);
            this.timeZoneId = timeZoneId;
            this.hasTime = true;
        }

        /// <summary>
        /// Gets a value indicating whether this instance has a time component.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has a time component; otherwise,
        /// <c>false</c>.
        /// </value>
        public bool HasTime
        {
            get
            {
                return this.hasTime;
            }
        }

        /// <summary>
        /// Gets the date this instance relates to.
        /// </summary>
        /// <value>
        /// The date this instance relates to.
        /// </value>
        public Date Date
        {
            get
            {
                return this.date;
            }
        }

        /// <summary>
        /// Gets the date time offset of this instance.
        /// </summary>
        /// <value>
        /// The date time offset of this instance.
        /// </value>
        /// <exception cref="ArgumentException">
        /// Thrown if this instance has no time component. Can be checked via
        /// <see cref="HasTime"/>.
        /// </exception>
        public DateTimeOffset DateTimeOffset
        {
            get
            {
                Preconditions.True(this.hasTime, "This EventTime has no time component");
                return this.dateTimeOffset;
            }
        }

        /// <summary>
        /// Gets the time zone identifier of this instance.
        /// </summary>
        /// <value>
        /// The time zone identifier of this instance.
        /// </value>
        public string TimeZoneId
        {
            get
            {
                return this.timeZoneId;
            }
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.date.GetHashCode() ^ this.dateTimeOffset.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            var other = obj as EventTime;

            if (other == null)
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Cronofy.EventTime"/> is
        /// equal to the current <see cref="Cronofy.EventTime"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Cronofy.EventTime"/> to compare with the current
        /// <see cref="Cronofy.EventTime"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Cronofy.EventTime"/> is
        /// equal to the current <see cref="Cronofy.EventTime"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public bool Equals(EventTime other)
        {
            return other != null
                && this.dateTimeOffset == other.dateTimeOffset
                && this.Date == other.Date
                && this.TimeZoneId == other.TimeZoneId;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            if (this.HasTime)
            {
                return string.Format(
                    "<EventTime DateTimeOffset={0}, TimeZoneId={1}>",
                    this.DateTimeOffset,
                    this.TimeZoneId);
            }

            return string.Format(
                "<EventTime Date={0}, TimeZoneId={1}>",
                this.Date,
                this.TimeZoneId);
        }
    }
}
