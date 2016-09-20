using System.Collections.Generic;
using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    public class Personalization
    {
        [JsonProperty(PropertyName = "to")]
        public List<Email> Tos { get; set; } = new List<Email>();

        [JsonProperty(PropertyName = "cc")]
        public List<Email> Ccs { get; set; } = new List<Email>();

        [JsonProperty(PropertyName = "bcc")]
        public List<Email> Bccs { get; set; } = new List<Email>();

        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "headers")]
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        [JsonProperty(PropertyName = "substitutions")]
        public Dictionary<string, string> Substitutions { get; set; } = new Dictionary<string, string>();

        [JsonProperty(PropertyName = "custom_args")]
        public Dictionary<string, string> CustomArgs { get; set; } = new Dictionary<string, string>();

        [JsonProperty(PropertyName = "send_at")]
        public long SendAt { get; set; }

        public void AddTo(Email email)
        {
            Tos.Add(email);
        }

        public void AddCc(Email email)
        {
            Ccs.Add(email);
        }

        public void AddBcc(Email email)
        {
            Bccs.Add(email);
        }

        public void AddHeader(string key, string value)
        {
            Headers.Add(key, value);
        }

        public void AddSubstitution(string key, string value)
        {
            Substitutions.Add(key, value);
        }

        public void AddCustomArgs(string key, string value)
        {
            CustomArgs.Add(key, value);
        }
    }
}