namespace Cronofy
{
    /// <summary>
    /// Generic builder interface.
    /// </summary>
    /// <typeparam name="T">
    /// The type the builder creates.
    /// </typeparam>
    public interface IBuilder<T>
    {
        /// <summary>
        /// Creates an instance of <typeparamref name="T"/> based upon the
        /// current state of the builder.
        /// </summary>
        /// <returns>
        /// An instance of <typeparamref name="T"/> based upon the current state
        /// of the builder.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// Raised if the current state of the builder cannot create a valid
        /// instance of <typeparamref name="T"/>.
        /// </exception>
        T Build();
    }
}
