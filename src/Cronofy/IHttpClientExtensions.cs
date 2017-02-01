namespace Cronofy
{
    using Newtonsoft.Json;

    /// <summary>
    /// Extensions for the <see cref="IHttpClient"/> interface for common
    /// operations.
    /// </summary>
    internal static class IHttpClientExtensions
    {
        /// <summary>
        /// The default serializer settings for JSON.NET.
        /// </summary>
        private static readonly JsonSerializerSettings DefaultSerializerSettings =
            new JsonSerializerSettings { DateParseHandling = DateParseHandling.None };

        /// <summary>
        /// Performs the request and deserializes the JSON response into an
        /// instance of <typeparamref name="T" />.
        /// </summary>
        /// <returns>
        /// The response as an instance of <typeparamref name="T" />.
        /// </returns>
        /// <param name="httpClient">
        /// The HTTP client to perform the request with.
        /// </param>
        /// <param name="request">
        /// The request to perform.
        /// </param>
        /// <typeparam name="T">
        /// The type to deserialize the response to.
        /// </typeparam>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="request"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="CronofyResponseException">
        /// Thrown if the response does not have a 200-range status code.
        /// </exception>
        /// <remarks>
        /// TODO Document request-based exceptions.
        /// </remarks>
        public static T GetJsonResponse<T>(this IHttpClient httpClient, HttpRequest request)
        {
            var response = httpClient.GetValidResponse(request);

            return JsonConvert.DeserializeObject<T>(response.Body, DefaultSerializerSettings);
        }

        /// <summary>
        /// Performs the request and throws an <see cref="System.Exception"/> if
        /// the response does not have a 200-range status code.
        /// </summary>
        /// <returns>
        /// The response.
        /// </returns>
        /// <param name="httpClient">
        /// The HTTP client to perform the request with.
        /// </param>
        /// <param name="request">
        /// The request to perform.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="request"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="CronofyResponseException">
        /// Thrown if the response does not have a 200-range status code.
        /// </exception>
        /// <remarks>
        /// TODO Document request-based exceptions.
        /// </remarks>
        public static HttpResponse GetValidResponse(this IHttpClient httpClient, HttpRequest request)
        {
            var response = httpClient.GetResponse(request);

            if (response.Code / 100 == 2)
            {
                return response;
            }

            switch (response.Code)
            {
                case 401:
                    throw new CronofyResponseException("Access denied", response);
                case 404:
                    throw new CronofyResponseException("Not found", response);
                case 410:
                    throw new CronofyResponseException("Gone", response);
                case 422:
                    throw new CronofyResponseException("Validation failed", response);
                default:
                    throw new CronofyResponseException(string.Format("Request failed - code={0}", response.Code), response);
            }
        }
    }
}
