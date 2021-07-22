using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using EventWebhook.Converters;
using EventWebhook.Models;

namespace EventWebhook.Parser
{
    public class EventParser
    {
        private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters =
            {
                new EventConverter()
            }
        };

        public static async Task<IEnumerable<Event>> ParseAsync(Stream stream, JsonSerializerOptions options = null)
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Event>>(stream, options ?? SerializerOptions);
        }

        public static IEnumerable<Event> Parse(string json, JsonSerializerOptions options = null)
        {
            return JsonSerializer.Deserialize<IEnumerable<Event>>(json, options ?? SerializerOptions);
        }

        public static IEnumerable<Event> Parse(Stream stream, JsonSerializerOptions options = null)
        {
            using var buffer = new MemoryStream();
            stream.CopyTo(buffer);
            return JsonSerializer.Deserialize<IEnumerable<Event>>(buffer.ToArray(), options ?? SerializerOptions);
        }
    }
}