namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Set of utilities for working with enumerable collections.
    /// </summary>
    internal static class EnumerableUtils
    {
        /// <summary>
        /// Version of <see cref="Enumerable.SequenceEqual{T}(IEnumerable{T}, IEnumerable{T})"/> that
        /// tolerates either input being <c>null</c>.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the sequences.
        /// </typeparam>
        /// <param name="left">
        /// The first sequence to compare.
        /// </param>
        /// <param name="right">
        /// The second sequence to compare.
        /// </param>
        /// <returns>
        /// <c>true</c>, if both sequences are null or if they contain the same
        /// sequence of elements; otherwise <c>false</c>.
        /// </returns>
        public static bool NullTolerantSequenceEqual<T>(IEnumerable<T> left, IEnumerable<T> right)
        {
            if (left == null)
            {
                return right == null;
            }

            if (right == null)
            {
                return false;
            }

            return left.SequenceEqual(right);
        }
    }
}
