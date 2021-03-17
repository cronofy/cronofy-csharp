namespace Cronofy
{
    /// <summary>
    /// A class to turn an instance into an builder of its type.
    /// </summary>
    internal static class Builder
    {
        /// <summary>
        /// Creates a builder wrapping the specified instance.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the instance to create a builder for.
        /// </typeparam>
        /// <param name="instance">
        /// The instance to wrap.
        /// </param>
        /// <returns>
        /// A builder for this instance.
        /// </returns>
        internal static IBuilder<T> Wrap<T>(T instance)
        {
            return new Wrapper<T>(instance);
        }

        /// <summary>
        /// Wraps an instance in a builder of its type.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the builder.
        /// </typeparam>
        private sealed class Wrapper<T> : IBuilder<T>
        {
            /// <summary>
            /// The wrapped instance.
            /// </summary>
            private T instance;

            /// <summary>
            /// Initializes a new instance of the
            /// <see cref="Wrapper{T}"/> class.
            /// </summary>
            /// <param name="instance">
            /// The instance to wrap.
            /// </param>
            public Wrapper(T instance)
            {
                this.instance = instance;
            }

            /// <inheritdoc/>
            public T Build()
            {
                return this.instance;
            }
        }
    }
}
