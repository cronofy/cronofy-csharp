using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Cronofy.Responses;
using Cronofy.Requests;
using Cronofy;

namespace Cronofy
{
	public sealed class Location
	{
		// TODO Remove this attribute, instead create a HTTP-specific class
		[JsonProperty("description")]
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
