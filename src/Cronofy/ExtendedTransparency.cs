namespace Cronofy
{
    /// <summary>
    /// Potential extended_transparency values.
    /// </summary>
    public static class ExtendedTransparency
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
        /// Indicates the user is working away from their normal site.
        /// </summary>
        public const string WorkingElsewhere = "working_elsewhere";

        /// <summary>
        /// Indicates an event being only tentatively accepted.
        /// </summary>
        public const string Tentative = "tentative";

        /// <summary>
        /// Indicates the user is unavailable due to being out of the office, such as being on vacation.
        /// </summary>
        public const string OutOfOffice = "out_of_office";

        /// <summary>
        /// The appearance of the account for the duration of the event is not
        /// known.
        /// </summary>
        public const string Unknown = "unknown";
    }
}
