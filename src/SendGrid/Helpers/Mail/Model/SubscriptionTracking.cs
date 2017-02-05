using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    public class SubscriptionTracking
    {
        [JsonProperty(PropertyName = "enable")]
        public bool Enable { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "html")]
        public string Html { get; set; }

        [JsonProperty(PropertyName = "substitution_tag")]
        public string SubstitutionTag { get; set; }
    }
}