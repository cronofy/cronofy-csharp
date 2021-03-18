namespace Cronofy.Test.EventTimeTests
{
    using Newtonsoft.Json;

    internal sealed class EventTimeHolder
    {
        [JsonProperty("event_time")]
        [JsonConverter(typeof(EventTimeConverter))]
        public EventTime EventTime { get; set; }
    }
}
