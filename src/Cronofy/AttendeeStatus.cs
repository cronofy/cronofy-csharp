namespace Cronofy
{
    /// <summary>
    /// Potential attendee status values.
    /// </summary>
    public static class AttendeeStatus
    {
        /// <summary>
        /// Status indicating the attendee has accepted their invitation to the
        /// event.
        /// </summary>
        public const string Accepted = "accepted";

        /// <summary>
        /// Status indicating the attendee has declined their invitation to the
        /// event.
        /// </summary>
        public const string Declined = "declined";

        /// <summary>
        /// Status indicating the attendee has not responding to their
        /// invitation to the event.
        /// </summary>
        public const string NeedsAction = "needs_action";

        /// <summary>
        /// Status indicating the attendee has tentatively accepted their
        /// invitation to the event.
        /// </summary>
        public const string Tentative = "tentative";

        /// <summary>
        /// Status indicating the attendee's status is unknown.
        /// </summary>
        public const string Unknown = "unknown";
    }
}
