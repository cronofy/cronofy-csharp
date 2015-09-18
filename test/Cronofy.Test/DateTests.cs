using System;
using NUnit.Framework;

namespace Cronofy.Test
{
	[TestFixture]
	public sealed class DateTests
	{
		[Test]
		public void InitializesAtEpoch()
		{
			var date = new Date();

			Assert.AreEqual(1970, date.Year);
			Assert.AreEqual(1, date.Month);
			Assert.AreEqual(1, date.Day);

			Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc), date.DateTime);
		}

		[Test]
		public void CanCreateArbitraryDate()
		{
			var date = new Date(1984, 3, 17);

			Assert.AreEqual(1984, date.Year);
			Assert.AreEqual(3, date.Month);
			Assert.AreEqual(17, date.Day);

			Assert.AreEqual(new DateTime(1984, 3, 17, 0, 0, 0, DateTimeKind.Utc), date.DateTime);
		}

		[Test]
		public void RejectInvalidDates()
		{
			var ex = Assert.Throws<ArgumentOutOfRangeException>(() => new Date(2015, 2, 29));

			Assert.AreEqual("year=2015, month=2, day=29 describe an unrepresentable Date", ex.Message);
		}

		[TestCase("1984-03-17", 1984, 3, 17)]
		[TestCase("2013-10-06", 2013, 10, 6)]
		public void ToStringFormat(string expected, int year, int month, int day)
		{
			var value = new Date(year, month, day);

			Assert.AreEqual(expected, value.ToString());
		}

		[TestCase("1984-03-17", 1984, 3, 17)]
		[TestCase("2013-10-06", 2013, 10, 6)]
		public void Parse(string input, int year, int month, int day)
		{
			var value = Date.Parse(input);

			Assert.AreEqual(new Date(year, month, day), value);
		}

		[TestCase("2015-02-29")]
		[TestCase("2013-13-06")]
		[TestCase("2013-1-1")]
		[TestCase("nonsense")]
		[TestCase("2015-01-01T01:01:01Z")]
		public void ParseRejects(string input)
		{
			var ex = Assert.Throws<FormatException>(() => Date.Parse(input));

			Assert.AreEqual(input + " was not recognized as a valid Date", ex.Message);
		}

		[TestCase("1984-03-17", true, 1984, 3, 17)]
		[TestCase("2013-10-06", true, 2013, 10, 6)]
		[TestCase("2015-02-29", false, 0, 0, 0)]
		[TestCase("2013-13-06", false, 0, 0, 0)]
		[TestCase("2013-1-1", false, 0, 0, 0)]
		[TestCase("nonsense", false, 0, 0, 0)]
		[TestCase("2015-01-01T01:01:01Z", false, 0, 0, 0)]
		public void TryParse(string input, bool expectedSuccess, int year, int month, int day)
		{
			Date value;
			var success = Date.TryParse(input, out value);

			Assert.AreEqual(expectedSuccess, success);

			if (expectedSuccess) {
				Assert.AreEqual(new Date(year, month, day), value);
			}
		}
	}
}
