namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using Cronofy.Requests;

    /// <summary>
    /// Class to build a request for elevated permissions.
    /// </summary>
    public sealed class ElevatedPermissionsBuilder : IBuilder<ElevatedPermissionsRequest>
    {
        /// <summary>
        /// The list of calendar permissions.
        /// </summary>
        private readonly IList<CalendarPermission> calendarPermissions = new List<CalendarPermission>();

        /// <summary>
        /// The URI to redirect to once the user responds to the request.
        /// </summary>
        private string redirectUri;

        /// <summary>
        /// Adds a calendar a permission level to the request.
        /// </summary>
        /// <param name="calendarId">
        /// The calendar id to request permission for, must not be blank.
        /// </param>
        /// <param name="permissionLevel">
        /// The level of permission to request for the calendar id, must not be blank.
        /// </param>
        /// <exception cref="ArgumentException">
        /// If <para>calendarId</para> or <para>permissionLevel</para> are blank.
        /// </exception>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public ElevatedPermissionsBuilder AddCalendarPermission(string calendarId, string permissionLevel)
        {
            Preconditions.NotBlank("calendarId", calendarId);
            Preconditions.NotBlank("permissionLevel", permissionLevel);

            this.calendarPermissions.Add(new CalendarPermission()
            {
                CalendarId = calendarId,
                PermissionLevel = permissionLevel
            });

            return this;
        }

        /// <summary>
        /// Sets the redirect url for this request.
        /// </summary>
        /// <param name="redirectUri">
        /// The redirect url to send the user to after granting permissions,
        ///  must not be blank.
        /// </param>
        /// <exception cref="ArgumentException">
        /// If <para>calendarId</para> or <para>permissionLevel</para> are blank.
        /// </exception>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public ElevatedPermissionsBuilder RedirectUri(string redirectUri)
        {
            Preconditions.NotBlank("redirectUri", redirectUri);

            this.redirectUri = redirectUri;

            return this;
        }

        /// <inheritdoc/>
        public ElevatedPermissionsRequest Build()
        {
            var request = new ElevatedPermissionsRequest
            {
                RedirectUri = this.redirectUri,
                Permissions = new List<ElevatedPermissionsRequest.CalendarPermission>(),
            };

            foreach (var calendarPermission in this.calendarPermissions)
            {
                request.Permissions.Add(new ElevatedPermissionsRequest.CalendarPermission()
                {
                    CalendarId = calendarPermission.CalendarId,
                    PermissionLevel = calendarPermission.PermissionLevel
                });
            }

            return request;
        }

        /// <summary>
        /// Calendar permission.
        /// </summary>
        private sealed class CalendarPermission
        {
            /// <summary>
            /// Gets or sets the ID of the calendar the permission relates to.
            /// </summary>
            /// <value>
            /// The ID of the calendar calendar.
            /// </value>
            public string CalendarId { get; set; }

            /// <summary>
            /// Gets or sets the requested permission level.
            /// </summary>
            /// <value>
            /// The permission level.
            /// </value>
            public string PermissionLevel { get; set; }
        }
    }
}