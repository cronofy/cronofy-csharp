namespace Cronofy
{
    using System;
    using System.Net;
    using System.Text;
    using System.IO;
    using System.Collections.Generic;

    internal sealed class ConcreteHttpClient : IHttpClient
    {
        private static readonly IDictionary<string, Header.Assignment> RestrictedHeaders
            = new Dictionary<string, Header.Assignment>();

        static ConcreteHttpClient()
        {
            RestrictedHeaders.Add("Content-Type", Header.SetContentType);
        }

        public HttpResponse GetResponse(HttpRequest request)
        {
            var urlBuilder = new UrlBuilder().Url(request.Url);

            if (request.QueryString != null)
            {
                foreach (var item in request.QueryString)
                {
                    urlBuilder.AddParameter(item.Key, item.Value);
                }
            }

            var url = urlBuilder.Build();

            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            httpRequest.KeepAlive = true;
            httpRequest.Method = request.Method;
            httpRequest.UserAgent = "Cronofy .NET";

            MapHeaders(request, httpRequest);
            WriteRequestBody(request, httpRequest);

            return GetResponse(httpRequest);
        }

        private static void MapHeaders(HttpRequest request, HttpWebRequest httpRequest)
        {
            foreach (var item in request.Headers)
            {
                if (RestrictedHeaders.ContainsKey(item.Key))
                {
                    RestrictedHeaders[item.Key].Invoke(httpRequest, item.Value);
                }
                else
                {
                    httpRequest.Headers[item.Key] = item.Value;
                }
            }
        }

        private static void WriteRequestBody(HttpRequest request, HttpWebRequest httpRequest)
        {
            if (string.IsNullOrEmpty(request.Body))
            {
                return;
            }

            var bodyBytes = Encoding.UTF8.GetBytes(request.Body);

            httpRequest.ContentLength = bodyBytes.Length;

            using (var stream = httpRequest.GetRequestStream())
            {
                stream.Write(bodyBytes, 0, bodyBytes.Length);
            }
        }

        private static HttpResponse GetResponse(HttpWebRequest httpRequest)
        {
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

        internal static class Header
        {
            internal delegate void Assignment(HttpWebRequest request, string value);

            internal static void SetContentType(HttpWebRequest request, string value)
            {
                request.ContentType = value;
            }
        }
    }
}
