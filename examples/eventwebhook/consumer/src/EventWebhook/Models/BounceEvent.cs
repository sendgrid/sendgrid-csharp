using System.Text.Json.Serialization;

namespace EventWebhook.Models
{
    public class BounceEvent : DroppedEvent
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BounceEventType BounceType { get; set; }

        public BounceEvent()
        {
            EventType = EventType.Bounce;
        }
    }
}
