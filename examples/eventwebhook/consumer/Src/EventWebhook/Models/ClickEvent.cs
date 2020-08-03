using EventWebhook.Converters;
using Newtonsoft.Json;
using System;

namespace EventWebhook.Models
{
    public class ClickEvent : OpenEvent
    {
        [JsonConverter(typeof(UriConverter))]
        public Uri Url { get; set; }
    }
}
