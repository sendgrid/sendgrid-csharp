using Newtonsoft.Json;

namespace SendGrid.Model
{
    public class Metrics
    {
        [JsonProperty("blocks")]
        public long Blocks { get; set; }

        [JsonProperty("bounce_drops")]
        public long BounceDrops { get; set; }

        [JsonProperty("bounces")]
        public long Bounces { get; set; }

        [JsonProperty("clicks")]
        public long Clicks { get; set; }

        [JsonProperty("deferred")]
        public long Deferred { get; set; }

        [JsonProperty("delivered")]
        public long Delivered { get; set; }

        [JsonProperty("invalid_emails")]
        public long InvalidEmails { get; set; }

        [JsonProperty("opens")]
        public long Opens { get; set; }

        [JsonProperty("processed")]
        public long Processed { get; set; }

        [JsonProperty("requests")]
        public long Requests { get; set; }

        [JsonProperty("spam_report_drops")]
        public long SpamReportDrops { get; set; }

        [JsonProperty("spam_reports")]
        public long SpamReports { get; set; }

        [JsonProperty("unique_clicks")]
        public long UniqueClicks { get; set; }

        [JsonProperty("unique_opens")]
        public long UniqueOpens { get; set; }

        [JsonProperty("unsubscribe_drops")]
        public long UnsubscribeDrops { get; set; }

        [JsonProperty("unsubscribes")]
        public long Unsubscribes { get; set; }
    }
}
