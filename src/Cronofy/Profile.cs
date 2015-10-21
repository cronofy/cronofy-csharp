using System;

namespace Cronofy
{
    public sealed class Profile
    {
        public string ProviderName { get; set; }
        public string ProfileId { get; set; }
        public string Name { get; set; }

        public override int GetHashCode()
        {
            return this.ProfileId.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Profile;

            if (other == null)
            {
                return false;
            }

            return Equals(other);
        }

        public bool Equals(Profile other)
        {
            return other != null
                && this.ProfileId == other.ProfileId
                && this.Name == other.Name
                && this.ProviderName == other.ProviderName;
        }

        public override string ToString()
        {
            return string.Format(
                "<{0} ProviderName={1}, ProfileId={2}, Name={3}>",
                GetType(), ProviderName, ProfileId, Name);
        }
    }
}
