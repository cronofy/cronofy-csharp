namespace Cronofy
{
    using System;

    /// <summary>
    /// Exception thrown by the Cronofy SDK when an error is encountered.
    /// </summary>
    public sealed class CronofyException : ApplicationException
    {
        /// <inheritdoc/>
        public CronofyException(string message)
            : base(message)
        {
        }

        /// <inheritdoc/>
        public CronofyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
