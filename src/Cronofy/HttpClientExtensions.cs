using System;
using Newtonsoft.Json;

namespace Cronofy
{
    internal static class HttpClientExtensions
    {
        internal static JsonSerializerSettings DefaultSerializerSettings =
            new JsonSerializerSettings {
                DateParseHandling = DateParseHandling.None,
            };
        
        public static T GetJsonResponse<T>(this IHttpClient httpClient, HttpRequest request)
        {
            var response = httpClient.GetResponse(request);

            return JsonConvert.DeserializeObject<T>(response.Body, DefaultSerializerSettings);
        }
    }
}
