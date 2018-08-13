namespace Cronofy.Requests
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of an real time sequencing request.
    /// </summary>
    public abstract class RealTimeSchedulingBaseRequest
    {
        /// <summary>
        /// Gets or sets the client id for the request.
        /// </summary>
        /// <value>
        /// The client id for the request.
        /// </value>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret for the request.
        /// </summary>
        /// <value>
        /// The client secret for the request.
        /// </value>
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the oauth details for the request.
        /// </summary>
        /// <value>
        /// The oauth details for the request.
        /// </value>
        [JsonProperty("oauth")]
        public OAuthDetails OAuth { get; set; }

        /// <summary>
        /// Gets or sets the event details for the request.
        /// </summary>
        /// <value>
        /// The event details for the request.
        /// </value>
        [JsonProperty("event")]
        public UpsertEventRequest Event { get; set; }

        /// <summary>
        /// Gets or sets the target calendars for the request.
        /// </summary>
        /// <value>
        /// The target calendars for the request.
        /// </value>
        [JsonProperty("target_calendars")]
        public IEnumerable<TargetCalendar> TargetCalendars { get; set; }

        /// <summary>
        /// Gets or sets the formatting for the request.
        /// </summary>
        /// <value>
        /// The formatting for the request.
        /// </value>
        [JsonProperty("formatting")]
        public SchedulingFormatting Formatting { get; set; }

        /// <summary>
        /// Gets or sets the timezone id for the request.
        /// </summary>
        /// <value>
        /// The timezone id for the request.
        /// </value>
        [JsonProperty("tzid")]
        public string Tzid { get; set; }

        /// <summary>
        /// Class for the serialization of the oauth details.
        /// </summary>
        public sealed class OAuthDetails
        {
            /// <summary>
            /// Gets or sets the oauth redirect uri.
            /// </summary>
            /// <value>
            /// The redirect uri for the oauth flow.
            /// </value>
            [JsonProperty("redirect_uri")]
            public string RedirectUri { get; set; }

            /// <summary>
            /// Gets or sets the oauth scopes.
            /// </summary>
            /// <value>
            /// The scopes of the oauth flow.
            /// </value>
            [JsonProperty("scope")]
            public string Scope { get; set; }

            /// <summary>
            /// Gets or sets the oauth state.
            /// </summary>
            /// <value>
            /// The state for the oauth flow.
            /// </value>
            [JsonProperty("state")]
            public string State { get; set; }
        }

        /// <summary>
        /// Class for the serialization of the target calendars.
        /// </summary>
        public sealed class TargetCalendar
        {
            /// <summary>
            /// Gets or sets the sub for the target calendar.
            /// </summary>
            /// <value>
            /// The sub for the target calendar.
            /// </value>
            [JsonProperty("sub")]
            public string Sub { get; set; }

            /// <summary>
            /// Gets or sets the ID for the target calendar.
            /// </summary>
            /// <value>
            /// The ID for the target calendar.
            /// </value>
            [JsonProperty("calendar_id")]
            public string CalendarId { get; set; }
        }
    }
}
