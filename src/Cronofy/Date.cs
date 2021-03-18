namespace Cronofy
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Struct representing a date in time.
    /// </summary>
    public struct Date
    {
        /// <summary>
        /// The format to parse and output dates with.
        /// </summary>
        private const string DateFormat = "yyyy-MM-dd";

        /// <summary>
        /// The date which all other dates are relative to.
        /// </summary>
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// The number of ticks this date is from <see cref="Epoch"/>.
        /// </summary>
        private readonly int ticks;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cronofy.Date"/> struct.
        /// </summary>
        /// <param name="year">
        /// The year of the date.
        /// </param>
        /// <param name="month">
        /// The month of the date.
        /// </param>
        /// <param name="day">
        /// The day of the date.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if the combination of <paramref name="year"/>,
        /// <paramref name="month"/>, and <paramref name="day"/> do not combine
        /// to represent a valid date.
        /// </exception>
        public Date(int year, int month, int day)
        {
            try
            {
                var dateTime = new DateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
                this.ticks = (int)(dateTime - Epoch).TotalDays;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                var message = string.Format(
                    "year={0}, month={1}, day={2} describe an unrepresentable Date",
                    year,
                    month,
                    day);

                throw new ArgumentOutOfRangeException(message, ex);
            }
        }

        /// <summary>
        /// Gets the year of the date.
        /// </summary>
        /// <value>
        /// The year of the date.
        /// </value>
        public int Year
        {
            get { return this.DateTime.Year; }
        }

        /// <summary>
        /// Gets the month of the date.
        /// </summary>
        /// <value>
        /// The month of the date.
        /// </value>
        public int Month
        {
            get { return this.DateTime.Month; }
        }

        /// <summary>
        /// Gets the day of the date.
        /// </summary>
        /// <value>
        /// The day of the date.
        /// </value>
        public int Day
        {
            get { return this.DateTime.Day; }
        }

        /// <summary>
        /// Gets the date represented as an instance of <see cref="DateTime"/>.
        /// </summary>
        /// <value>
        /// The date represented as an instance of <see cref="DateTime"/>.
        /// </value>
        public DateTime DateTime
        {
            get { return Epoch.AddDays(this.ticks); }
        }

        /// <summary>
        /// Compares two <see cref="Date"/>s for equality.
        /// </summary>
        /// <param name="left">
        /// The first value to compare.
        /// </param>
        /// <param name="right">
        /// The second value to compare.
        /// </param>
        /// <returns>
        /// <c>true</c> if both <see cref="Date"/>s are equal; otherwise,
        /// <c>false</c>.
        /// </returns>
        public static bool operator ==(Date left, Date right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two <see cref="Date"/>s for inequality.
        /// </summary>
        /// <param name="left">
        /// The first value to compare.
        /// </param>
        /// <param name="right">
        /// The second value to compare.
        /// </param>
        /// <returns>
        /// <c>false</c> if both <see cref="Date"/>s are equal; otherwise,
        /// <c>true</c>.
        /// </returns>
        public static bool operator !=(Date left, Date right)
        {
            return left.Equals(right) == false;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.ticks.GetHashCode();
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is Date)
            {
                return this.Equals((Date)obj);
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified <see cref="Cronofy.Date"/> is equal
        /// to the current <see cref="Cronofy.Date"/>.
        /// </summary>
        /// <param name="other">
        /// The <see cref="Cronofy.Date"/> to compare with the current
        /// <see cref="Cronofy.Date"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="Cronofy.Date"/> is equal to
        /// the current <see cref="Cronofy.Date"/>; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(Date other)
        {
            return this.ticks == other.ticks;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return this.DateTime.ToString(DateFormat);
        }

        /// <summary>
        /// Creates an equivalent <see cref="Date"/> for the given
        /// <see cref="DateTime"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="DateTime"/> to create an equivalent
        /// <see cref="Date"/> for.
        /// </param>
        /// <returns>
        /// A <see cref="Date"/> that is equivalent to the provided
        /// <see cref="DateTime"/>.
        /// </returns>
        internal static Date From(DateTime value)
        {
            return new Date(value.Year, value.Month, value.Day);
        }

        /// <summary>
        /// Creates an equivalent <see cref="Date"/> for the given
        /// <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="DateTimeOffset"/> to create an equivalent
        /// <see cref="Date"/> for.
        /// </param>
        /// <returns>
        /// A <see cref="Date"/> that is equivalent to the provided
        /// <see cref="DateTimeOffset"/>.
        /// </returns>
        internal static Date From(DateTimeOffset value)
        {
            return new Date(value.Year, value.Month, value.Day);
        }

        /// <summary>
        /// Creates an equivalent <see cref="Date"/> for the given
        /// <see cref="string"/>.
        /// </summary>
        /// <param name="input">
        /// The value to convert into a <see cref="Date"/>.
        /// </param>
        /// <returns>
        /// A <see cref="Date"/> that is equivalent to the provided
        /// <see cref="DateTimeOffset"/>.
        /// </returns>
        /// <remarks>
        /// <see cref="DateFormat"/> is the expected format of the input.
        /// </remarks>
        /// <exception cref="FormatException">
        /// Thrown if <paramref name="input"/> cannot be converted into a
        /// <see cref="Date"/>.
        /// </exception>
        internal static Date Parse(string input)
        {
            Date result;

            if (TryParse(input, out result) == false)
            {
                var message = string.Format(
                    "{0} was not recognized as a valid Date",
                    input);

                throw new FormatException(message);
            }

            return result;
        }

        /// <summary>
        /// Tries to parse the given <see cref="string"/> into a
        /// <see cref="Date"/>.
        /// </summary>
        /// <param name="input">
        /// The value to attempt to parse.
        /// </param>
        /// <param name="value">
        /// The value of date when it has been parsed successfully.
        /// </param>
        /// <returns>
        /// <c>true</c>, if <paramref name="input"/> was parsed successfully;
        /// otherwise <c>false</c>.
        /// </returns>
        internal static bool TryParse(string input, out Date value)
        {
            var success = DateTime.TryParseExact(input, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out DateTime dateTime);

            if (success)
            {
                value = From(dateTime);
            }
            else
            {
                value = default;
            }

            return success;
        }
    }
}
