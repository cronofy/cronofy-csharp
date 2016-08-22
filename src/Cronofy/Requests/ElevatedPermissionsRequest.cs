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
        [JsonProperty("permissions")]
        public IList<CalendarPermission> Permissions { get; set; }

        /// <summary>
        /// Gets or sets the redirect url for the request.
        /// </summary>
        [JsonProperty("redirect_uri")]
        public string RedirectUri { get; set; }

        /// <summary>
        /// Class for the serialization of an elevated permission
        /// for a single calendar.
        /// </summary>
        public sealed class CalendarPermission
        {
            /// <summary>
            /// Gets or sets the calendar id for the request.
            /// </summary>
            [JsonProperty("calendar_id")]
            public string CalendarId { get; set; }

            /// <summary>
            /// Gets or sets the permission level for the request.
            /// </summary>
            [JsonProperty("permission_level")]
            public string PermissionLevel { get; set; }
        }
    }
}