namespace Cronofy.Test.ExtremeTimesTests
{
    using System;
    using Newtonsoft.Json;

    internal sealed class TimeHolder
    {
        [JsonProperty("time")]
        [JsonConverter(typeof(TimestampConverter))]
        public DateTime Time { get; set; }
    }
}
