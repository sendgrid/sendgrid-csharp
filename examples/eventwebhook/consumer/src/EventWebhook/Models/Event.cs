using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using EventWebhook.Converters;

namespace EventWebhook.Models
{
    public class Event
    {
        public string Email { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("smtp-id")]
        public string SmtpId { get; set; }

        [JsonPropertyName("event")]
        [JsonConverter(typeof(JsonStringEnumMemberConverter))]
        public EventType EventType { get; set; }

        [JsonConverter(typeof(CategoryConverter))]
        public Category Category { get; set; }

        [JsonPropertyName("sg_event_id")]
        public string SendGridEventId { get; set; }

        [JsonPropertyName("sg_message_id")]
        public string SendGridMessageId { get; set; }

        public string TLS { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> UniqueArgs { get; set; }

        [JsonPropertyName("marketing_campaign_id")]
        public string MarketingCampainId { get; set; }

        [JsonPropertyName("marketing_campaign_name")]
        public string MarketingCampainName { get; set; }

    }
}
