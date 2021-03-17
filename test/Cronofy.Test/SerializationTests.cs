namespace Cronofy.Test
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using NUnit.Framework;

    [TestFixture]
    public sealed class SerializationTests
    {
        private sealed class Wrapper
        {
            public EventTime EventTime { get; set; }
        }

        [Test]
        public void CanSerializeEventTime()
        {
            var eventTime = new EventTime(new Date(2018, 3, 17), "Etc/UTC");
            var wrapper = new Wrapper { EventTime = eventTime };

            var serializerSettings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new EventTimeConverter() },
            };

            var serialized = JsonConvert.SerializeObject(wrapper, serializerSettings);

            var deserialized = JsonConvert.DeserializeObject<Wrapper>(serialized, serializerSettings);

            Assert.AreEqual(eventTime, deserialized.EventTime);
        }
    }
}
