namespace Cronofy
{
    /// <summary>
    /// Class representing a response to a request for elevated permissions.
    /// </summary>
    public sealed class ElevatedPermissions
    {
        /// <summary>
        /// Gets or sets the Url to redirect the user to in order to grant the
        /// permissions requested.
        /// </summary>
        public string Url { get; set; }
    }
}