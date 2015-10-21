using System;

namespace Cronofy
{
    public sealed class Attendee
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Status { get; set; }

        public override int GetHashCode()
        {
            return this.Email.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Attendee;

            if (other == null)
            {
                return false;
            }

            return Equals(other);
        }

        public bool Equals(Attendee other)
        {
            return other != null
                && this.Email == other.Email
                && this.DisplayName == other.DisplayName
                && this.Status == other.Status;
        }

        public override string ToString()
        {
            return string.Format(
                "<{0} Email={1}, DisplayName={2}, Status={3}>",
                GetType(), Email, DisplayName, Status);
        }
    }
}
