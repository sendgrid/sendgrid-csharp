using System.Collections.Generic;
using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    public class Personalization
    {
        [JsonProperty(PropertyName = "to")]
        public List<EmailAddress> Tos { get; set; }

        [JsonProperty(PropertyName = "cc")]
        public List<EmailAddress> Ccs { get; set; }

        [JsonProperty(PropertyName = "bcc")]
        public List<EmailAddress> Bccs { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "headers")]
        public Dictionary<string, string> Headers { get; set; }

        [JsonProperty(PropertyName = "substitutions")]
        public Dictionary<string, string> Substitutions { get; set; }

        [JsonProperty(PropertyName = "custom_args")]
        public Dictionary<string, string> CustomArgs { get; set; }

        [JsonProperty(PropertyName = "send_at")]
        public long? SendAt { get; set; }
    }
}