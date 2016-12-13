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

    public class PlainTextContent : Content
    {
        public PlainTextContent(string value)
        {
            this.Type = MimeType.Text;
            this.Value = value;
        }
    }

    public class HtmlContent : Content
    {
        public HtmlContent(string value)
        {
            this.Type = MimeType.Html;
            this.Value = value;
        }
    }
}