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
        [JsonProperty("permissions_request")]
        public PermissionsRequestResponse PermissionsRequest { get; set; }

        /// <summary>
        /// Converts the response to an instance of
        /// <see cref="Cronofy.ElevatedPermissionsResponse"/>.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="Cronofy.ElevatedPermissionsResponse"/>.
        /// </returns>
        public Cronofy.ElevatedPermissionsResponse ToElevatedPermissions()
        {
            return new Cronofy.ElevatedPermissionsResponse()
            {
                Url = this.PermissionsRequest.Url,
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
