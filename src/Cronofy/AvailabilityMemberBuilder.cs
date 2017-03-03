namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using Requests;

    /// <summary>
    /// Class to build <see cref="AvailabilityRequest.Member"/>s.
    /// </summary>
    public sealed class AvailabilityMemberBuilder : IBuilder<AvailabilityRequest.Member>
    {
        /// <summary>
        /// The sub of the member.
        /// </summary>
        private string sub;

        /// <summary>
        /// The available periods for the member.
        /// </summary>
        private IList<AvailabilityRequest.AvailablePeriod> availablePeriods;

        /// <summary>
        /// The request's calendar IDs.
        /// </summary>
        private IEnumerable<string> calendarIds;

        /// <summary>
        /// Sets the sub of the member.
        /// </summary>
        /// <param name="sub">
        /// The sub of the member.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="AvailabilityMemberBuilder"/>.
        /// </returns>
        public AvailabilityMemberBuilder Sub(string sub)
        {
            Preconditions.NotBlank("sub", sub);

            this.sub = sub;

            return this;
        }

        /// <summary>
        /// Adds an available period to the member.
        /// </summary>
        /// <param name="start">
        /// The start of the available period.
        /// </param>
        /// <param name="end">
        /// The end of the available period.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="AvailabilityMemberBuilder"/>.
        /// </returns>
        public AvailabilityMemberBuilder AddAvailablePeriod(DateTimeOffset start, DateTimeOffset end)
        {
            var period = new AvailabilityRequest.AvailablePeriod
            {
                Start = start,
                End = end,
            };

            if (this.availablePeriods == null)
            {
                this.availablePeriods = new List<AvailabilityRequest.AvailablePeriod>();
            }

            this.availablePeriods.Add(period);

            return this;
        }

        /// <summary>
        /// Sets the calendar IDs for the request.
        /// </summary>
        /// <param name="calendarIds">
        /// The calendar IDs to restrict the availability query to, must not be
        /// null.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="calendarIds"/> is null.
        /// </exception>
        public AvailabilityMemberBuilder CalendarIds(IEnumerable<string> calendarIds)
        {
            Preconditions.NotNull("calendarIds", calendarIds);

            this.calendarIds = calendarIds;
            return this;
        }

        /// <summary>
        /// Sets the calendar ID for the request.
        /// </summary>
        /// <param name="calendarId">
        /// The calendar ID to restrict the availability query to, must not be
        /// null.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="calendarId"/> is null.
        /// </exception>
        public AvailabilityMemberBuilder CalendarId(string calendarId)
        {
            Preconditions.NotNull("calendarId", calendarId);

            return this.CalendarIds(new[] { calendarId });
        }

        /// <inheritdoc />
        public AvailabilityRequest.Member Build()
        {
            return new AvailabilityRequest.Member
            {
                Sub = this.sub,
                AvailablePeriods = this.availablePeriods,
                CalendarIds = this.calendarIds,
            };
        }
    }
}
