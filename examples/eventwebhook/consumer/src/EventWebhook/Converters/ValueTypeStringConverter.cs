using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EventWebhook.Converters
{
    public class ValueTypeStringConverter : JsonConverter<object>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsValueType;
        }

        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (typeToConvert == typeof(decimal))
            {
                if (reader.TokenType == JsonTokenType.Number && reader.TryGetDecimal(out var value))
                {
                    return value;
                }
                if (decimal.TryParse(reader.GetString(), NumberStyles.Currency, CultureInfo.InvariantCulture,
                    out var @decimal))
                {
                    return @decimal;
                }
            }
            if (typeToConvert == typeof(float))
            {
                if (reader.TokenType == JsonTokenType.Number && reader.TryGetSingle(out var value))
                {
                    return value;
                }
                if (float.TryParse(reader.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture,
                    out var @float))
                {
                    return @float;
                }
            }
            if (typeToConvert == typeof(double))
            {
                if (reader.TokenType == JsonTokenType.Number && reader.TryGetDouble(out var value))
                {
                    return value;
                }
                if (double.TryParse(reader.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture,
                    out var @double))
                {
                    return @double;
                }
            }
            if (typeToConvert == typeof(int))
            {
                if (reader.TokenType == JsonTokenType.Number && reader.TryGetInt32(out var value))
                {
                    return value;
                }
                if (int.TryParse(reader.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture, out var integer))
                {
                    return integer;
                }
            }

            if (typeToConvert == typeof(bool))
            {
                if (reader.TokenType == JsonTokenType.False || reader.TokenType == JsonTokenType.False)
                {
                    return reader.GetBoolean();
                }
                if (bool.TryParse(reader.GetString(), out var @bool))
                {
                    return @bool;
                }
            }
            return TypeDescriptor.GetConverter(typeToConvert).ConvertFromInvariantString(reader.GetString());
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case decimal decimalValue:
                    writer.WriteStringValue(decimalValue.ToString(CultureInfo.InvariantCulture));
                    break;
                case float floatValue:
                    writer.WriteStringValue(floatValue.ToString(CultureInfo.InvariantCulture));
                    break;
                case double doubleValue:
                    writer.WriteStringValue(doubleValue.ToString(CultureInfo.InvariantCulture));
                    break;
                case bool boolValue:
                    writer.WriteStringValue(boolValue.ToString().ToLowerInvariant());
                    break;
                default:
                    writer.WriteStringValue(value.ToString());
                    break;
            }
        }
    }
}