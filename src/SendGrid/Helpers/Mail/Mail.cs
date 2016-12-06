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
        public List<Personalization> Personalization { get; set; }

        [JsonProperty(PropertyName = "content")]
        public List<Content> Contents { get; set; }

        [JsonProperty(PropertyName = "attachments")]
        public List<Attachment> Attachments { get; set; }

        [JsonProperty(PropertyName = "template_id")]
        public string TemplateId { get; set; }

        [JsonProperty(PropertyName = "headers")]
        public Dictionary<string, string> Headers { get; set; }

        [JsonProperty(PropertyName = "sections")]
        public Dictionary<string, string> Sections { get; set; }

        [JsonProperty(PropertyName = "categories")]
        public List<string> Categories { get; set; }

        [JsonProperty(PropertyName = "custom_args")]
        public Dictionary<string, string> CustomArgs { get; set; }

        [JsonProperty(PropertyName = "send_at")]
        public long? SendAt { get; set; }

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
            if (Personalization == null)
            {
                Personalization = new List<Personalization>();
            }
            Personalization.Add(personalization);
        }

        public void AddContent(Content content)
        {
            if (Contents == null)
            {
                Contents = new List<Content>();
            }
            Contents.Add(content);
        }

        public void AddAttachment(Attachment attachment)
        {
            if (Attachments == null)
            {
                Attachments = new List<Attachment>();
            }
            Attachments.Add(attachment);
        }

        public void AddHeader(string key, string value)
        {
            if (Headers == null)
            {
                Headers = new Dictionary<string, string>();
            }
            Headers.Add(key, value);
        }

        public void AddSection(string key, string value)
        {
            if (Sections == null)
            {
                Sections = new Dictionary<string, string>();
            }
            Sections.Add(key, value);
        }

        public void AddCategory(string category)
        {
            if (Categories == null)
            {
                Categories = new List<string>();
            }
            Categories.Add(category);
        }

        public void AddCustomArgs(string key, string value)
        {
            if (CustomArgs == null)
            {
                CustomArgs = new Dictionary<string, string>();
            }
            CustomArgs.Add(key, value);
        }

        public string Get()
        {
            return JsonConvert.SerializeObject(this,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Include,
                    StringEscapeHandling = StringEscapeHandling.EscapeHtml
                });
        }
    }
}