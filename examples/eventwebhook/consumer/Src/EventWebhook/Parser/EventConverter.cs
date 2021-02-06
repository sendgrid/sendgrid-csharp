using EventWebhook.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace EventWebhook.Parser
{
    public class EventConverter : JsonConverter
    {
        private static readonly Dictionary<EventType, Func<string, Event>> eventConverters =
            new Dictionary<EventType, Func<string, Event>>()
            {
                { EventType.Bounce, (json) => JsonConvert.DeserializeObject<BounceEvent>(json) },
                { EventType.Click, (json) => JsonConvert.DeserializeObject<ClickEvent>(json) },
                { EventType.Deferred, (json) => JsonConvert.DeserializeObject<DeferredEvent>(json) },
                { EventType.Delivered, (json) => JsonConvert.DeserializeObject<DeliveredEvent>(json) },
                { EventType.Dropped, (json) => JsonConvert.DeserializeObject<DroppedEvent>(json) },
                { EventType.GroupResubscribe, (json) => JsonConvert.DeserializeObject<GroupResubscribeEvent>(json) },
                { EventType.GroupUnsubscribe, (json) => JsonConvert.DeserializeObject<GroupUnsubscribeEvent>(json) },
                { EventType.Open, (json) => JsonConvert.DeserializeObject<OpenEvent>(json) },
                { EventType.Processed, (json) => JsonConvert.DeserializeObject<ProcessedEvent>(json) },
                { EventType.SpamReport, (json) => JsonConvert.DeserializeObject<SpamReportEvent>(json) },
                { EventType.Unsubscribe, (json) => JsonConvert.DeserializeObject<UnsubscribeEvent>(json) },
            };

        private static Event DeserializeEvent(EventType type, string json)
        {
            if (!eventConverters.ContainsKey(type))
            {
                throw new ArgumentOutOfRangeException($"Unknown event type: {type.ToString()}");
            }

            return eventConverters.GetValueOrDefault(type)(json);
        }

        public override bool CanConvert(Type objectType) => typeof(Event) == objectType;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            jsonObject.TryGetValue("event", StringComparison.OrdinalIgnoreCase, out JToken eventTypeJsonProperty);

            var eventType = (EventType)eventTypeJsonProperty.ToObject(typeof(EventType));

            var webhookEvent = DeserializeEvent(eventType, jsonObject.ToString());

            return webhookEvent;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
