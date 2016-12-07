using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    public class FooterSettings
    {
        [JsonProperty(PropertyName = "enable")]
        public bool Enable { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "html")]
        public string Html { get; set; }
    }
}