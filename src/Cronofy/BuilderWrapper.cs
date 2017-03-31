namespace Cronofy
{
    /// <summary>
    /// A class to turn an instance into an builder of its type.
    /// </summary>
    internal static class BuilderWrapper
    {
        /// <summary>
        /// Creates a wrapper the specified instance.
        /// </summary>
        /// <returns>A builder for this instance.</returns>
        /// <param name="instance">The instance to wrap.</param>
        /// <typeparam name="T">
        /// The type of the instance to create a builder for.
        /// </typeparam>
        internal static IBuilder<T> For<T>(T instance)
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
            /// <see cref="T:Cronofy.BuilderWrapper.Wrapper`1"/> class.
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
