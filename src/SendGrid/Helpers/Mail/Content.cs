using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    public class Content
    {
        public Content()
        {
        }

        public Content(string type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}