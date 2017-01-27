using Newtonsoft.Json;
using System.Collections.Generic;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    ///     Class Mail builds an object that sends an email through SendGrid.
    /// </summary>
    public class SendGridMessage
    {
        [JsonProperty(PropertyName = "from")]
        public EmailAddress From { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "personalizations")]
        public List<Personalization> Personalizations { get; set; }

        [JsonProperty(PropertyName = "content")]
        public List<Content> Contents { get; set; }

        [JsonIgnore]
        public string PlainTextContent { get; set; }

        [JsonIgnore]
        public string HtmlContent { get; set; }

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
        public EmailAddress ReplyTo { get; set; }

        public void AddTo(EmailAddress email, Personalization personalization = null)
        {
            if (personalization != null)
            {
                personalization.Tos.Add(email);
                return;
            }
            else if (Personalizations != null)
            {
                Personalizations[0].Tos.Add(email);
                return;
            }

            Personalizations = new List<Personalization>() {
                new Personalization() {
                    Tos = new List<EmailAddress>() {
                        email
                    }
                }
            };
            return;
        }

        // TODO: implement the rest of the Personalization properties (e.g. AddTos, AddBcc, AddBccs, etc.)

        public string Serialize()
        {
            // TODO: account for the case when only HTML is provided
            // Plain text, if provided must always come first
            if (PlainTextContent != null || HtmlContent != null )
            {
                Contents = new List<Content>() {
                    new PlainTextContent(PlainTextContent),
                    new HtmlContent(HtmlContent)
                };
            }

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