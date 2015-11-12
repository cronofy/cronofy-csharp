namespace Cronofy
{
    using System;
    using Cronofy.Requests;

    /// <summary>
    /// Builder for generating <see cref="CreateChannelRequest"/>s.
    /// </summary>
    public sealed class CreateChannelBuilder : IBuilder<CreateChannelRequest>
    {
        /// <summary>
        /// The callback URL for the request.
        /// </summary>
        private string callbackUrl;

        /// <summary>
        /// The request's only managed flag.
        /// </summary>
        private bool? onlyManaged;

        /// <summary>
        /// Sets the callback URL for the request.
        /// </summary>
        /// <param name="callbackUrl">
        /// The callback URL for the request, must not be empty.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="callbackUrl"/> is empty.
        /// </exception>
        public CreateChannelBuilder CallbackUrl(string callbackUrl)
        {
            Preconditions.NotEmpty("callbackUrl", callbackUrl);

            this.callbackUrl = callbackUrl;

            return this;
        }

        /// <summary>
        /// Sets the only managed flag for the request.
        /// </summary>
        /// <param name="onlyManaged">
        /// A flag specifying whether only events that are managed by the
        /// application should be returned.
        /// </param>
        /// <returns>
        /// A reference to the modified builder.
        /// </returns>
        public CreateChannelBuilder OnlyManaged(bool onlyManaged)
        {
            this.onlyManaged = onlyManaged;
            return this;
        }

        /// <inheritdoc/>
        public CreateChannelRequest Build()
        {
            return new CreateChannelRequest
            {
                CallbackUrl = this.callbackUrl,
                Filters = new CreateChannelRequest.ChannelFilters
                {
                    OnlyManaged = this.onlyManaged,
                },
            };
        }
    }
}
