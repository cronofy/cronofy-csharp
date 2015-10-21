using System;

namespace Cronofy
{
    internal static class Preconditions
    {
        public static void True(bool value, string message)
        {
            if (value == false)
            {
                throw new ArgumentException(message);
            }
        }

        public static void NotBlank(string name, string value)
        {
            if (string.IsNullOrEmpty(value) || value.Trim().Length == 0)
            {
                throw new ArgumentException(string.Format("{0} must not be blank", name));
            }
        }

        public static void NotEmpty(string name, string[] value)
        {
            if (value.Length == 0)
            {
                throw new ArgumentException(string.Format("{0} must not be empty", name));
            }
        }

        public static void NotEmpty(string name, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(string.Format("{0} must not be null or empty", name));
            }
        }

        public static void NotNull(string name, string value)
        {
            if (value == null)
            {
                throw new ArgumentException(string.Format("{0} must not be null", name));
            }
        }

        public static void NotNull(string name, object value)
        {
            if (value == null)
            {
                throw new ArgumentException(string.Format("{0} must not be null", name));
            }
        }
    }
}
