namespace Cronofy
{
    /// <summary>
    /// Class for the configuration of the Cronofy SDK.
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// The default data centre.
        /// </summary>
        private static DataCentre defaultDataCentre;

        /// <summary>
        /// Gets or sets the default data centre.
        /// </summary>
        /// <value>
        /// The default data centre.
        /// </value>
        public static DataCentre DefaultDataCentre
        {
            get
            {
                return defaultDataCentre ?? DataCentre.Default;
            }

            set
            {
                defaultDataCentre = value;
            }
        }
    }
}
