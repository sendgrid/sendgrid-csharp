using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    public class SpamCheck
    {
        [JsonProperty(PropertyName = "enable")]
        public bool? Enable { get; set; }

        [JsonProperty(PropertyName = "threshold")]
        public int? Threshold { get; set; }

        [JsonProperty(PropertyName = "post_to_url")]
        public string PostToUrl { get; set; }
    }
}