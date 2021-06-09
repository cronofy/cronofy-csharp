namespace Cronofy.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of a calendar list response.
    /// </summary>
    internal sealed class CalendarsResponse
    {
        /// <summary>
        /// Gets or sets the array of calendars.
        /// </summary>
        /// <value>
        /// The array of calendars.
        /// </value>
        [JsonProperty("calendars")]
        public CalendarResponse[] Calendars { get; set; }

        /// <summary>
        /// Class for the deserialization of a calendar response.
        /// </summary>
        internal sealed class CalendarResponse
        {
            /// <summary>
            /// Gets or sets the name of the calendar provider.
            /// </summary>
            /// <value>
            /// The name of the calendar provider.
            /// </value>
            [JsonProperty("provider_name")]
            public string ProviderName { get; set; }

            /// <summary>
            /// Gets or sets the ID of the calendar's profile.
            /// </summary>
            /// <value>
            /// The ID of the calendar's profile.
            /// </value>
            [JsonProperty("profile_id")]
            public string ProfileId { get; set; }

            /// <summary>
            /// Gets or sets the name of the calendar's profile.
            /// </summary>
            /// <value>
            /// The name of the calendar's profile.
            /// </value>
            [JsonProperty("profile_name")]
            public string ProfileName { get; set; }

            /// <summary>
            /// Gets or sets the ID of the calendar.
            /// </summary>
            /// <value>
            /// The ID of the calendar.
            /// </value>
            [JsonProperty("calendar_id")]
            public string CalendarId { get; set; }

            /// <summary>
            /// Gets or sets the name of the calendar.
            /// </summary>
            /// <value>
            /// The name of the calendar.
            /// </value>
            [JsonProperty("calendar_name")]
            public string CalendarName { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this calendar is
            /// readonly.
            /// </summary>
            /// <value>
            /// <c>true</c> if the calendar readonly; otherwise, <c>false</c>.
            /// </value>
            [JsonProperty("calendar_readonly")]
            public bool CalendarReadonly { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this calendar has been
            /// deleted.
            /// </summary>
            /// <value>
            /// <c>true</c> if the calendar has been deleted; otherwise,
            /// <c>false</c>.
            /// </value>
            [JsonProperty("calendar_deleted")]
            public bool CalendarDeleted { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this calendar is the
            /// primary one for the profile.
            /// </summary>
            /// <value>
            /// <c>true</c> if the primary calendar for the profile; otherwise,
            /// <c>false</c>.
            /// </value>
            [JsonProperty("calendar_primary")]
            public bool CalendarPrimary { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether this calendar supports
            /// "integrated" conferencing.
            /// </summary>
            /// <value>
            /// <c>true</c> if the calendar supports integrated conferencing;
            /// otherwise, <c>false</c>.
            /// </value>
            [JsonProperty("calendar_integrated_conferencing_available")]
            public bool CalendarIntegratedConferencingAvailable { get; set; }

            /// <summary>
            /// Converts the response into a <see cref="Cronofy.Calendar"/>.
            /// </summary>
            /// <returns>
            /// A <see cref="Cronofy.Calendar"/> based upon the response.
            /// </returns>
            public Calendar ToCalendar()
            {
                var profile = new Calendar.ProfileSummary
                {
                    ProviderName = this.ProviderName,
                    ProfileId = this.ProfileId,
                    Name = this.ProfileName,
                };

                return new Calendar
                {
                    Profile = profile,
                    CalendarId = this.CalendarId,
                    Name = this.CalendarName,
                    ReadOnly = this.CalendarReadonly,
                    Deleted = this.CalendarDeleted,
                    Primary = this.CalendarPrimary,
                    IntegratedConferencingAvailable = this.CalendarIntegratedConferencingAvailable,
                };
            }
        }
    }
}
