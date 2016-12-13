namespace Cronofy
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Convertor for translating <see cref="NullableString"/>s to JSON.
    /// </summary>
    internal sealed class NullableStringConverter : JsonConverter
    {
        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var nullableString = value as NullableString;

            if (nullableString == null)
            {
                return;
            }

            if (nullableString.HasValue == false)
            {
                return;
            }

            writer.WriteValue(nullableString.Value);
        }
    }
}
