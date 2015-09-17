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
			var key = GetRequestKey(request.Method, request.Url, request.Headers, request.Body);

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
			var encodedHeaders = new StringBuilder();

			foreach (var header in headers)
			{
				encodedHeaders.AppendFormat("{0}: {1}\n", header.Key, header.Value);
			}

			return string.Format("{0} {1}\n{2}\n{3}", method, url, encodedHeaders, body);
		}
	}
}
