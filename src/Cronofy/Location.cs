using System;
using Cronofy;

namespace Cronofy
{
	public sealed class Location
	{
		public string Description { get; set; }

		public override int GetHashCode()
		{
			return this.Description.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			var other = obj as Location;

			if (other == null)
			{
				return false;
			}

			return Equals(other);
		}

		public bool Equals(Location other)
		{
			return other != null
				&& this.Description == other.Description;
		}
	}
}
