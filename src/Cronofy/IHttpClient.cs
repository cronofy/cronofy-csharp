namespace Cronofy
{
    /// <summary>
    /// Interface for a HTTP client.
    /// </summary>
    internal interface IHttpClient
    {
        /// <summary>
        /// Gets the response to the given request.
        /// </summary>
        /// <param name="request">
        /// The request to make, must not be <c>null</c>.
        /// </param>
        /// <returns>
        /// The response to the request.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="request"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// TODO Document request-based exceptions.
        /// </remarks>
        HttpResponse GetResponse(HttpRequest request);
    }
}
