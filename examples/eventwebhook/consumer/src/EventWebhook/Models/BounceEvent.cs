using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EventWebhook.Models
{
    public class BounceEvent : DroppedEvent
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public BounceEventType BounceType { get; set; }
    }
}
