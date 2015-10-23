namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// Concrete implementation of the <see cref="IHttpClient"/> interface that
    /// utilizes <see cref="HttpWebRequest"/>.
    /// </summary>
    internal sealed class ConcreteHttpClient : IHttpClient
    {
        /// <summary>
        /// The user agent string to use for all requests.
        /// </summary>
        private static readonly string UserAgentString;

        /// <summary>
        /// A collection of headers that cannot be assigned in a dictionary-like
        /// manner, and the corresponding actions to use to set them instead.
        /// </summary>
        private static readonly IDictionary<string, Header.Assignment> RestrictedHeaders;

        /// <summary>
        /// Initializes static members of the
        /// <see cref="Cronofy.ConcreteHttpClient"/> class.
        /// </summary>
        static ConcreteHttpClient()
        {
            UserAgentString = string.Format(
                "Cronofy .NET {0}",
                typeof(ConcreteHttpClient).Assembly.GetName().Version);

            RestrictedHeaders = new Dictionary<string, Header.Assignment>();
            RestrictedHeaders.Add("Content-Type", Header.SetContentType);
        }

        /// <inheritdoc/>
        public HttpResponse GetResponse(HttpRequest request)
        {
            Preconditions.NotNull("request", request);

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
            httpRequest.UserAgent = UserAgentString;

            MapHeaders(request, httpRequest);
            WriteRequestBody(request, httpRequest);

            return GetResponse(httpRequest);
        }

        /// <summary>
        /// Maps the headers onto the given <see cref="HttpWebRequest"/>.
        /// </summary>
        /// <param name="request">
        /// The request being mapped from.
        /// </param>
        /// <param name="httpRequest">
        /// The request being mapped to.
        /// </param>
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

        /// <summary>
        /// Writes the request body to the given <see cref="HttpWebRequest"/>.
        /// </summary>
        /// <param name="request">
        /// The request containing the details of the body to be written.
        /// </param>
        /// <param name="httpRequest">
        /// The request to write the request to.
        /// </param>
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

        /// <summary>
        /// Gets the response to the fully populated request.
        /// </summary>
        /// <param name="httpRequest">
        /// The request to invoke.
        /// </param>
        /// <returns>
        /// The response to the request.
        /// </returns>
        private static HttpResponse GetResponse(HttpWebRequest httpRequest)
        {
            using (var httpResponse = GetAnyResponse(httpRequest))
            {
                var responseBody = GetResponseBody(httpResponse);

                var response = new HttpResponse();

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

        /// <summary>
        /// Gets any valid response instead of throwing an exception depending
        /// on the status code.
        /// </summary>
        /// <param name="httpRequest">
        /// The request to make.
        /// </param>
        /// <returns>
        /// The response to the request.
        /// </returns>
        private static HttpWebResponse GetAnyResponse(HttpWebRequest httpRequest)
        {
            try
            {
                return (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                var response = ex.Response as HttpWebResponse;

                if (response == null)
                {
                    var message = string.Format(
                        "Failed to get a response to the request - {0}",
                        ex.Message);
                    throw new CronofyException(message, ex);
                }

                return response;
            }
        }

        /// <summary>
        /// Gets the body of the response.
        /// </summary>
        /// <param name="response">
        /// The response to extract the body from.
        /// </param>
        /// <returns>
        /// The response body.
        /// </returns>
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

        /// <summary>
        /// Class to help manage the assignment of restricted headers.
        /// </summary>
        internal static class Header
        {
            /// <summary>
            /// Delegate for the assignment of restricted headers.
            /// </summary>
            /// <param name="request">
            /// The target of the header assignment.
            /// </param>
            /// <param name="value">
            /// The value to assign the header to.
            /// </param>
            internal delegate void Assignment(HttpWebRequest request, string value);

            /// <summary>
            /// Sets the content type of the request.
            /// </summary>
            /// <param name="request">
            /// The request to set the content type of.
            /// </param>
            /// <param name="value">
            /// The value to set as the content type of the request.
            /// </param>
            internal static void SetContentType(HttpWebRequest request, string value)
            {
                request.ContentType = value;
            }
        }
    }
}
