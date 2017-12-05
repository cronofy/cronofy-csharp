namespace Cronofy.Requests
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the serialization of a delete external event request.
    /// </summary>
    public sealed class DeleteExternalEventRequest
    {
        /// <summary>
        /// Gets or sets the event UID.
        /// </summary>
        /// <value>
        /// The event UID.
        /// </value>
        [JsonProperty("event_uid")]
        public string EventUid { get; set; }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var a = obj as DeleteExternalEventRequest;

            return a != null && this.Equals(a);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.EventUid != null ? this.EventUid.GetHashCode() : 0;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format("<{0} EventUid={1}>", this.GetType(), this.EventUid);
        }

        /// <summary>
        /// Determines whether the specified
        /// <see cref="DeleteExternalEventRequest"/> is equal to the current
        /// <see cref="DeleteExternalEventRequest"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="DeleteExternalEventRequest"/> to compare with the
        /// current <see cref="DeleteExternalEventRequest"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="DeleteEventRequest"/> is
        /// equal to the current <see cref="DeleteEventRequest"/>; otherwise,
        /// <c>false</c>.
        /// </returns>
        private bool Equals(DeleteExternalEventRequest other)
        {
            return string.Equals(this.EventUid, other.EventUid);
        }
    }
}
