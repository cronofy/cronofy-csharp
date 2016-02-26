using System;
using Newtonsoft.Json;

namespace Cronofy.Test.ExtremeTimesTests
{
    internal sealed class TimeHolder
    {
        [JsonProperty("time")]
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
    }
}
