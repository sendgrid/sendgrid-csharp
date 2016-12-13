using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    public class TrackingSettings
    {
        [JsonProperty(PropertyName = "click_tracking")]
        public ClickTracking ClickTracking { get; set; }

        [JsonProperty(PropertyName = "open_tracking")]
        public OpenTracking OpenTracking { get; set; }

        [JsonProperty(PropertyName = "subscription_tracking")]
        public SubscriptionTracking SubscriptionTracking { get; set; }

        [JsonProperty(PropertyName = "ganalytics")]
        public Ganalytics Ganalytics { get; set; }
    }
}