using System.Text.Json.Serialization;
using EventWebhook.Converters;

namespace EventWebhook.Models
{
    public class DeferredEvent : DeliveredEvent
    {
        [JsonConverter(typeof(ValueTypeStringConverter))]
        public int Attempt { get; set; }

        public DeferredEvent()
        {
            EventType = EventType.Deferred;
        }
    }
}
