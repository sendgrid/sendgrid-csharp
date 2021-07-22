using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EventWebhook.Converters
{
    public class UriConverter : JsonConverter<Uri>
    {
        public override Uri Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return new Uri(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, Uri value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.OriginalString);
        }
    }
}
