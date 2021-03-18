namespace Cronofy
{
    /// <summary>
    /// Class for the configuration of the Cronofy SDK.
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// The default data center identifier.
        /// </summary>
        private static string defaultDataCenter;

        /// <summary>
        /// Gets or sets the default data center identifier.
        /// See our list of data centers at https://docs.cronofy.com/developers/data-centers/ and use the "SDK identifier".
        /// </summary>
        /// <value>
        /// The default data center identifier.
        /// </value>
        public static string DefaultDataCenter
        {
            get
            {
                return defaultDataCenter ?? "us";
            }

            set
            {
                defaultDataCenter = value;
            }
        }
    }
}
