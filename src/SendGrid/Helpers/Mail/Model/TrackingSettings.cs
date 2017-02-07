using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// 
    /// </summary>
    public class TrackingSettings
    {
        /// <summary>
        /// Settings to determine how you would like to track the metrics of how your recipients interact with your email.
        /// </summary>
        [JsonProperty(PropertyName = "click_tracking")]
        public ClickTracking ClickTracking { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "open_tracking")]
        public OpenTracking OpenTracking { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "subscription_tracking")]
        public SubscriptionTracking SubscriptionTracking { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "ganalytics")]
        public Ganalytics Ganalytics { get; set; }
    }
}