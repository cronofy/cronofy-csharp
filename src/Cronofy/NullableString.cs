namespace Cronofy
{
    /// <summary>
    /// Represents a string that differentiates between an explicit
    /// <code>null</code> and the omission of the value.
    /// </summary>
    public sealed class NullableString
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Cronofy.NullableString"/> class.
        /// </summary>
        /// <param name="value">
        /// The explicit value.
        /// </param>
        /// <remarks>
        /// Sets <see cref="HasValue"/> as <code>true</code>.
        /// </remarks>
        public NullableString(string value) : this()
        {
            this.Value = value;
            this.HasValue = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Cronofy.NullableString"/> class.
        /// </summary>
        /// <remarks>
        /// Sets <see cref="HasValue"/> as <code>true</code>.
        /// </remarks>
        public NullableString()
        {
            this.HasValue = false;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; private set; }

        /// <summary>
        /// Gets a value indicating whether there is an explicit value.
        /// </summary>
        /// <value>
        /// <code>true</code> if the value has been set explicitly, otherwise
        /// <code>false</code>.
        /// </value>
        public bool HasValue { get; private set; }
    }
}
