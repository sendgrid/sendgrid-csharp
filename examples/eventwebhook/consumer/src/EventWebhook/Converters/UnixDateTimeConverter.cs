using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EventWebhook.Converters
{
    public class UnixDateTimeConverter : JsonConverter<DateTime>
    {
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String && long.TryParse(reader.GetString(),out var seconds))
            {
                return DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime;
            }
            return DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64()).UtcDateTime;
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var seconds = (long)(value.ToUniversalTime() - UnixEpoch).TotalSeconds;
            writer.WriteNumberValue(seconds);
        }
    }
}