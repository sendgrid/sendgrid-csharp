using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;

namespace SendGrid.Utilities
{
    public class SendGridDateTimeConverter : DateTimeConverterBase
    {
        private const string SENDGRID_DATETIME_FORMAT = "yyyy-MM-dd HH:mm:ss";

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null) return;
            writer.WriteValue(((DateTime)value).ToString(SENDGRID_DATETIME_FORMAT));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;
            var date = DateTime.ParseExact(reader.Value.ToString(), SENDGRID_DATETIME_FORMAT, new CultureInfo("en-US"), DateTimeStyles.AssumeUniversal);
            return date;
        }
    }
}