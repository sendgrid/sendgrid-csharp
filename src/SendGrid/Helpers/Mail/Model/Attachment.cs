using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    public class Attachment
    {
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "filename")]
        public string Filename { get; set; }

        [JsonProperty(PropertyName = "disposition")]
        public string Disposition { get; set; }

        [JsonProperty(PropertyName = "content_id")]
        public string ContentId { get; set; }
    }
}