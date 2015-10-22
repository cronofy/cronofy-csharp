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
        public static T GetJsonResponse<T>(this IHttpClient httpClient, HttpRequest request)
        {
            var response = httpClient.GetResponse(request);

            return JsonConvert.DeserializeObject<T>(response.Body, DefaultSerializerSettings);
        }
    }
}
