using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    ///     Class SendGridMessage builds an object that sends an email through SendGrid.
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

        public void AddTo(EmailAddress email, int personalizationIndex = 0, Personalization personalization = null)
        {
            if (personalization != null)
            {
                personalization.Tos.Add(email);
                if (Personalizations == null )
                {
                    Personalizations = new List<Personalization>();
                    Personalizations.Add(personalization);
                }
                else
                {
                    Personalizations.Add(personalization);
                }
                return;
            }

            if (Personalizations != null)
            {
                Personalizations[personalizationIndex].Tos.Add(email);
                return;
            }

            Personalizations = new List<Personalization>() {
                new Personalization()
                {
                    Tos = new List<EmailAddress>()
                    {
                        email
                    }
                }
            };
            return;
        }

        public void AddTos(List<EmailAddress> emails, int personalizationIndex = 0, Personalization personalization = null)
        {
            if (personalization != null)
            {
                personalization.Tos.AddRange(emails);
                if (Personalizations == null)
                {
                    Personalizations = new List<Personalization>();
                    Personalizations.Add(personalization);
                }
                else
                {
                    Personalizations.Add(personalization);
                }
                return;
            }

            if (Personalizations != null)
            {
                Personalizations[personalizationIndex].Tos.AddRange(emails);
                return;
            }

            Personalizations = new List<Personalization>() {
                new Personalization()
                {
                    Tos = emails
                }
            };
            return;
        }

        public void AddCc(EmailAddress email, int personalizationIndex = 0, Personalization personalization = null)
        {
            if (personalization != null)
            {
                personalization.Ccs.Add(email);
                if (Personalizations == null)
                {
                    Personalizations = new List<Personalization>();
                    Personalizations.Add(personalization);
                }
                else
                {
                    Personalizations.Add(personalization);
                }
                return;
            }

            if (Personalizations != null)
            {
                Personalizations[personalizationIndex].Ccs.Add(email);
                return;
            }

            Personalizations = new List<Personalization>() {
                new Personalization()
                {
                    Ccs = new List<EmailAddress>()
                    {
                        email
                    }
                }
            };
            return;
        }

        public void AddCcs(List<EmailAddress> emails, int personalizationIndex = 0, Personalization personalization = null)
        {
            if (personalization != null)
            {
                personalization.Ccs.AddRange(emails);
                if (Personalizations == null)
                {
                    Personalizations = new List<Personalization>();
                    Personalizations.Add(personalization);
                }
                else
                {
                    Personalizations.Add(personalization);
                }
                return;
            }

            if (Personalizations != null)
            {
                Personalizations[personalizationIndex].Ccs.AddRange(emails);
                return;
            }

            Personalizations = new List<Personalization>() {
                new Personalization()
                {
                    Ccs = emails
                }
            };
            return;
        }

        public void AddBcc(EmailAddress email, int personalizationIndex = 0, Personalization personalization = null)
        {
            if (personalization != null)
            {
                personalization.Bccs.Add(email);
                if (Personalizations == null)
                {
                    Personalizations = new List<Personalization>();
                    Personalizations.Add(personalization);
                }
                else
                {
                    Personalizations.Add(personalization);
                }
                return;
            }

            if (Personalizations != null)
            {
                Personalizations[personalizationIndex].Bccs.Add(email);
                return;
            }

            Personalizations = new List<Personalization>() {
                new Personalization()
                {
                    Bccs = new List<EmailAddress>()
                    {
                        email
                    }
                }
            };
            return;
        }

        public void AddBccs(List<EmailAddress> emails, int personalizationIndex = 0, Personalization personalization = null)
        {
            if (personalization != null)
            {
                personalization.Bccs.AddRange(emails);
                if (Personalizations == null)
                {
                    Personalizations = new List<Personalization>();
                    Personalizations.Add(personalization);
                }
                else
                {
                    Personalizations.Add(personalization);
                }
                return;
            }

            if (Personalizations != null)
            {
                Personalizations[personalizationIndex].Bccs.AddRange(emails);
                return;
            }

            Personalizations = new List<Personalization>() {
                new Personalization()
                {
                    Bccs = emails
                }
            };
            return;
        }

        public void AddHeader(string headerKey, string headerValue, int personalizationIndex = 0, Personalization personalization = null)
        {
            if (personalization != null)
            {
                personalization.Headers.Add(headerKey, headerValue);
                if (Personalizations == null)
                {
                    Personalizations = new List<Personalization>();
                    Personalizations.Add(personalization);
                }
                else
                {
                    Personalizations.Add(personalization);
                }
                return;
            }

            if (Personalizations != null)
            {
                Personalizations[personalizationIndex].Headers.Add(headerKey, headerValue);
                return;
            }

            Personalizations = new List<Personalization>() {
                new Personalization()
                {
                    Headers = new Dictionary<string, string>()
                    {
                        { headerKey, headerValue }
                    }
                }
            };
            return;
        }

        public void AddHeaders(Dictionary<string, string> headers, int personalizationIndex = 0, Personalization personalization = null)
        {
            if (personalization != null)
            {
                personalization.Headers = (personalization.Headers != null)
                    ? personalization.Headers.Union(headers).ToDictionary(pair => pair.Key, pair => pair.Value) : headers;
                if (Personalizations == null)
                {
                    Personalizations = new List<Personalization>();
                    Personalizations.Add(personalization);
                }
                else
                {
                    Personalizations.Add(personalization);
                }
                return;
            }

            if (Personalizations != null)
            {
                Personalizations[personalizationIndex].Headers = (Personalizations[personalizationIndex].Headers != null)
                    ? Personalizations[personalizationIndex].Headers.Union(headers).ToDictionary(pair => pair.Key, pair => pair.Value) : headers;
                return;
            }

            Personalizations = new List<Personalization>() {
                new Personalization()
                {
                    Headers = headers
                }
            };
            return;
        }

        // TODO: implement the rest of the Personalization properties (e.g. AddTos, AddBcc, AddBccs, etc.)
        // TODO: implement the reamining properties (e.g. see the Model directory)

        public string Serialize()
        {
            // TODO: account for the case when only HTML is provided
            // Plain text, if provided must always come first
            if (PlainTextContent != null || HtmlContent != null )
            {
                Contents = new List<Content>()
                {
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