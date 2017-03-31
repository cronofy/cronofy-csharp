namespace Cronofy
{
    /// <summary>
    /// A class to wrap a pre-built implementation.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the builder.
    /// </typeparam>
    public sealed class BuilderWrapper<T> : IBuilder<T>
    {
        /// <summary>
        /// The instance to wrap.
        /// </summary>
        private T instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Cronofy.BuilderWrapper`1"/> class.
        /// </summary>
        /// <param name="instance">The instance to wrap.</param>
        public BuilderWrapper(T instance)
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
