using EventWebhook.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EventWebhook.Parser
{
    public class EventParser
    {
        public static async Task<IEnumerable<Event>> ParseAsync(string json)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return await ParseAsync(stream);
            }
        }

        public static async Task<IEnumerable<Event>> ParseAsync(Stream stream)
        {
            var reader = new StreamReader(stream);

            var json = await reader.ReadToEndAsync();

            return JsonConvert.DeserializeObject<IEnumerable<Event>>(json, new EventConverter());
        }

        public static IEnumerable<Event> Parse(string json)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                return Parse(stream);
            }
        }

        public static IEnumerable<Event> Parse(Stream stream)
        {
            var reader = new StreamReader(stream);

            var json = reader.ReadToEnd();

            return JsonConvert.DeserializeObject<IEnumerable<Event>>(json, new EventConverter());
        }
    }
}
