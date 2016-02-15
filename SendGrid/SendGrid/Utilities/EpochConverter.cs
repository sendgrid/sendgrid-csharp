using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SendGrid.Utilities
{
    public class EpochConverter : DateTimeConverterBase
    {
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null) return;

            var secondsSinceEpoch = ((DateTime)value).ToUnixTime();
            serializer.Serialize(writer, secondsSinceEpoch);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;
            return _epoch.AddSeconds((long)reader.Value);
        }
    }
}