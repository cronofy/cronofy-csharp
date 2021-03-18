namespace Cronofy
{
    using System.Collections.Generic;

    /// <summary>
    /// URL provider factory.
    /// </summary>
    internal static class UrlProviderFactory
    {
        /// <summary>
        /// The cache for generated <see cref="UrlProvider"/>s.
        /// </summary>
        private static readonly IDictionary<string, UrlProvider> ProviderCache =
            new Dictionary<string, UrlProvider>();

        /// <summary>
        /// Gets a <see cref="UrlProvider"/> for the given data center.
        /// </summary>
        /// <param name="dataCenter">
        /// An identifier for the data center.
        /// </param>
        /// <returns>
        /// A <see cref="UrlProvider"/> for the given data center.
        /// </returns>
        public static UrlProvider GetProvider(string dataCenter)
        {
            var key = (dataCenter ?? Configuration.DefaultDataCenter ?? string.Empty).ToLowerInvariant();

            if (key == "us")
            {
                key = string.Empty;
            }

            if (!ProviderCache.ContainsKey(key))
            {
                var urlProvider = new UrlProvider(key);
                ProviderCache.Add(key, urlProvider);
            }

            return ProviderCache[key];
        }
    }
}
