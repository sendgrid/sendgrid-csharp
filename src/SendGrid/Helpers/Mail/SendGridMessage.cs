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
        public string IpPoolName { get; set; }

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

        public void SetSubject(string subject, int personalizationIndex = 0, Personalization personalization = null)
        {
            if (personalization != null)
            {
                personalization.Subject = subject;
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
                Personalizations[personalizationIndex].Subject = subject;
                return;
            }

            Personalizations = new List<Personalization>() {
                new Personalization()
                {
                    Subject = subject
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

        public void AddSubstitution(string substitutionKey, string substitutionValue, int personalizationIndex = 0, Personalization personalization = null)
        {
            if (personalization != null)
            {
                personalization.Substitutions.Add(substitutionKey, substitutionValue);
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
                Personalizations[personalizationIndex].Substitutions.Add(substitutionKey, substitutionValue);
                return;
            }

            Personalizations = new List<Personalization>() {
                new Personalization()
                {
                    Substitutions = new Dictionary<string, string>()
                    {
                        { substitutionKey, substitutionValue }
                    }
                }
            };
            return;
        }

        public void AddSubstitutions(Dictionary<string, string> substitutions, int personalizationIndex = 0, Personalization personalization = null)
        {
            if (personalization != null)
            {
                personalization.Substitutions = (personalization.Substitutions != null)
                    ? personalization.Substitutions.Union(substitutions).ToDictionary(pair => pair.Key, pair => pair.Value) : substitutions;
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
                Personalizations[personalizationIndex].Substitutions = (Personalizations[personalizationIndex].Substitutions != null)
                    ? Personalizations[personalizationIndex].Substitutions.Union(substitutions).ToDictionary(pair => pair.Key, pair => pair.Value) : substitutions;
                return;
            }

            Personalizations = new List<Personalization>() {
                new Personalization()
                {
                    Substitutions = substitutions
                }
            };
            return;
        }

        public void AddCustomArg(string customArgKey, string customArgValue, int personalizationIndex = 0, Personalization personalization = null)
        {
            if (personalization != null)
            {
                personalization.CustomArgs.Add(customArgKey, customArgValue);
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
                Personalizations[personalizationIndex].CustomArgs.Add(customArgKey, customArgValue);
                return;
            }

            Personalizations = new List<Personalization>() {
                new Personalization()
                {
                    CustomArgs = new Dictionary<string, string>()
                    {
                        { customArgKey, customArgValue }
                    }
                }
            };
            return;
        }

        public void AddCustomArgs(Dictionary<string, string> customArgs, int personalizationIndex = 0, Personalization personalization = null)
        {
            if (personalization != null)
            {
                personalization.CustomArgs = (personalization.CustomArgs != null)
                    ? personalization.CustomArgs.Union(customArgs).ToDictionary(pair => pair.Key, pair => pair.Value) : customArgs;
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
                Personalizations[personalizationIndex].CustomArgs = (Personalizations[personalizationIndex].CustomArgs != null)
                    ? Personalizations[personalizationIndex].CustomArgs.Union(customArgs).ToDictionary(pair => pair.Key, pair => pair.Value) : customArgs;
                return;
            }

            Personalizations = new List<Personalization>() {
                new Personalization()
                {
                    CustomArgs = customArgs
                }
            };
            return;
        }

        public void SetSendAt(int sendAt, int personalizationIndex = 0, Personalization personalization = null)
        {
            if (personalization != null)
            {
                personalization.SendAt = sendAt;
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
                Personalizations[personalizationIndex].SendAt = sendAt;
                return;
            }

            Personalizations = new List<Personalization>() {
                new Personalization()
                {
                    SendAt = sendAt
                }
            };
            return;
        }

        public void SetFrom(EmailAddress email)
        {
            From = email;
        }

        public void SetReplyTo(EmailAddress email)
        {
            ReplyTo = email;
        }

        public void SetGlobalSubject(string subject)
        {
            Subject = subject;
        }

        public void AddContent(string mimeType, string text)
        {
            var content = new Content()
            {
                Type = mimeType,
                Value = text
            };

            if (Contents == null)
            {

                Contents = new List<Content>()
                {
                    content
                };
            }
            else
            {
                Contents.Add(content);
            }
            return;
        }

        public void AddContents(List<Content> contents)
        {
            if (Contents == null)
            {
                Contents = new List<Content>();
                Contents = contents;
            }
            else
            {
                Contents.AddRange(contents);
            }
            return;
        }

        public void AddAttachment(string filename, string content, string type = null, string disposition = null, string content_id = null)
        {
            var attachment = new Attachment()
            {
                Filename = filename,
                Content = content,
                Type = type,
                Disposition = disposition,
                ContentId = content_id
            };

            if (Attachments == null)
            {

                Attachments = new List<Attachment>()
                {
                    attachment
                };
            }
            else
            {
                Attachments.Add(attachment);
            }
            return;
        }

        public void AddAttachments(List<Attachment> attachments)
        {
            if (Attachments == null)
            {
                Attachments = new List<Attachment>();
                Attachments = attachments;
            }
            else
            {
                Attachments.AddRange(attachments);
            }
            return;
        }

        public void SetTemplateId(string templateID)
        {
            TemplateId = templateID;
        }

        public void AddSection(string key, string value)
        {
            if (Sections == null)
            {
                Sections = new Dictionary<string, string>()
                {
                    {  key, value }
                };
            }
            else
            {
                Sections.Add(key, value);
            }
            return;
        }

        public void AddSections(Dictionary<string, string> sections)
        {
            if (Sections == null)
            {
                Sections = sections;
            }
            else
            {
                Sections = (Sections != null)
                    ? Sections.Union(sections).ToDictionary(pair => pair.Key, pair => pair.Value) : sections;
            }
            return;
        }

        public void AddGlobalHeader(string key, string value)
        {
            if (Headers == null)
            {
                Headers = new Dictionary<string, string>()
                {
                    {  key, value }
                };
            }
            else
            {
                Headers.Add(key, value);
            }
            return;
        }

        public void AddGlobalHeaders(Dictionary<string,string> headers)
        {
            if (Headers == null)
            {
                Headers = headers;
            }
            else
            {
                Headers = (Headers != null)
                    ? Headers.Union(headers).ToDictionary(pair => pair.Key, pair => pair.Value) : headers;
            }
            return;
        }

        public void AddCategory(string category)
        {
            if (Categories == null)
            {

                Categories = new List<string>()
                {
                    category
                };
            }
            else
            {
                Categories.Add(category);
            }
            return;
        }

        public void AddCategories(List<string> categories)
        {
            if (Categories == null)
            {
                Categories = new List<string>();
                Categories = categories;
            }
            else
            {
                Categories.AddRange(categories);
            }
            return;
        }

        public void AddGlobalCustomArg(string key, string value)
        {
            if (CustomArgs == null)
            {
                CustomArgs = new Dictionary<string, string>()
                {
                    {  key, value }
                };
            }
            else
            {
                CustomArgs.Add(key, value);
            }
            return;
        }

        public void AddGlobalCustomArgs(Dictionary<string, string> customArgs)
        {
            if (CustomArgs == null)
            {
                CustomArgs = customArgs;
            }
            else
            {
                customArgs = (CustomArgs != null)
                    ? Headers.Union(customArgs).ToDictionary(pair => pair.Key, pair => pair.Value) : customArgs;
            }
            return;
        }

        public void SetGlobalSendAt(int sendAt)
        {
            SendAt = sendAt;
        }

        public void SetBatchId(string batchId)
        {
            BatchId = batchId;
        }

        public void SetAsm(int groupID, List<int> groupsToDisplay)
        {
            Asm = new ASM();
            Asm.GroupId = groupID;
            Asm.GroupsToDisplay = groupsToDisplay;
            return;
        }

        public void SetIpPoolName(string ipPoolName)
        {
            IpPoolName = ipPoolName;
        }

        public void SetBccSetting(bool enable, string email)
        {
            if( MailSettings == null)
            {
                MailSettings = new MailSettings();
            }
            MailSettings.BccSettings = new BCCSettings()
            {
                Enable = enable,
                Email = email
            };
            return;
        }

        public void SetBypassListManagement(bool enable)
        {
            if (MailSettings == null)
            {
                MailSettings = new MailSettings();
            }
            MailSettings.BypassListManagement = new BypassListManagement()
            {
                Enable = enable
            };
            return;
        }

        public void SetFooterSetting(bool enable, string html = null, string text = null)
        {
            if (MailSettings == null)
            {
                MailSettings = new MailSettings();
            }
            MailSettings.FooterSettings = new FooterSettings()
            {
                Enable = enable,
                Html = html,
                Text = text
            };
            return;
        }

        public void SetSandBoxMode(bool enable)
        {
            if (MailSettings == null)
            {
                MailSettings = new MailSettings();
            }
            MailSettings.SandboxMode = new SandboxMode()
            {
                Enable = enable
            };
            return;
        }

        public void SetSpamCheck(bool enable, int threshold = 1, string postToUrl = null)
        {
            if (MailSettings == null)
            {
                MailSettings = new MailSettings();
            }
            MailSettings.SpamCheck = new SpamCheck()
            {
                Enable = enable,
                Threshold = threshold,
                PostToUrl = postToUrl
            };
            return;
        }

        public void SetClickTracking(bool enable, bool enableText)
        {
            if (TrackingSettings == null)
            {
                TrackingSettings = new TrackingSettings();
            }
            TrackingSettings.ClickTracking = new ClickTracking()
            {
                Enable = enable,
                EnableText = enableText
            };
            return;
        }

        public void SetOpenTracking(bool enable, string substitutionTag)
        {
            if (TrackingSettings == null)
            {
                TrackingSettings = new TrackingSettings();
            }
            TrackingSettings.OpenTracking = new OpenTracking()
            {
                Enable = enable,
                SubstitutionTag = substitutionTag
            };
            return;
        }

        public void SetSubscriptionTracking(bool enable, string html = null, string text = null, string substitutionTag = null)
        {
            if (TrackingSettings == null)
            {
                TrackingSettings = new TrackingSettings();
            }
            TrackingSettings.SubscriptionTracking = new SubscriptionTracking()
            {
                Enable = enable,
                SubstitutionTag = substitutionTag,
                Html = html,
                Text = text
            };
            return;
        }

        public void SetGoogleAnalytics(bool enable, string utmCampaign = null, string utmContent = null, string utmMedium = null, string utmSource = null, string utmTerm = null)
        {
            if (TrackingSettings == null)
            {
                TrackingSettings = new TrackingSettings();
            }
            TrackingSettings.Ganalytics = new Ganalytics()
            {
                Enable = enable,
                UtmCampaign = utmCampaign,
                UtmContent = utmContent,
                UtmMedium = utmMedium,
                UtmSource = utmSource,
                UtmTerm = utmTerm
            };
            return;
        }

        // TODO: implement the reamining properties (e.g. see the Model directory)

        public string Serialize()
        {
            if (PlainTextContent != null || HtmlContent != null )
            {
                Contents = new List<Content>()
                {
                    new PlainTextContent(PlainTextContent),
                    new HtmlContent(HtmlContent)
                };
                PlainTextContent = null;
                HtmlContent = null;
            }
            else if( Contents != null )
            {
                for (var i = 0; i < Contents.Count; i++)
                {
                    if(Contents[i].Type == MimeType.Text)
                    {
                        var tempContent = new Content();
                        tempContent = Contents[i];
                        Contents.RemoveAt(i);
                        Contents.Insert(0, tempContent);
                    }
                }
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