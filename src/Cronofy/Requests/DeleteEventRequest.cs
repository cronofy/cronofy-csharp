using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Cronofy.Requests
{
    /// <summary>
    /// Class for the serialization of a delete event request.
    /// </summary>
    public sealed class DeleteEventRequest
    {
        /// <summary>
        /// Gets or sets the event ID.
        /// </summary>
        [JsonProperty("event_id")]
        public string EventId { get; set; }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            var a = obj as DeleteEventRequest;

            return a != null && this.Equals(a);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (this.EventId != null ? this.EventId.GetHashCode() : 0);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format("<{0} EventId: {1}>", this.GetType(), this.EventId);
        }

        private bool Equals(DeleteEventRequest other)
        {
            return string.Equals(this.EventId, other.EventId);
        }
    }
}
