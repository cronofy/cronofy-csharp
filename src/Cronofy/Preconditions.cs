namespace Cronofy
{
    using System;

    /// <summary>
    /// Collection of precondition methods for code contract enforcement.
    /// </summary>
    internal static class Preconditions
    {
        /// <summary>
        /// Ensures that <paramref name="value"/> is <c>true</c>.
        /// </summary>
        /// <param name="value">
        /// The value to check, must be true.
        /// </param>
        /// <param name="message">
        /// The message to use for the exception raised if
        /// <paramref name="value"/> is <c>false</c>.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="value"/> is <c>false</c>.
        /// </exception>
        public static void True(bool value, string message)
        {
            if (value == false)
            {
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// Ensures that <paramref name="value"/> is not blank.
        /// </summary>
        /// <param name="name">
        /// The name of the parameter being checked, used to provide a more
        /// meaningful exception message.
        /// </param>
        /// <param name="value">
        /// The value to check, must not be blank.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="value"/> is blank.
        /// </exception>
        /// <remarks>
        /// A <see cref="string"/> is considered blank if it is <c>null</c> or
        /// contains no non-whitespace characters.
        /// </remarks>
        public static void NotBlank(string name, string value)
        {
            if (string.IsNullOrEmpty(value) || value.Trim().Length == 0)
            {
                throw new ArgumentException(string.Format("{0} must not be blank", name));
            }
        }

        /// <summary>
        /// Ensures that <paramref name="value"/> is not empty.
        /// </summary>
        /// <param name="name">
        /// The name of the parameter being checked, used to provide a more
        /// meaningful exception message.
        /// </param>
        /// <param name="value">
        /// The value to check, must not be empty.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="value"/> is empty.
        /// </exception>
        /// <remarks>
        /// An array is considered empty if it is <c>null</c> or contains no
        /// values.
        /// </remarks>
        public static void NotEmpty(string name, string[] value)
        {
            if (value == null || value.Length == 0)
            {
                throw new ArgumentException(string.Format("{0} must not be empty", name));
            }
        }

        /// <summary>
        /// Ensures that <paramref name="value"/> is not empty.
        /// </summary>
        /// <param name="name">
        /// The name of the parameter being checked, used to provide a more
        /// meaningful exception message.
        /// </param>
        /// <param name="value">
        /// The value to check, must not be empty.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="value"/> is empty.
        /// </exception>
        /// <remarks>
        /// A <see cref="string"/> is considered empty if it is <c>null</c> or
        /// contains characters.
        /// </remarks>
        public static void NotEmpty(string name, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(string.Format("{0} must not be null or empty", name));
            }
        }

        /// <summary>
        /// Ensures that <paramref name="value"/> is not <c>null</c>.
        /// </summary>
        /// <param name="name">
        /// The name of the parameter being checked, used to provide a more
        /// meaningful exception message.
        /// </param>
        /// <param name="value">
        /// The value to check, must not be <c>null</c>.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="value"/> is <c>null</c>.
        /// </exception>
        public static void NotNull(string name, string value)
        {
            if (value == null)
            {
                throw new ArgumentException(string.Format("{0} must not be null", name));
            }
        }

        /// <summary>
        /// Ensures that <paramref name="value"/> is not <c>null</c>.
        /// </summary>
        /// <param name="name">
        /// The name of the parameter being checked, used to provide a more
        /// meaningful exception message.
        /// </param>
        /// <param name="value">
        /// The value to check, must not be <c>null</c>.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="value"/> is <c>null</c>.
        /// </exception>
        public static void NotNull(string name, object value)
        {
            if (value == null)
            {
                throw new ArgumentException(string.Format("{0} must not be null", name));
            }
        }

        /// <summary>
        /// Ensures that <paramref name="value"/> is not negative.
        /// </summary>
        /// <param name="name">
        /// The name of the parameter being checked, used to provide a more
        /// meaningful exception message.
        /// </param>
        /// <param name="value">
        /// The value to check, must not be negative.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown if <paramref name="value"/> is negative.
        /// </exception>
        public static void NotNegative(string name, int value)
        {
            if (value < 0)
            {
                throw new ArgumentException(string.Format("{0} must not be negative but was {1}", name, value));
            }
        }
    }
}
