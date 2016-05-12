using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SendGrid.Utilities;
using System;

namespace SendGrid.Model
{
    public class TemplateVersion
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("template_id")]
        public string TemplateId { get; set; }

        [JsonProperty("active")]
        [JsonConverter(typeof(IntegerBooleanConverter))]
        public bool IsActive { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("html_content")]
        public string HtmlContent { get; set; }

        [JsonProperty("plain_content")]
        public string TextContent { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(SendGridDateTimeConverter))]
        public DateTime UpdatedOn { get; set; }
    }
}
