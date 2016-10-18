using Newtonsoft.Json;
using System.Collections.Generic;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    ///     Extension methods for Mail.
    /// </summary>
    public static class MailExtensions
    {
        
        public static void AddPersonalization(this Models.Mail mail, Personalization personalization)
        {
            if (mail.Personalization == null)
            {
                mail.Personalization = new List<Personalization>();
            }

            mail.Personalization.Add(personalization);
        }

        public static void AddContent(this Models.Mail mail, Content content)
        {
            if (mail.Contents == null)
            {
                mail.Contents = new List<Content>();
            }

            mail.Contents.Add(content);
        }

        public static void AddAttachment(this Models.Mail mail, Attachment attachment)
        {
            if (mail.Attachments == null)
            {
                mail.Attachments = new List<Attachment>();
            }

            mail.Attachments.Add(attachment);
        }

        public static void AddHeader(this Models.Mail mail, string key, string value)
        {
            if (mail.Headers == null)
            {
                mail.Headers = new Dictionary<string, string>();
            }

            mail.Headers.Add(key, value);
        }

        public static void AddSection(this Models.Mail mail, string key, string value)
        {
            if (mail.Sections == null)
            {
                mail.Sections = new Dictionary<string, string>();
            }

            mail.Sections.Add(key, value);
        }

        public static void AddCategory(this Models.Mail mail, string category)
        {
            if (mail.Categories == null)
            {
                mail.Categories = new List<string>();
            }

            mail.Categories.Add(category);
        }

        public static void AddCustomArgs(this Models.Mail mail, string key, string value)
        {
            if (mail.CustomArgs == null)
            {
                mail.CustomArgs = new Dictionary<string, string>();
            }

            mail.CustomArgs.Add(key, value);
        }

        public static string Serialize(this Models.Mail mail)
        {
            return JsonConvert.SerializeObject(mail,
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