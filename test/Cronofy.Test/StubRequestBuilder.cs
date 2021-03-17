namespace Cronofy.Test
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public sealed class StubRequestBuilder
    {
        private readonly string method;
        private readonly string url;
        private readonly IList<KeyValuePair<string, string>> requestHeaders;
        private string requestBody;
        private int responseCode;
        private string responseBody;

        public StubRequestBuilder(string method, string url)
        {
            this.method = method;
            this.url = url;
            this.requestHeaders = new List<KeyValuePair<string, string>>();
        }

        public StubRequestBuilder RequestHeader(string name, string value)
        {
            var pair = new KeyValuePair<string, string>(name, value);
            this.requestHeaders.Add(pair);
            return this;
        }

        public StubRequestBuilder RequestBody(string body)
        {
            this.requestBody = body;
            return this;
        }

        public StubRequestBuilder RequestBodyFormat(string format, params object[] args)
        {
            return this.RequestBody(string.Format(format, args));
        }

        public StubRequestBuilder JsonRequest(string jsonBody)
        {
            this.RequestHeader("Content-Type", "application/json; charset=utf-8");

            var deserialized = JsonConvert.DeserializeObject(jsonBody);
            var generatedBody = JsonConvert.SerializeObject(deserialized);

            this.RequestBody(generatedBody);

            return this;
        }

        public StubRequestBuilder ResponseCode(int code)
        {
            this.responseCode = code;
            return this;
        }

        public StubRequestBuilder ResponseBody(string body)
        {
            this.responseBody = body;
            return this;
        }

        public StubRequestBuilder ResponseBodyFormat(string format, params object[] args)
        {
            return this.ResponseBody(string.Format(format, args));
        }

        public StubRequest Build()
        {
            return new StubRequest
            {
                Method = this.method,
                Url = this.url,
                RequestHeaders = this.requestHeaders,
                RequestBody = this.requestBody,
                ResponseCode = this.responseCode,
                ResponseBody = this.responseBody,
            };
        }
    }

#pragma warning disable SA1402 // Allow multiple types in single file
    public sealed class StubRequest
    {
        public string Method { get; set; }

        public string Url { get; set; }

        public IEnumerable<KeyValuePair<string, string>> RequestHeaders { get; set; }

        public string RequestBody { get; set; }

        public int ResponseCode { get; set; }

        public string ResponseBody { get; set; }
    }

    public static class HttpPost
    {
        public static StubRequestBuilder Url(string url)
        {
            return new StubRequestBuilder("POST", url);
        }
    }

    public static class HttpGet
    {
        public static StubRequestBuilder Url(string url)
        {
            return new StubRequestBuilder("GET", url);
        }
    }

    public static class HttpDelete
    {
        public static StubRequestBuilder Url(string url)
        {
            return new StubRequestBuilder("DELETE", url);
        }
    }
#pragma warning restore SA1402 // Disllow multiple types in single file
}
