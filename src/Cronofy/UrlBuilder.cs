using System;
using System.Collections.Generic;
using System.Web;

namespace Cronofy
{
	internal sealed class UrlBuilder
	{
		private readonly List<string> parameters;
		private string url;

		public UrlBuilder()
		{
			this.parameters = new List<string>();
		}

		public UrlBuilder Url(string url)
		{
			this.url = url;
			return this;
		}

		public UrlBuilder AddParameter(string key, string value)
		{
			var encodedKey = HttpUtility.UrlPathEncode(key);
			var encodedValue = HttpUtility.UrlPathEncode(value);

			var parameter = string.Format("{0}={1}", encodedKey, encodedValue);

			this.parameters.Add(parameter);

			return this;
		}

		public string Build()
		{
			if (this.parameters.Count > 0)
			{
				var queryString = string.Join("&", this.parameters.ToArray());
				return string.Format("{0}?{1}", this.url, queryString);
			}
			else
			{
				return this.url;
			}
		}
	}
}
