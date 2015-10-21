using System;
using System.Collections.Generic;

namespace Cronofy
{
    internal sealed class HttpRequest
    {
        public HttpRequest()
        {
            this.Headers = new Dictionary<string, string>();
            this.QueryString = new Dictionary<string, string>();
        }

        public string Method { get; set; }
        public string Url { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public IDictionary<string, string> QueryString { get; set; }
        public string Body { get; set; }
    }
}
