namespace Cronofy.Requests
{
    using Newtonsoft.Json;

    /// <summary>
    /// A request to provision a new Cronofy application.
    /// </summary>
    public class ProvisionApplicationRequest
    {
        /// <summary>
        /// Gets or sets the name of your application, displayed to users during authorization.
        /// </summary>
        /// <value>
        /// The name of your application, displayed to users during authorization.
        /// </value>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL of your application. Usually the homepage of your application or similar.
        /// </summary>
        /// <value>
        /// The URL of your application. Usually the homepage of your application or similar.
        /// </value>
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
