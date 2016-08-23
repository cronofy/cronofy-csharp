namespace Cronofy
{
    /// <summary>
    /// Class representing a response to a request for elevated permissions.
    /// </summary>
    public sealed class ElevatedPermissionsResponse
    {
        /// <summary>
        /// Gets or sets the Url to redirect the user to in order to grant the
        /// permissions requested.
        /// </summary>
        /// /// <value>
        /// The URL to redirect the user to.
        /// </value>
        public string Url { get; set; }
    }
}
