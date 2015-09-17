using System;
using System.Collections.Generic;

namespace Cronofy
{
	internal sealed class HttpRequest
	{
		public string Method { get; set; }
		public string Url { get; set; }
		public IDictionary<string, string> Headers { get; set; }
		public string Body { get; set; }
	}
}
