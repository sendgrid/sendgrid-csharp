using EventWebhook.Models;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace EventWebhook.Converters
{
    public class CategoryConverter : JsonConverter
    {
        private readonly Type[] _types;

        public CategoryConverter()
        {
            _types = new Type[] { typeof(string), typeof(string[]) };
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }

        public override bool CanWrite => true;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartArray)
            {
                return new Category(serializer.Deserialize<string[]>(reader), JsonToken.StartArray);
            }
            else
            {
                return new Category(new[] { serializer.Deserialize<string>(reader) }, reader.TokenType);
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is Category category)
            {
                if (category.IsArray)
                {
                    serializer.Serialize(writer, category);
                } else
                {
                    serializer.Serialize(writer, category.Value[0]);
                }
            }
        }
    }
}
