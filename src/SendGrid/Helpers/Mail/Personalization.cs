using System.Collections.Generic;
using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    public class Personalization
    {
        [JsonProperty(PropertyName = "to")]
        public List<MailAddress> Tos { get; set; }

        [JsonProperty(PropertyName = "cc")]
        public List<MailAddress> Ccs { get; set; }

        [JsonProperty(PropertyName = "bcc")]
        public List<MailAddress> Bccs { get; set; }

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

        public void AddTo(MailAddress email)
        {
            if (Tos == null)
                Tos = new List<MailAddress>();

            Tos.Add(email);
        }

        public void AddCc(MailAddress email)
        {
            if (Ccs == null)
                Ccs = new List<MailAddress>();

            Ccs.Add(email);
        }

        public void AddBcc(MailAddress email)
        {
            if (Bccs == null)
                Bccs = new List<MailAddress>();

            Bccs.Add(email);
        }

        public void AddHeader(string key, string value)
        {
            if (Headers == null)
                Headers = new Dictionary<string, string>();

            Headers.Add(key, value);
        }

        public void AddSubstitution(string key, string value)
        {
            if (Substitutions == null)
                Substitutions = new Dictionary<string, string>();

            Substitutions.Add(key, value);
        }

        public void AddCustomArgs(string key, string value)
        {
            if (CustomArgs == null)
                CustomArgs = new Dictionary<string, string>();

            CustomArgs.Add(key, value);
        }
    }
}