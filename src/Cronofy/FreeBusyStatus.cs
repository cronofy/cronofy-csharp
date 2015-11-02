namespace Cronofy
{
    /// <summary>
    /// Potential free-busy status values.
    /// </summary>
    public static class FreeBusyStatus
    {
        /// <summary>
        /// Status indicating the person is busy for the specified period.
        /// </summary>
        public const string Busy = "busy";

        /// <summary>
        /// Status indicating the person is free for the specified period.
        /// </summary>
        public const string Free = "free";

        /// <summary>
        /// Status indicating the person is probably busy for the specified
        /// period.
        /// </summary>
        public const string Tentative = "tentative";

        /// <summary>
        /// Status indicating the status of the person for the period is
        /// unknown.
        /// </summary>
        public const string Unknown = "unknown";
    }
}
