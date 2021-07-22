using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using EventWebhook.Models;

namespace EventWebhook.Converters
{
    public class CategoryConverter : JsonConverter<Category>
    {
        public override Category Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType == JsonTokenType.StartArray
                ? new Category(JsonSerializer.Deserialize<string[]>(ref reader), true)
                : new Category(new []{reader.GetString()},false);
        }

        public override void Write(Utf8JsonWriter writer, Category value, JsonSerializerOptions options)
        {
            if (value.IsArray)
            {
                writer.WriteStartArray();
                foreach (var item in value.Value)
                {
                    writer.WriteStringValue(item);
                }
                writer.WriteEndArray();
            }
            else
            {
                writer.WriteStringValue(value.Value.FirstOrDefault());
            }
        }
    }
}
