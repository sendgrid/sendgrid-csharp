using Newtonsoft.Json;
using System;

namespace EventWebhook.Converters
{
    public class UriConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(string);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                return null;
            }

            if (reader.TokenType == JsonToken.String)
            {
                return new Uri((string)reader.Value);
            }

            throw new InvalidOperationException("Invalid Url");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (null == value)
            {
                writer.WriteNull();
                return;
            }

            if (value is Uri)
            {
                writer.WriteValue(((Uri)value).OriginalString);
                return;
            }
        }
    }
}
