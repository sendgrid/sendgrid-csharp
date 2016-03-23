using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SendGrid.Model
{
    public class Campaign
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("sender_id")]
        public long SenderId { get; set; }

        [JsonProperty("list_ids")]
        public long[] Lists { get; set; }

        [JsonProperty("segment_ids")]
        public long[] Segments { get; set; }

        [JsonProperty("categories")]
        public string[] Categories { get; set; }

        [JsonProperty("suppression_group_id")]
        public long? SuppressionGroupId { get; set; }

        [JsonProperty("custom_unsubscribe_url")]
        public string CustomUnsubscribeUrl { get; set; }

        [JsonProperty("ip_pool")]
        public string IpPool { get; set; }

        [JsonProperty("html_content")]
        public string HtmlContent { get; set; }

        [JsonProperty("plain_content")]
        public string TextContent { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public CampaignStatus Status { get; set; }
    }
}
