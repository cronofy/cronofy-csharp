namespace Cronofy
{
    using Cronofy.Responses;

    /// <summary>
    /// Exception thrown by the Cronofy SDK when a batch request contains
    /// non-successful entries.
    /// </summary>
    public sealed class BatchWithErrorsException : CronofyException
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="BatchWithErrorsException"/> class.
        /// </summary>
        /// <param name="message">
        /// A message that describes the error.
        /// </param>
        /// <param name="response">
        /// The batch response that contains non-successful entries, must not be
        /// null.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if <paramref name="response"/> is null.
        /// </exception>
        public BatchWithErrorsException(string message, BatchResponse response)
            : base(message)
        {
            Preconditions.NotNull("batchResponse", response);

            this.Response = response;
        }

        /// <summary>
        /// Gets the batch response that contains non-successful entries.
        /// </summary>
        /// <value>
        /// The batch response.
        /// </value>
        public BatchResponse Response { get; private set; }
    }
}
