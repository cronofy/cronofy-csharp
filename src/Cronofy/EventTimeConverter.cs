namespace Cronofy
{
    using System;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Convertor for translating <see cref="EventTime"/>s to and from JSON.
    /// </summary>
    public sealed class EventTimeConverter : JsonConverter
    {
        /// <summary>
        /// The date time offset formats using for parsing times.
        /// </summary>
        private static readonly string[] DateTimeOffsetFormats =
        {
            "yyyy-MM-ddTHH:mm:ssZ",
            "yyyy-MM-ddTHH:mm:sszzz",
        };

        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return typeof(EventTime).IsAssignableFrom(objectType);
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var value = (string)reader.Value;

                DateTimeOffset dtoResult;
                if (DateTimeOffset.TryParseExact(value, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dtoResult))
                {
                    return new EventTime(dtoResult, TimeZoneIdentifiers.UTC);
                }

                Date dateResult;
                if (Date.TryParse(value, out dateResult))
                {
                    return new EventTime(dateResult, TimeZoneIdentifiers.UTC);
                }

                throw new JsonSerializationException("Failed to parse " + value);
            }

            if (reader.TokenType == JsonToken.StartObject)
            {
                var jobject = JObject.Load(reader);

                var timeString = jobject.GetValue("time").Value<string>();
                var timeZoneId = jobject.GetValue("tzid").Value<string>();

                DateTimeOffset dtoResult;
                if (DateTimeOffset.TryParseExact(timeString, DateTimeOffsetFormats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out dtoResult))
                {
                    return new EventTime(dtoResult, timeZoneId);
                }

                Date dateResult;
                if (Date.TryParse(timeString, out dateResult))
                {
                    return new EventTime(dateResult, timeZoneId);
                }

                throw new JsonSerializationException("Failed to parse " + jobject);
            }

            throw new JsonSerializationException("Failed to parse " + reader.TokenType);
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var eventTime = value as EventTime;

            if (eventTime == null)
            {
                return;
            }

            writer.WriteStartObject();
            writer.WritePropertyName("time");

            if (eventTime.HasTime)
            {
                writer.WriteValue(eventTime.DateTimeOffset.ToString("u"));
            }
            else
            {
                writer.WriteValue(eventTime.Date.ToString());
            }

            writer.WritePropertyName("tzid");
            writer.WriteValue(eventTime.TimeZoneId);
            writer.WriteEndObject();
        }
    }
}
