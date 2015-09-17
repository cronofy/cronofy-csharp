using System;
using System.Collections.Generic;

namespace Cronofy
{
	internal sealed class HttpResponse
	{
		public int Code { get; set; }
		public IDictionary<string, string> Headers { get; set; }
		public string Body { get; set; }
	}
}
