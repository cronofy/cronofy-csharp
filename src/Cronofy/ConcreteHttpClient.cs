using System;
using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Cronofy
{
	internal sealed class ConcreteHttpClient : IHttpClient
	{
		public HttpResponse GetResponse(HttpRequest request)
		{
			var urlBuilder = new UrlBuilder().Url(request.Url);

			foreach (var item in request.QueryString)
			{
				urlBuilder.AddParameter(item.Key, item.Value);
			}

			var url = urlBuilder.Build();

			var httpRequest = (HttpWebRequest)WebRequest.Create(url);
			httpRequest.KeepAlive = true;
			httpRequest.Method = request.Method;

			foreach (var item in request.Headers)
			{
				httpRequest.Headers[item.Key] = item.Value;
			}

			httpRequest.UserAgent = "Cronofy .NET";

			if (string.IsNullOrEmpty(request.Body) == false)
			{
				var bodyBytes = Encoding.UTF8.GetBytes(request.Body);

				httpRequest.ContentLength = bodyBytes.Length;

				using (var stream = httpRequest.GetRequestStream())
				{
					stream.Write(bodyBytes, 0, bodyBytes.Length);
				}
			}

			using (var httpResponse = (HttpWebResponse)httpRequest.GetResponse())
			{
				var responseBody = GetResponseBody(httpResponse);

				var response = new HttpResponse();

				// TODO Check 422 response as it isn't in the enum
				response.Code = (int)httpResponse.StatusCode;
				response.Headers = new Dictionary<string, string>();

				foreach (var key in httpResponse.Headers.AllKeys)
				{
					var value = httpResponse.GetResponseHeader(key);
					response.Headers.Add(key, value);
				}

				response.Body = responseBody;

				return response;
			}
		}

		private static string GetResponseBody(WebResponse response)
		{
			using (var stream = response.GetResponseStream())
			{
				if (stream == null)
				{
					return null;
				}

				using (var reader = new StreamReader(stream))
				{
					return reader.ReadToEnd();
				}
			}
		}
	}
}
