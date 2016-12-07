using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    public class ClickTracking
    {
        [JsonProperty(PropertyName = "enable")]
        public bool? Enable { get; set; }

        [JsonProperty(PropertyName = "enable_text")]
        public bool? EnableText { get; set; }
    }
}