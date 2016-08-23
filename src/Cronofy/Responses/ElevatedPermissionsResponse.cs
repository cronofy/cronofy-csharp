namespace Cronofy.Responses
{
    using Newtonsoft.Json;

    /// <summary>
    /// Elevated permissions response.
    /// </summary>
    internal sealed class ElevatedPermissionsResponse
    {
        /// <summary>
        /// Gets or sets the permissions request.
        /// </summary>
        /// <value>
        /// The permissions request.
        /// </value>
        [JsonProperty("permission")]
        public PermissionsRequestResponse PermissionsRequest { get; set; }

        /// <summary>
        /// Converts the response to an instance of
        /// <see cref="ElevatedPermissions"/>.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="ElevatedPermissions"/>.
        /// </returns>
        public ElevatedPermissions ToElevatedPermissions()
        {
            return new ElevatedPermissions()
            {
                Url = this.PermissionsRequest.Url
            };
        }

        /// <summary>
        /// Permissions request response.
        /// </summary>
        internal sealed class PermissionsRequestResponse
        {
            /// <summary>
            /// Gets or sets the URL.
            /// </summary>
            /// <value>
            /// The URL.
            /// </value>
            [JsonProperty("url")]
            public string Url { get; set; }
        }
    }
}
