using System;
using System.Text.Json.Serialization;
using EventWebhook.Converters;

namespace EventWebhook.Models
{
    public class ClickEvent : OpenEvent
    {
        [JsonConverter(typeof(UriConverter))]
        public Uri Url { get; set; }

        public ClickEvent()
        {
            EventType = EventType.Click;
        }
    }
}
