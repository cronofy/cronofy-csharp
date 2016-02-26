using System;
using NUnit.Framework;
using Newtonsoft.Json;

namespace Cronofy.Test.ExtremeTimesTests
{
    [TestFixture]
    public sealed class JsonParsing
    {
        [Test]
        public void CanParseTimeStringUtc()
        {
            const string json = @"{ ""time"": ""2015-09-19T12:30:45Z"" }";

            var result = JsonConvert.DeserializeObject<TimeHolder>(json, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });

            var expected = new DateTime(2015, 9, 19, 12, 30, 45, DateTimeKind.Utc);

            Assert.AreEqual(expected, result.Time);
        }

        [Test]
        public void CanParseTimeStringDst()
        {
            const string json = @"{ ""time"": ""2015-05-19T12:30:45Z"" }";

            var result = JsonConvert.DeserializeObject<TimeHolder>(json, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });

            var expected = new DateTime(2015, 5, 19, 12, 30, 45, DateTimeKind.Utc);

            Assert.AreEqual(expected, result.Time);
        }

        [Test]
        public void CanParseLowTimeString()
        {
            const string json = @"{ ""time"": ""0000-12-29T00:00:00Z"" }";

            var result = JsonConvert.DeserializeObject<TimeHolder>(json, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None });

            var expected = DateTime.MinValue.ToUniversalTime();

            Assert.AreEqual(expected, result.Time);
        }

        [Test]
        public void RejectsNonTimeString()
        {
            const string json = @"{ ""time"": ""hello"" }";

            Assert.Throws<JsonSerializationException>(() =>
                JsonConvert.DeserializeObject<TimeHolder>(json, new JsonSerializerSettings { DateParseHandling = DateParseHandling.None }
            ));
        }
    }
}
