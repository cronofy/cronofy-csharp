namespace Cronofy
{
    /// <summary>
    /// Potential transparency values.
    /// </summary>
    public static class Transparency
    {
        /// <summary>
        /// The account should appear as busy for the duration of the event.
        /// </summary>
        public const string Opaque = "opaque";

        /// <summary>
        /// The account should not appear as busy for the duration of the event.
        /// </summary>
        public const string Transparent = "transparent";

        /// <summary>
        /// The appearance of the account for the duration of the event is not
        /// known.
        /// </summary>
        public const string Unknown = "unknown";
    }
}
