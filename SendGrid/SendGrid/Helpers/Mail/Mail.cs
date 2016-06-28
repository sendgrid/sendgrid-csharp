using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    ///     Class Mail builds an object that sends an email through SendGrid.
    /// </summary>
    public class Mail
    {
        private Email from;
        private String subject;
        private List<Personalization> personalizations;
        private List<Content> contents;
        private List<Attachment> attachments;
        private String templateId;
        private Dictionary<String, String> headers;
        private Dictionary<String, String> sections;
        private List<String> categories;
        private Dictionary<String, String> customArgs;
        private long sendAt;
        private ASM asm;
        private String batchId;
        private String setIpPoolId;
        private MailSettings mailSettings;
        private TrackingSettings trackingSettings;
        private Email replyTo;

        public Mail()
        {
            return;
        }

        public Mail(Email from, String subject, Email to, Content content)
        {
            this.From = from;
            Personalization personalization = new Personalization();
            personalization.AddTo(to);
            this.AddPersonalization(personalization);
            this.Subject = subject;
            this.AddContent(content);
        }

        [JsonProperty(PropertyName = "from")]
        public Email From
        {
            get
            {
                return from;
            }

            set
            {
                from = value;
            }
        }

        [JsonProperty(PropertyName = "subject")]
        public string Subject
        {
            get
            {
                return subject;
            }

            set
            {
                subject = value;
            }
        }

        [JsonProperty(PropertyName = "personalizations")]
        public List<Personalization> Personalization
        {
            get
            {
                return personalizations;
            }

            set
            {
                personalizations = value;
            }
        }

        [JsonProperty(PropertyName = "content")]
        public List<Content> Contents
        {
            get
            {
                return contents;
            }

            set
            {
                contents = value;
            }
        }

        [JsonProperty(PropertyName = "attachments")]
        public List<Attachment> Attachments
        {
            get
            {
                return attachments;
            }

            set
            {
                attachments = value;
            }
        }

        [JsonProperty(PropertyName = "template_id")]
        public string TemplateId
        {
            get
            {
                return templateId;
            }

            set
            {
                templateId = value;
            }
        }

        [JsonProperty(PropertyName = "headers")]
        public Dictionary<string, string> Headers
        {
            get
            {
                return headers;
            }

            set
            {
                headers = value;
            }
        }

        [JsonProperty(PropertyName = "sections")]
        public Dictionary<string, string> Sections
        {
            get
            {
                return sections;
            }

            set
            {
                sections = value;
            }
        }

        [JsonProperty(PropertyName = "categories")]
        public List<string> Categories
        {
            get
            {
                return categories;
            }

            set
            {
                categories = value;
            }
        }

        [JsonProperty(PropertyName = "custom_args")]
        public Dictionary<string, string> CustomArgs
        {
            get
            {
                return customArgs;
            }

            set
            {
                customArgs = value;
            }
        }

        [JsonProperty(PropertyName = "send_at")]
        public long SendAt
        {
            get
            {
                return sendAt;
            }

            set
            {
                sendAt = value;
            }
        }

        [JsonProperty(PropertyName = "asm")]
        public ASM Asm
        {
            get
            {
                return asm;
            }

            set
            {
                asm = value;
            }
        }

        [JsonProperty(PropertyName = "batch_id")]
        public string BatchId
        {
            get
            {
                return batchId;
            }

            set
            {
                batchId = value;
            }
        }

        [JsonProperty(PropertyName = "ip_pool_name")]
        public string SetIpPoolId
        {
            get
            {
                return setIpPoolId;
            }

            set
            {
                setIpPoolId = value;
            }
        }

        [JsonProperty(PropertyName = "mail_settings")]
        public MailSettings MailSettings
        {
            get
            {
                return mailSettings;
            }

            set
            {
                mailSettings = value;
            }
        }

        [JsonProperty(PropertyName = "tracking_settings")]
        public TrackingSettings TrackingSettings
        {
            get
            {
                return trackingSettings;
            }

            set
            {
                trackingSettings = value;
            }
        }

        [JsonProperty(PropertyName = "reply_to")]
        public Email ReplyTo
        {
            get
            {
                return replyTo;
            }

            set
            {
                replyTo = value;
            }
        }

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

        public void AddHeader(String key, String value)
        {
            if (headers == null)
            {
                headers = new Dictionary<String, String>();
            }
            headers.Add(key, value);
        }

        public void AddSection(String key, String value)
        {
            if (sections == null)
            {
                sections = new Dictionary<String, String>();
            }
            sections.Add(key, value);
        }

        public void AddCategory(String category)
        {
            if (Categories == null)
            {
                Categories = new List<String>();
            }
            Categories.Add(category);
        }

        public void AddCustomArgs(String key, String value)
        {
            if (customArgs == null)
            {
                customArgs = new Dictionary<String, String>();
            }
            customArgs.Add(key, value);
        }

        public String Get()
        {
            return JsonConvert.SerializeObject(this,
                                Formatting.None,
                                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore, StringEscapeHandling = StringEscapeHandling.EscapeHtml });
        }
    }


    public class ClickTracking
    {
        private bool enable;
        private bool enableText;

        [JsonProperty(PropertyName = "enable")]
        public bool Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }

        [JsonProperty(PropertyName = "enable_text")]
        public bool EnableText
        {
            get
            {
                return enableText;
            }

            set
            {
                enableText = value;
            }
        }
    }


    public class OpenTracking
    {
        private bool enable;
        private String substitutionTag;

        [JsonProperty(PropertyName = "enable")]
        public bool Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }

        [JsonProperty(PropertyName = "substitution_tag")]
        public string SubstitutionTag
        {
            get
            {
                return substitutionTag;
            }

            set
            {
                substitutionTag = value;
            }
        }
    }


    public class SubscriptionTracking
    {
        private bool enable;
        private String text;
        private String html;
        private String substitutionTag;

        [JsonProperty(PropertyName = "enable")]
        public bool Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }

        [JsonProperty(PropertyName = "text")]
        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        [JsonProperty(PropertyName = "html")]
        public string Html
        {
            get
            {
                return html;
            }

            set
            {
                html = value;
            }
        }

        [JsonProperty(PropertyName = "substitution_tag")]
        public string SubstitutionTag
        {
            get
            {
                return substitutionTag;
            }

            set
            {
                substitutionTag = value;
            }
        }
    }


    public class Ganalytics
    {
        private bool enable;
        private String utmSource;
        private String utmMedium;
        private String utmTerm;
        private String utmContent;
        private String utmCampaign;

        [JsonProperty(PropertyName = "enable")]
        public bool Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }

        [JsonProperty(PropertyName = "utm_source")]
        public string UtmSource
        {
            get
            {
                return utmSource;
            }

            set
            {
                utmSource = value;
            }
        }

        [JsonProperty(PropertyName = "utm_medium")]
        public string UtmMedium
        {
            get
            {
                return utmMedium;
            }

            set
            {
                utmMedium = value;
            }
        }

        [JsonProperty(PropertyName = "utm_term")]
        public string UtmTerm
        {
            get
            {
                return utmTerm;
            }

            set
            {
                utmTerm = value;
            }
        }

        [JsonProperty(PropertyName = "utm_content")]
        public string UtmContent
        {
            get
            {
                return utmContent;
            }

            set
            {
                utmContent = value;
            }
        }

        [JsonProperty(PropertyName = "utm_campaign")]
        public string UtmCampaign
        {
            get
            {
                return utmCampaign;
            }

            set
            {
                utmCampaign = value;
            }
        }
    }


    public class TrackingSettings
    {
        private ClickTracking clickTracking;
        private OpenTracking openTracking;
        private SubscriptionTracking subscriptionTracking;
        private Ganalytics ganalytics;

        [JsonProperty(PropertyName = "click_tracking")]
        public ClickTracking ClickTracking
        {
            get
            {
                return clickTracking;
            }

            set
            {
                clickTracking = value;
            }
        }

        [JsonProperty(PropertyName = "open_tracking")]
        public OpenTracking OpenTracking
        {
            get
            {
                return openTracking;
            }

            set
            {
                openTracking = value;
            }
        }

        [JsonProperty(PropertyName = "subscription_tracking")]
        public SubscriptionTracking SubscriptionTracking
        {
            get
            {
                return subscriptionTracking;
            }

            set
            {
                subscriptionTracking = value;
            }
        }

        [JsonProperty(PropertyName = "ganalytics")]
        public Ganalytics Ganalytics
        {
            get
            {
                return ganalytics;
            }

            set
            {
                ganalytics = value;
            }
        }
    }


    public class BCCSettings
    {
        private bool enable;
        private String email;

        [JsonProperty(PropertyName = "enable")]
        public bool Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }

        [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }
    }


    public class BypassListManagement
    {
        private bool enable;

        [JsonProperty(PropertyName = "enable")]
        public bool Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }
    }


    public class FooterSettings
    {
        private bool enable;
        private String text;
        private String html;

        [JsonProperty(PropertyName = "enable")]
        public bool Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }

        [JsonProperty(PropertyName = "text")]
        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        [JsonProperty(PropertyName = "html")]
        public string Html
        {
            get
            {
                return html;
            }

            set
            {
                html = value;
            }
        }
    }


    public class SandboxMode
    {
        private bool enable;

        [JsonProperty(PropertyName = "enable")]
        public bool Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }
    }


    public class SpamCheck
    {
        private bool enable;
        private int threshold;
        private String postToUrl;

        [JsonProperty(PropertyName = "enable")]
        public bool Enable
        {
            get
            {
                return enable;
            }

            set
            {
                enable = value;
            }
        }

        [JsonProperty(PropertyName = "threshold")]
        public int Threshold
        {
            get
            {
                return threshold;
            }

            set
            {
                threshold = value;
            }
        }

        [JsonProperty(PropertyName = "post_to_url")]
        public string PostToUrl
        {
            get
            {
                return postToUrl;
            }

            set
            {
                postToUrl = value;
            }
        }
    }


    public class MailSettings
    {
        private BCCSettings bccSettings;
        private BypassListManagement bypassListManagement;
        private FooterSettings footerSettings;
        private SandboxMode sandboxMode;
        private SpamCheck spamCheck;

        [JsonProperty(PropertyName = "bcc")]
        public BCCSettings BccSettings
        {
            get
            {
                return bccSettings;
            }

            set
            {
                bccSettings = value;
            }
        }

        [JsonProperty(PropertyName = "bypass_list_management")]
        public BypassListManagement BypassListManagement
        {
            get
            {
                return bypassListManagement;
            }

            set
            {
                bypassListManagement = value;
            }
        }

        [JsonProperty(PropertyName = "footer")]
        public FooterSettings FooterSettings
        {
            get
            {
                return footerSettings;
            }

            set
            {
                footerSettings = value;
            }
        }

        [JsonProperty(PropertyName = "sandbox_mode")]
        public SandboxMode SandboxMode
        {
            get
            {
                return sandboxMode;
            }

            set
            {
                sandboxMode = value;
            }
        }

        [JsonProperty(PropertyName = "spam_check")]
        public SpamCheck SpamCheck
        {
            get
            {
                return spamCheck;
            }

            set
            {
                spamCheck = value;
            }
        }
    }


    public class ASM
    {
        private int groupId;
        private List<int> groupsToDisplay;

        [JsonProperty(PropertyName = "group_id")]
        public int GroupId
        {
            get
            {
                return groupId;
            }

            set
            {
                groupId = value;
            }
        }

        [JsonProperty(PropertyName = "groups_to_display")]
        public List<int> GroupsToDisplay
        {
            get
            {
                return groupsToDisplay;
            }

            set
            {
                groupsToDisplay = value;
            }
        }
    }


    public class Attachment
    {
        private String content;
        private String type;
        private String filename;
        private String disposition;
        private String contentId;

        [JsonProperty(PropertyName = "content")]
        public string Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
            }
        }

        [JsonProperty(PropertyName = "type")]
        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        [JsonProperty(PropertyName = "filename")]
        public string Filename
        {
            get
            {
                return filename;
            }

            set
            {
                filename = value;
            }
        }

        [JsonProperty(PropertyName = "disposition")]
        public string Disposition
        {
            get
            {
                return disposition;
            }

            set
            {
                disposition = value;
            }
        }

        [JsonProperty(PropertyName = "content_id")]
        public string ContentId
        {
            get
            {
                return contentId;
            }

            set
            {
                contentId = value;
            }
        }
    }


    public class Content
    {
        private String type;
        private String value;

        public Content()
        {
            return;
        }

        public Content(String type, String value)
        {
            this.Type = type;
            this.Value = value;
        }

        [JsonProperty(PropertyName = "type")]
        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        [JsonProperty(PropertyName = "value")]
        public string Value
        {
            get
            {
                return value;
            }

            set
            {
                this.value = value;
            }
        }
    }


    public class Email
    {
        private String name;
        private String address;

        public Email()
        {
            return;
        }

        public Email(String email, String name = null)
        {
            this.Address = email;
            this.Name = name;
        }

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        [JsonProperty(PropertyName = "email")]
        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }
    }


    public class Personalization
    {
        private List<Email> tos;
        private List<Email> ccs;
        private List<Email> bccs;
        private String subject;
        private Dictionary<String, String> headers;
        private Dictionary<String, String> substitutions;
        private Dictionary<String, String> customArgs;
        private long sendAt;

        [JsonProperty(PropertyName = "to")]
        public List<Email> Tos
        {
            get
            {
                return tos;
            }

            set
            {
                tos = value;
            }
        }

        [JsonProperty(PropertyName = "cc")]
        public List<Email> Ccs
        {
            get
            {
                return ccs;
            }

            set
            {
                ccs = value;
            }
        }

        [JsonProperty(PropertyName = "bcc")]
        public List<Email> Bccs
        {
            get
            {
                return bccs;
            }

            set
            {
                bccs = value;
            }
        }

        [JsonProperty(PropertyName = "subject")]
        public string Subject
        {
            get
            {
                return subject;
            }

            set
            {
                subject = value;
            }
        }

        [JsonProperty(PropertyName = "headers")]
        public Dictionary<string, string> Headers
        {
            get
            {
                return headers;
            }

            set
            {
                headers = value;
            }
        }

        [JsonProperty(PropertyName = "substitutions")]
        public Dictionary<string, string> Substitutions
        {
            get
            {
                return substitutions;
            }

            set
            {
                substitutions = value;
            }
        }

        [JsonProperty(PropertyName = "custom_args")]
        public Dictionary<string, string> CustomArgs
        {
            get
            {
                return customArgs;
            }

            set
            {
                customArgs = value;
            }
        }

        [JsonProperty(PropertyName = "send_at")]
        public long SendAt
        {
            get
            {
                return sendAt;
            }

            set
            {
                sendAt = value;
            }
        }

        public void AddTo(Email email)
        {
            if (tos == null)
            {
                tos = new List<Email>();

            }
            tos.Add(email);
        }

        public void AddCc(Email email)
        {
            if (ccs == null)
            {
                ccs = new List<Email>();
            }
            ccs.Add(email);
        }

        public void AddBcc(Email email)
        {
            if (bccs == null)
            {
                bccs = new List<Email>();
            }
            bccs.Add(email);
        }

        public void AddHeader(String key, String value)
        {
            if (headers == null)
            {
                headers = new Dictionary<String, String>();
            }
            headers.Add(key, value);
        }

        public void AddSubstitution(String key, String value)
        {
            if (substitutions == null)
            {
                substitutions = new Dictionary<String, String>();
            }
            substitutions.Add(key, value);
        }

        public void AddCustomArgs(String key, String value)
        {
            if (customArgs == null)
            {
                customArgs = new Dictionary<String, String>();
            }
            customArgs.Add(key, value);
        }
    }
}

