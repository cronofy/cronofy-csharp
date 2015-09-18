using System;
using System.Globalization;

namespace Cronofy
{
	public struct Date
	{
		private const string DateFormat = "yyyy-MM-dd";

		private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		private readonly int ticks;

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
					year, month, day
				);

				throw new ArgumentOutOfRangeException(message, ex);
			}
		}

		internal static Date From(DateTime value)
		{
			return new Date(value.Year, value.Month, value.Day);
		}

		internal static Date From(DateTimeOffset value)
		{
			return new Date(value.Year, value.Month, value.Day);
		}

		internal static Date Parse(string input)
		{
			Date result;

			if (TryParse(input, out result) == false)
			{
				var message = string.Format(
					"{0} was not recognized as a valid Date",
					input
				);

				throw new FormatException(message);
			}

			return result;
		}

		internal static bool TryParse(string input, out Date value)
		{
			DateTime dateTime;

			var success = DateTime.TryParseExact(input, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out dateTime);

			if (success)
			{
				value = From(dateTime);
			}
			else
			{
				value = new Date();
			}

			return success;
		}

		public int Year
		{
			get {
				return this.DateTime.Year;
			}
		}

		public int Month
		{
			get {
				return this.DateTime.Month;
			}
		}

		public int Day
		{
			get {
				return this.DateTime.Day;
			}
		}

		public DateTime DateTime
		{
			get {
				return Epoch.AddDays(ticks);
			}
		}

		public override int GetHashCode()
		{
			return this.ticks.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj is Date)
			{
				return Equals((Date)obj);
			}

			return false;
		}

		public bool Equals(Date other)
		{
			return this.ticks == other.ticks;
		}

		public static bool operator ==(Date left, Date right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Date left, Date right)
		{
			return left.Equals(right) == false;
		}

		public override string ToString()
		{
			return this.DateTime.ToString(DateFormat);
		}
	}
}
