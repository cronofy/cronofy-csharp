namespace Cronofy
{
    /// <summary>
    /// Potential event status values.
    /// </summary>
    public static class EventStatus
    {
        /// <summary>
        /// Status indicating the event has been cancelled.
        /// </summary>
        public const string Cancelled = "cancelled";

        /// <summary>
        /// Status indicating the details of the event are confirmed.
        /// </summary>
        public const string Confirmed = "confirmed";

        /// <summary>
        /// Status indicating the details of the event are tentative.
        /// </summary>
        public const string Tentative = "tentative";

        /// <summary>
        /// Status indicating the status of the event is unknown.
        /// </summary>
        public const string Unknown = "unknown";
    }
}
