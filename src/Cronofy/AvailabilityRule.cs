namespace Cronofy
{
    using System;
    using System.Linq;

    /// <summary>
    /// Class for representing an availability rule.
    /// </summary>
    public sealed class AvailabilityRule
    {
        /// <summary>
        /// Gets or sets the unique identifier of the availability rule.
        /// </summary>
        /// <value>
        /// The unique identifier of the availability rule.
        /// </value>
        public string AvailabilityRuleId { get; set; }

        /// <summary>
        /// Gets or sets the time zone for which the availability rule start and end times are represented in.
        /// </summary>
        /// <value>
        /// The time zone for which the availability rule start and end times are represented in.
        /// </value>
        public string TimeZoneId { get; set; }

        /// <summary>
        /// Gets or sets the calendars that should impact the user's availability.
        /// </summary>
        /// <value>
        /// The calendars that should impact the user's availability.
        /// </value>
        public string[] CalendarIds { get; set; }

        /// <summary>
        /// Gets or sets the weekly recurring periods for the availability rule.
        /// </summary>
        /// <value>
        /// The weekly recurring periods for the availability rule.
        /// </value>
        public WeeklyPeriod[] WeeklyPeriods { get; set; }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.AvailabilityRuleId.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is AvailabilityRule && this.Equals((AvailabilityRule)obj);
        }

        /// <summary>
        /// Determines whether the specified <see cref="AvailabilityRule"/>
        /// is equal to the current <see cref="AvailabilityRule"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="AvailabilityRule"/> to compare with the current
        /// <see cref="AvailabilityRule"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="AvailabilityRule"/> is
        /// equal to the current <see cref="AvailabilityRule"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        public bool Equals(AvailabilityRule other)
        {
            return this.AvailabilityRuleId == other.AvailabilityRuleId &&
                this.TimeZoneId == other.TimeZoneId &&
                this.CalendarIds.SequenceEqual(other.CalendarIds) &&
                this.WeeklyPeriods.SequenceEqual(other.WeeklyPeriods);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return string.Format(
                "<{0} AvailabilityRuleId={1}, TimeZoneId={2}, CalendarIds={3}, WeeklyPeriods={4}>",
                this.GetType(),
                this.AvailabilityRuleId,
                this.TimeZoneId,
                this.CalendarIds,
                string.Join(", ", this.WeeklyPeriods.Select(weeklyPeriod => weeklyPeriod.ToString())));
        }

        /// <summary>
        /// Class to represent a weekly period.
        /// </summary>
        public class WeeklyPeriod
        {
            /// <summary>
            /// Gets or sets the week day the period applies to.
            /// </summary>
            /// <value>
            /// The week day the period applies to.
            /// </value>
            public string Day { get; set; }

            /// <summary>
            /// Gets or sets the time of day the period should start.
            /// </summary>
            /// <value>
            /// The time of day the period should start.
            /// </value>
            public string StartTime { get; set; }

            /// <summary>
            /// Gets or sets the time of day the period should end.
            /// </summary>
            /// <value>
            /// The time of day the period should end.
            /// </value>
            public string EndTime { get; set; }

            /// <inheritdoc />
            public override int GetHashCode()
            {
                return this.Day.GetHashCode() ^ this.StartTime.GetHashCode() ^ this.EndTime.GetHashCode();
            }

            /// <inheritdoc/>
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }

                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                return obj is WeeklyPeriod && this.Equals((WeeklyPeriod)obj);
            }

            /// <summary>
            /// Determines whether the specified <see cref="WeeklyPeriod"/>
            /// is equal to the current <see cref="WeeklyPeriod"/>.
            /// </summary>
            /// <param name="other">
            /// The <see cref="WeeklyPeriod"/> to compare with the current
            /// <see cref="WeeklyPeriod"/>.
            /// </param>
            /// <returns>
            /// <c>true</c> if the specified <see cref="WeeklyPeriod"/> is
            /// equal to the current <see cref="WeeklyPeriod"/>; otherwise,
            /// <c>false</c>.
            /// </returns>
            public bool Equals(WeeklyPeriod other)
            {
                return this.Day == other.Day &&
                    this.StartTime == other.StartTime &&
                    this.EndTime == other.EndTime;
            }

            /// <inheritdoc/>
            public override string ToString()
            {
                return string.Format(
                    "<{0} Day={1}, StartTime={2}, EndTime={3}>",
                    this.GetType(),
                    this.Day,
                    this.StartTime,
                    this.EndTime);
            }
        }
    }
}
