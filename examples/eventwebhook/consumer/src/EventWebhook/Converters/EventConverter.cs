using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using System.Runtime.Serialization;
using EventWebhook.Models;

namespace EventWebhook.Converters
{
    public class EventConverter : JsonConverter<IEnumerable<Event>>
    {
        private static readonly IDictionary<string, string> EnumNameFor = new Dictionary<string, string>();

        static EventConverter()
        {
            var enumType = typeof(EventType);
            foreach (var name in enumType.GetEnumNames())
            {
                var field = enumType.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
                var enumMemberAttribute = field?.GetCustomAttribute<EnumMemberAttribute>(true);
                if (enumMemberAttribute == null) continue;
                EnumNameFor[enumMemberAttribute.Value] = name;
            }
        }
        public override IEnumerable<Event> Read(ref Utf8JsonReader reader, Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException($"Unrecognized token: {reader.TokenType}");
            }

            var elementType = typeToConvert.IsArray
                ? typeToConvert.GetElementType()
                : typeToConvert.GenericTypeArguments.FirstOrDefault();
            if (elementType == null)
            {
                throw new JsonException($"Impossible to read JSON array to fill type: {typeToConvert.Name}");
            }

            var list = typeToConvert.IsArray || typeToConvert.IsAbstract
                ? (IList) Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType))
                : (IList) Activator.CreateInstance(typeToConvert);
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                list.Add(ReadObject(ref reader, elementType, options));
            }

            if (!typeToConvert.IsArray)
            {
                return (IEnumerable<Event>) list;
            }

            var array = Array.CreateInstance(elementType, list.Count);
            list.CopyTo(array, 0);
            return (Event[]) array;
        }

        private static Event ReadObject(ref Utf8JsonReader reader, Type elementType, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            if (!doc.RootElement.TryGetProperty("event", out var eventProperty))
            {
                throw new JsonException("event property not found");
            }

            var eventTypeString = eventProperty.GetString();
            if (EnumNameFor.TryGetValue(eventTypeString, out var enumName))
            {
                eventTypeString = enumName;
            }

            if (!Enum.TryParse<EventType>(eventTypeString, true, out var eventType))
            {
                throw new JsonException($"event type not found: [{eventTypeString}]");
            }

            var typeName = string.Join(".", typeof(Event).Namespace, eventType + "Event");
            var type = Type.GetType(typeName);
            if (type == null)
            {
                throw new JsonException($"event type not found: [{typeName}]");
            }

            using var utf8Json = new MemoryStream();
            using (var utf8JsonWriter = new Utf8JsonWriter(utf8Json))
            {
                doc.RootElement.WriteTo(utf8JsonWriter);
            }

            utf8Json.Seek(0, SeekOrigin.Begin);
            return (Event) JsonSerializer.Deserialize(utf8Json.ToArray(), type, options);
        }

        public override void Write(Utf8JsonWriter writer, IEnumerable<Event> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}