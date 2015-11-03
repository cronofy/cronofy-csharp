namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Class representing a HTTP request.
    /// </summary>
    internal sealed class HttpRequest
    {
        /// <summary>
        /// The default JSON serializer settings.
        /// </summary>
        private static readonly JsonSerializerSettings DefaultSerializerSettings =
            new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };

        /// <summary>
        /// Initializes a new instance of the <see cref="Cronofy.HttpRequest"/>
        /// class.
        /// </summary>
        public HttpRequest()
        {
            this.Headers = new Dictionary<string, string>();
            this.QueryString = new QueryStringCollection();
        }

        /// <summary>
        /// Gets or sets the HTTP method of the request.
        /// </summary>
        /// <value>
        /// The HTTP method of the request.
        /// </value>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the URL of the request.
        /// </summary>
        /// <value>
        /// The URL of the request.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the headers of the request.
        /// </summary>
        /// <value>
        /// The headers of the request.
        /// </value>
        public IDictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Gets or sets the query string parameters of the request.
        /// </summary>
        /// <value>
        /// The query string parameters of the request.
        /// </value>
        public QueryStringCollection QueryString { get; set; }

        /// <summary>
        /// Gets or sets the body of the request.
        /// </summary>
        /// <value>
        /// The body of the request.
        /// </value>
        public string Body { get; set; }

        /// <summary>
        /// Adds an OAuth authorization header to the request.
        /// </summary>
        /// <param name="accessToken">
        /// The OAuth access token to use for authorization, must not be empty.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="accessToken"/> is empty.
        /// </exception>
        public void AddOAuthAuthorization(string accessToken)
        {
            Preconditions.NotEmpty("accessToken", accessToken);

            this.Headers.Add("Authorization", "Bearer " + accessToken);
        }

        /// <summary>
        /// Sets the body of the request using a JSON-serialized version of the
        /// passed object.
        /// </summary>
        /// <param name="bodyObject">
        /// The object to serialize to JSON and assign as the request body, must
        /// not be <c>null</c>.
        /// </param>
        /// <remarks>
        /// Also sets the <c>Content-Type</c> header to indicate the request has
        /// a JSON-encoded body.
        /// </remarks>
        public void SetJsonBody(object bodyObject)
        {
            Preconditions.NotNull("bodyObject", bodyObject);

            this.Headers.Add("Content-Type", "application/json; charset=utf-8");

            this.Body = JsonConvert.SerializeObject(bodyObject, DefaultSerializerSettings);
        }

        /// <summary>
        /// Type conversion aware dictionary for building query strings.
        /// </summary>
        internal sealed class QueryStringCollection : Dictionary<string, string>
        {
            /// <summary>
            /// Add the specified key and value if one is present.
            /// </summary>
            /// <param name="key">
            /// The key to add the value under, must not be null.
            /// </param>
            /// <param name="nullable">
            /// The value to add, if one is present.
            /// </param>
            /// <exception cref="ArgumentException">
            /// Thrown if <paramref name="key"/> is null.
            /// </exception>
            public void Add(string key, bool? nullable)
            {
                Preconditions.NotNull("key", key);

                if (nullable.HasValue)
                {
                    this.Add(key, nullable.Value);
                }
            }

            /// <summary>
            /// Add the specified key and value.
            /// </summary>
            /// <param name="key">
            /// The key to add the value under, must not be null.
            /// </param>
            /// <param name="value">
            /// The value to add.
            /// </param>
            /// <exception cref="ArgumentException">
            /// Thrown if <paramref name="key"/> is null.
            /// </exception>
            public void Add(string key, bool value)
            {
                Preconditions.NotNull("key", key);

                this.Add(key, value.ToString().ToLowerInvariant());
            }

            /// <summary>
            /// Add the specified key and value if one is present.
            /// </summary>
            /// <param name="key">
            /// The key to add the value under, must not be null.
            /// </param>
            /// <param name="nullable">
            /// The value to add, if one is present.
            /// </param>
            /// <exception cref="ArgumentException">
            /// Thrown if <paramref name="key"/> is null.
            /// </exception>
            public void Add(string key, DateTime? nullable)
            {
                Preconditions.NotNull("key", key);

                if (nullable.HasValue)
                {
                    this.Add(key, nullable.Value);
                }
            }

            /// <summary>
            /// Add the specified key and value.
            /// </summary>
            /// <param name="key">
            /// The key to add the value under, must not be null.
            /// </param>
            /// <param name="value">
            /// The value to add.
            /// </param>
            /// <exception cref="ArgumentException">
            /// Thrown if <paramref name="key"/> is null.
            /// </exception>
            public void Add(string key, DateTime value)
            {
                Preconditions.NotNull("key", key);

                this.Add(key, value.ToString("u"));
            }

            /// <summary>
            /// Add the specified key and value if one is present.
            /// </summary>
            /// <param name="key">
            /// The key to add the value under, must not be null.
            /// </param>
            /// <param name="nullable">
            /// The value to add, if one is present.
            /// </param>
            /// <exception cref="ArgumentException">
            /// Thrown if <paramref name="key"/> is null.
            /// </exception>
            public void Add(string key, Date? nullable)
            {
                Preconditions.NotNull("key", key);

                if (nullable.HasValue)
                {
                    this.Add(key, nullable.Value);
                }
            }

            /// <summary>
            /// Add the specified key and value.
            /// </summary>
            /// <param name="key">
            /// The key to add the value under, must not be null.
            /// </param>
            /// <param name="value">
            /// The value to add.
            /// </param>
            /// <exception cref="ArgumentException">
            /// Thrown if <paramref name="key"/> is null.
            /// </exception>
            public void Add(string key, Date value)
            {
                Preconditions.NotNull("key", key);

                this.Add(key, value.ToString());
            }
        }
    }
}
