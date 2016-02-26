namespace Cronofy
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;

    /// <summary>
    /// Convertor for translating <see cref="DateTime"/>s from JSON.
    /// </summary>
    internal sealed class TimestampConverter : JsonConverter
    {
        /// <summary>
        /// The minimum time that can be parsed, as a string.
        /// </summary>
        private static readonly string MinTime = DateTime.MinValue.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");

        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.String)
            {
                var value = (string)reader.Value;

                DateTime dtoResult;
                if (DateTime.TryParseExact(value, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out dtoResult))
                {
                    return dtoResult;
                }

                if (Regex.IsMatch(value, @"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z") == false)
                {
                    throw new JsonSerializationException("Failed to parse " + value);
                }

                if (string.Compare(value, MinTime, false, CultureInfo.InvariantCulture) < 0)
                {
                    return DateTime.MinValue.ToUniversalTime();
                }

                throw new JsonSerializationException("Failed to parse " + value);
            }

            throw new JsonSerializationException("Failed to parse " + reader.TokenType);
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
