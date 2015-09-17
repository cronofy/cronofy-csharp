using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cronofy
{
	internal interface IHttpClient
	{
		HttpResponse GetResponse(HttpRequest request);
	}
}
