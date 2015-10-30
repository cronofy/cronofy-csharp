namespace Cronofy
{
    /// <summary>
    /// Exception thrown by the Cronofy SDK when an error is encountered in a
    /// HTTP response.
    /// </summary>
    public sealed class CronofyResponseException : CronofyException
    {
        /// <summary>
        /// The response that caused the exception.
        /// </summary>
        private readonly HttpResponse response;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Cronofy.CronofyResponseException"/> class.
        /// </summary>
        /// <param name="message">
        /// A message that describes the error.
        /// </param>
        /// <param name="response">
        /// The response that caused the exception, must not be null.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="response"/> is null.
        /// </exception>
        public CronofyResponseException(string message, HttpResponse response)
            : base(message)
        {
            Preconditions.NotNull("response", response);

            this.response = response;
        }

        /// <summary>
        /// Gets the response that caused the exception.
        /// </summary>
        /// <value>
        /// The response that caused the exception.
        /// </value>
        public HttpResponse Response
        {
            get { return this.response; }
        }
    }
}
