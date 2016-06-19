using Newtonsoft.Json;
using System.Collections.Generic;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    ///     Class Mail builds an object that sends an email through SendGrid.
    /// </summary>
    public class Mail
    {
        public Mail()
        {
        }

        public Mail(Email from, string subject, Email to, Content content)
        {
            this.From = from;

            var personalization = new Personalization();
            personalization.AddTo(to);
            this.AddPersonalization(personalization);

            this.Subject = subject;
            this.AddContent(content);
        }

        [JsonProperty(PropertyName = "from")]
        public Email From { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "personalizations")]
        public List<Personalization> Personalization { get; } = new List<Personalization>();

        [JsonProperty(PropertyName = "content")]
        public List<Content> Contents { get; } = new List<Content>();

        [JsonProperty(PropertyName = "attachments")]
        public List<Attachment> Attachments { get; } = new List<Attachment>();

        [JsonProperty(PropertyName = "template_id")]
        public string TemplateId { get; set; }

        [JsonProperty(PropertyName = "headers")]
        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();

        [JsonProperty(PropertyName = "sections")]
        public Dictionary<string, string> Sections { get; } = new Dictionary<string, string>();

        [JsonProperty(PropertyName = "categories")]
        public List<string> Categories { get; } = new List<string>();

        [JsonProperty(PropertyName = "custom_args")]
        public Dictionary<string, string> CustomArgs { get; } = new Dictionary<string, string>();

        [JsonProperty(PropertyName = "send_at")]
        public long SendAt { get; set; }

        [JsonProperty(PropertyName = "asm")]
        public ASM Asm { get; set; }

        [JsonProperty(PropertyName = "batch_id")]
        public string BatchId { get; set; }

        [JsonProperty(PropertyName = "ip_pool_name")]
        public string SetIpPoolId { get; set; }

        [JsonProperty(PropertyName = "mail_settings")]
        public MailSettings MailSettings { get; set; }

        [JsonProperty(PropertyName = "tracking_settings")]
        public TrackingSettings TrackingSettings { get; set; }

        [JsonProperty(PropertyName = "reply_to")]
        public Email ReplyTo { get; set; }

        public void AddPersonalization(Personalization personalization)
        {
            Personalization.Add(personalization);
        }

        public void AddContent(Content content)
        {
            Contents.Add(content);
        }

        public void AddAttachment(Attachment attachment)
        {
            Attachments.Add(attachment);
        }

        public void AddHeader(string key, string value)
        {
            Headers.Add(key, value);
        }

        public void AddSection(string key, string value)
        {
            Sections.Add(key, value);
        }

        public void AddCategory(string category)
        {
            Categories.Add(category);
        }

        public void AddCustomArgs(string key, string value)
        {
            CustomArgs.Add(key, value);
        }

        public string Get()
        {
            return JsonConvert.SerializeObject(this,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
        }
    }
}