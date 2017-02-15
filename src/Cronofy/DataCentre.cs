namespace Cronofy
{
    /// <summary>
    /// Class representing a data centre.
    /// </summary>
    public sealed class DataCentre
    {
        /// <summary>
        /// The default data centre.
        /// </summary>
        public static readonly DataCentre Default = new DataCentre(null);

        /// <summary>
        /// The US data centre.
        /// </summary>
        public static readonly DataCentre US = new DataCentre("us");

        /// <summary>
        /// The German data centre.
        /// </summary>
        public static readonly DataCentre German = new DataCentre("de");

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Cronofy.DataCentre"/> class.
        /// </summary>
        /// <param name="identifier">
        /// The identifier of the data centre.
        /// </param>
        public DataCentre(string identifier)
        {
            this.Identifier = identifier;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Identifier
        {
            get;
            private set;
        }
    }
}
