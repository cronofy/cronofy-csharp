namespace Cronofy
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Helper for building URLs.
    /// </summary>
    internal sealed class UrlBuilder
    {
        /// <summary>
        /// The list of querystring parameters.
        /// </summary>
        private readonly List<string> parameters;

        /// <summary>
        /// The base URL.
        /// </summary>
        private string url;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cronofy.UrlBuilder"/>
        /// class.
        /// </summary>
        public UrlBuilder()
        {
            this.parameters = new List<string>();
        }

        /// <summary>
        /// Sets the base URL for the builder.
        /// <para>
        /// Generally the scheme, TLD, and path of the eventual URL.
        /// </para>
        /// </summary>
        /// <param name="url">
        /// The base URL, must not be null or empty.
        /// </param>
        /// <returns>
        /// A reference to the builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="url"/> is null or empty.
        /// </exception>
        public UrlBuilder Url(string url)
        {
            Preconditions.NotEmpty("url", url);

            this.url = url;

            return this;
        }

        /// <summary>
        /// Adds a querystring parameter to the URL.
        /// </summary>
        /// <param name="key">
        /// The key of the querystring parameter, must not be null or empty.
        /// </param>
        /// <param name="value">
        /// The value of the querystring parameter, must not be null.
        /// </param>
        /// <returns>
        /// A reference to the builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="key"/> is null or empty, or if
        /// <paramref name="value"/> is null.
        /// </exception>
        public UrlBuilder AddParameter(string key, string value)
        {
            Preconditions.NotEmpty("key", key);
            Preconditions.NotNull("value", value);

            var encodedKey = EncodeParameter(key);
            var encodedValue = EncodeParameter(value);

            var parameter = string.Format("{0}={1}", encodedKey, encodedValue);

            this.parameters.Add(parameter);

            return this;
        }

        /// <summary>
        /// Adds a querystring parameter to the URL.
        /// </summary>
        /// <param name="key">
        /// The key of the querystring parameter, must not be null or empty.
        /// </param>
        /// <param name="value">
        /// The value of the querystring parameter.
        /// </param>
        /// <returns>
        /// A reference to the builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="key"/> is null or empty.
        /// </exception>
        public UrlBuilder AddParameter(string key, bool value)
        {
            var stringValue = value.ToString().ToLowerInvariant();

            return this.AddParameter(key, stringValue);
        }

        /// <summary>
        /// Generates a URL based on the current state of the builder.
        /// </summary>
        /// <returns>
        /// A URL based on the current state of the builder.
        /// </returns>
        public string Build()
        {
            if (this.parameters.Count == 0)
            {
                return this.url;
            }

            var queryString = string.Join("&", this.parameters.ToArray());
            return string.Format("{0}?{1}", this.url, queryString);
        }

        /// <summary>
        /// Encodes the parameter.
        /// </summary>
        /// <param name="parameter">
        /// The parameter to encode.
        /// </param>
        /// <returns>
        /// The parameter in an encoded form.
        /// </returns>
        internal static string EncodeParameter(string parameter)
        {
            return Uri.EscapeDataString(parameter);
        }
    }
}
