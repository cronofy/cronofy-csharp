namespace Cronofy.Requests
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of an elevated permissions request.
    /// </summary>
    public sealed class ElevatedPermissionsRequest
    {
        /// <summary>
        /// Gets or sets the permissions for the request.
        /// </summary>
        /// <value>
        /// The permissions for the request.
        /// </value>
        [JsonProperty("permissions")]
        public IList<CalendarPermission> Permissions { get; set; }

        /// <summary>
        /// Gets or sets the redirect URI for the request.
        /// </summary>
        /// <value>
        /// The redirect URI for the request.
        /// </value>
        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        /// <summary>
        /// Class for the serialization of an elevated permission
        /// for a single calendar.
        /// </summary>
        public sealed class CalendarPermission
        {
            /// <summary>
            /// Gets or sets the calendar ID for the request.
            /// </summary>
            /// <value>
            /// The calendar ID for the request.
            /// </value>
            [JsonProperty("calendar_id")]
            public string CalendarId { get; set; }

            /// <summary>
            /// Gets or sets the permission level for the request.
            /// </summary>
            /// <value>
            /// The permission level for the request.
            /// </value>
            [JsonProperty("permission_level")]
            public string PermissionLevel { get; set; }
        }
    }
}
