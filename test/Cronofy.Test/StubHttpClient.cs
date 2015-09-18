using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cronofy.Test
{
	internal sealed class StubHttpClient : IHttpClient
	{
		private readonly IDictionary<string, StubRequest> stubbedRequests;

		public StubHttpClient()
		{
			this.stubbedRequests = new Dictionary<string, StubRequest>();
		}

		public StubHttpClient Stub(StubRequestBuilder builder)
		{
			var request = builder.Build();
			var key = GetRequestKey(request.Method, request.Url, request.RequestHeaders, request.RequestBody);

			this.stubbedRequests.Add(key, request);

			return this;
		}

		public HttpResponse GetResponse(HttpRequest request)
		{
			var url = request.Url;

			if (request.QueryString != null && request.QueryString.Any())
			{
				var encodedQueryString = ToQueryString(request.QueryString);
				url = string.Format("{0}?{1}", url, encodedQueryString);
			}

			var key = GetRequestKey(request.Method, url, request.Headers, request.Body);

			if (stubbedRequests.ContainsKey(key) == false)
			{
				throw new ArgumentException(
					"\n\n----------\n\n" +
					"No stub found\n" +
					"=============\n\n" +
					key +
					"\n\n\n" +
					"Known stubs" +
					"\n============\n\n" +
					string.Join("\n\n-----\n\n", stubbedRequests.Keys.ToArray()) +
					"\n\n"
				);
			}

			var stub = stubbedRequests[key];

			return new HttpResponse {
				Code = stub.ResponseCode,
				Headers = new Dictionary<string, string>(),
				Body = stub.ResponseBody,
			};
		}

		private static string GetRequestKey(string method, string url, IEnumerable<KeyValuePair<string, string>> headers, string body)
		{
			var requestKey = new StringBuilder();

			requestKey.AppendFormat("{0} {1}", method, url);

			var allHeaders = headers.ToList();

			if (allHeaders.Any())
			{
				requestKey.AppendLine();
			}

			foreach (var header in allHeaders)
			{
				requestKey.AppendFormat("{0}: {1}\n", header.Key, header.Value);
			}

			if (string.IsNullOrEmpty(body) == false)
			{
				requestKey.AppendLine();
				requestKey.Append(body);
			}

			return requestKey.ToString();
		}

		private static string ToQueryString(IEnumerable<KeyValuePair<string, string>> queryString)
		{
			var encodedPairs = new List<string>();

			foreach (var qs in queryString)
			{
				encodedPairs.Add(string.Format("{0}={1}", UrlBuilder.EncodeParameter(qs.Key), UrlBuilder.EncodeParameter(qs.Value)));
			}

			return string.Join("&", encodedPairs.ToArray());
		}
	}
}
