using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// Class SendGridMessage builds an object that sends an email through SendGrid.
    /// </summary>
    public class SendGridMessage
    {
        /// <summary>
        /// An email object containing the email address and name of the sender. Unicode encoding is not supported for the from field.
        /// </summary>
        [JsonProperty(PropertyName = "from")]
        public EmailAddress From { get; set; }

        /// <summary>
        /// The subject of your email. This may be overridden by personalizations[x].subject.
        /// </summary>
        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        /// <summary>
        /// A list of messages and their metadata. Each object within personalizations can be thought of as an envelope - it defines who should receive an individual message and how that message should be handled. For more information, please see our documentation on Personalizations. Parameters in personalizations will override the parameters of the same name from the message level.
        /// https://sendgrid.com/docs/Classroom/Send/v3_Mail_Send/personalizations.html
        /// </summary>
        [JsonProperty(PropertyName = "personalizations")]
        public List<Personalization> Personalizations { get; set; }

        /// <summary>
        /// A list in which you may specify the content of your email. You can include multiple mime types of content, but you must specify at least one. To include more than one mime type, simply add another object to the array containing the type and value parameters. If included, text/plain and text/html must be the first indices of the array in this order. If you choose to include the text/plain or text/html mime types, they must be the first indices of the content array in the order text/plain, text/html.*Content is NOT mandatory if you using a transactional template and have defined the template_id in the Request
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        public List<Content> Contents { get; set; }

        /// <summary>
        /// A Content object with a Mime Type of text/plain.
        /// </summary>
        [JsonIgnore]
        public string PlainTextContent { get; set; }

        /// <summary>
        /// A Content object with a Mime Type of text/html.
        /// </summary>
        [JsonIgnore]
        public string HtmlContent { get; set; }

        /// <summary>
        /// A list of objects in which you can specify any attachments you want to include.
        /// </summary>
        [JsonProperty(PropertyName = "attachments")]
        public List<Attachment> Attachments { get; set; }

        /// <summary>
        /// The id of a template that you would like to use. If you use a template that contains content and a subject (either text or html), you do not need to specify those in the respective personalizations or message level parameters.
        /// </summary>
        [JsonProperty(PropertyName = "template_id")]
        public string TemplateId { get; set; }

        /// <summary>
        /// An object containing key/value pairs of header names and the value to substitute for them. You must ensure these are properly encoded if they contain unicode characters. Must not be any of the following reserved headers: x-sg-id, x-sg-eid, received, dkim-signature, Content-Type, Content-Transfer-Encoding, To, From, Subject, Reply-To, CC, BCC
        /// </summary>
        [JsonProperty(PropertyName = "headers")]
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// An object of key/value pairs that define large blocks of content that can be inserted into your emails using substitution tags.
        /// </summary>
        [JsonProperty(PropertyName = "sections")]
        public Dictionary<string, string> Sections { get; set; }

        /// <summary>
        /// A list of category names for this message. Each category name may not exceed 255 characters. You cannot have more than 10 categories per request.
        /// </summary>
        [JsonProperty(PropertyName = "categories")]
        public List<string> Categories { get; set; }

        /// <summary>
        /// Values that are specific to the entire send that will be carried along with the email and its activity data. Substitutions will not be made on custom arguments, so any string that is entered into this parameter will be assumed to be the custom argument that you would like to be used. This parameter is overridden by any conflicting personalizations[x].custom_args if that parameter has been defined. If personalizations[x].custom_args has been defined but does not conflict with the values defined within this parameter, the two will be merged. The combined total size of these custom arguments may not exceed 10,000 bytes.
        /// </summary>
        [JsonProperty(PropertyName = "custom_args")]
        public Dictionary<string, string> CustomArgs { get; set; }

        /// <summary>
        /// A unix timestamp allowing you to specify when you want your email to be sent from SendGrid. This is not necessary if you want the email to be sent at the time of your API request.
        /// </summary>
        [JsonProperty(PropertyName = "send_at")]
        public long? SendAt { get; set; }

        /// <summary>
        /// An object allowing you to specify how to handle unsubscribes.
        /// </summary>
        [JsonProperty(PropertyName = "asm")]
        public ASM Asm { get; set; }

        /// <summary>
        /// This ID represents a batch of emails (AKA multiple sends of the same email) to be associated to each other for scheduling. Including a batch_id in your request allows you to include this email in that batch, and also enables you to cancel or pause the delivery of that entire batch. For more information, please read about Cancel Scheduled Sends.
        /// https://sendgrid.com/docs/API_Reference/Web_API_v3/cancel_schedule_send.html
        /// </summary>
        [JsonProperty(PropertyName = "batch_id")]
        public string BatchId { get; set; }

        /// <summary>
        /// The IP Pool that you would like to send this email from.
        /// </summary>
        [JsonProperty(PropertyName = "ip_pool_name")]
        public string IpPoolName { get; set; }

        /// <summary>
        /// A collection of different mail settings that you can use to specify how you would like this email to be handled.
        /// </summary>
        [JsonProperty(PropertyName = "mail_settings")]
        public MailSettings MailSettings { get; set; }

        /// <summary>
        /// Settings to determine how you would like to track the metrics of how your recipients interact with your email.
        /// </summary>
        [JsonProperty(PropertyName = "tracking_settings")]
        public TrackingSettings TrackingSettings { get; set; }

        /// <summary>
        /// An email object containing the email address and name of the individual who should receive responses to your email.
        /// </summary>
        [JsonProperty(PropertyName = "reply_to")]
        public EmailAddress ReplyTo { get; set; }

        /// <summary>
        /// Add a recipient email.
        /// </summary>
        /// <param name="email">An email recipient that may contain the recipient’s name, but must always contain the recipient’s email.</param>
        /// <param name="personalizationIndex">Specify the index of the Personalization object where you want to add the recipient email.</param>
        /// <param name="personalization">A personalization object to append to the message.</param>
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
                if((personalizationIndex != 0) && (Personalizations.Count() <= personalizationIndex))
                {
                    var p = new Personalization();
                    Personalizations.Insert(personalizationIndex, p);
                }
                if (Personalizations[personalizationIndex].Tos == null)
                {
                    Personalizations[personalizationIndex].Tos = new List<EmailAddress>();
                }
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

        /// <summary>
        /// Add recipient emails.
        /// </summary>
        /// <param name="emails">A list of recipients. Each email object within this array may contain the recipient’s name, but must always contain the recipient’s email.</param>
        /// <param name="personalizationIndex">Specify the index of the Personalization object where you want to add the recipient emails.</param>
        /// <param name="personalization">A personalization object to append to the message.</param>
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
                if ((personalizationIndex != 0) && (Personalizations.Count() <= personalizationIndex))
                {
                    var p = new Personalization();
                    Personalizations.Insert(personalizationIndex, p);
                }
                if (Personalizations[personalizationIndex].Tos == null)
                {
                    Personalizations[personalizationIndex].Tos = new List<EmailAddress>();
                }
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

        /// <summary>
        /// Add a cc email recipient.
        /// </summary>
        /// <param name="email">An email recipient that may contain the recipient’s name, but must always contain the recipient’s email.</param>
        /// <param name="personalizationIndex">Specify the index of the Personalization object where you want to add the cc email.</param>
        /// <param name="personalization">A personalization object to append to the message.</param>
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
                if (Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    Personalizations.Insert(personalizationIndex, p);
                }

                if (Personalizations[personalizationIndex].Ccs == null)
                {
                    Personalizations[personalizationIndex].Ccs = new List<EmailAddress>();
                }

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

        /// <summary>
        /// Add cc recipient emails.
        /// </summary>
        /// <param name="emails">A list of cc recipients. Each email object within this array may contain the recipient’s name, but must always contain the recipient’s email.</param>
        /// <param name="personalizationIndex">Specify the index of the Personalization object where you want to add the cc emails.</param>
        /// <param name="personalization">A personalization object to append to the message.</param>
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
                if (Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    Personalizations.Insert(personalizationIndex, p);
                }
                if (Personalizations[personalizationIndex].Ccs == null)
                {
                    Personalizations[personalizationIndex].Ccs = new List<EmailAddress>();
                }
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

        /// <summary>
        /// Add a bcc recipient emails.
        /// </summary>
        /// <param name="email">An email recipient that may contain the recipient’s name, but must always contain the recipient’s email.</param>
        /// <param name="personalizationIndex">Specify the index of the Personalization object where you want to add the bcc email.</param>
        /// <param name="personalization">A personalization object to append to the message.</param>
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
                if (Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    Personalizations.Insert(personalizationIndex, p);
                }
                if (Personalizations[personalizationIndex].Bccs == null)
                {
                    Personalizations[personalizationIndex].Bccs = new List<EmailAddress>();
                }
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

        /// <summary>
        /// Add bcc email recipients.
        /// </summary>
        /// <param name="emails">A list of bcc recipients. Each email object within this array may contain the recipient’s name, but must always contain the recipient’s email.</param>
        /// <param name="personalizationIndex">Specify the index of the Personalization object where you want to add the bcc emails.</param>
        /// <param name="personalization">A personalization object to append to the message.</param>
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
                if (Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    Personalizations.Insert(personalizationIndex, p);
                }
                if (Personalizations[personalizationIndex].Bccs == null)
                {
                    Personalizations[personalizationIndex].Bccs = new List<EmailAddress>();
                }
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

        /// <summary>
        /// Add a subject to the email.
        /// </summary>
        /// <param name="subject">The subject line of your email.</param>
        /// <param name="personalizationIndex">Specify the index of the Personalization object where you want to add the subject.</param>
        /// <param name="personalization">A personalization object to append to the message.</param>
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

        /// <summary>
        /// Add a header to the email.
        /// </summary>
        /// <param name="headerKey">Header key. (e.g. X-Header)</param>
        /// <param name="headerValue">Header value.</param>
        /// <param name="personalizationIndex">Specify the index of the Personalization object where you want to add the header.</param>
        /// <param name="personalization">A personalization object to append to the message.</param>
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
                if (Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    Personalizations.Insert(personalizationIndex, p);
                }
                if (Personalizations[personalizationIndex].Headers == null)
                {
                    Personalizations[personalizationIndex].Headers = new Dictionary<string, string>();
                }
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

        /// <summary>
        /// Add headers to the email.
        /// </summary>
        /// <param name="headers">A list of Headers.</param>
        /// <param name="personalizationIndex">Specify the index of the Personalization object where you want to add the headers.</param>
        /// <param name="personalization">A personalization object to append to the message.</param>
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
                if (Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    Personalizations.Insert(personalizationIndex, p);
                }
                if (Personalizations[personalizationIndex].Headers == null)
                {
                    Personalizations[personalizationIndex].Headers = new Dictionary<string, string>();
                }
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

        /// <summary>
        /// Add a substitution to the email.
        /// You may not include more than 100 substitutions per personalization object, and the total collective size of your substitutions may not exceed 10,000 bytes per personalization object.
        /// </summary>
        /// <param name="substitutionKey">The substitution key.</param>
        /// <param name="substitutionValue">The substitution value.</param>
        /// <param name="personalizationIndex">Specify the index of the Personalization object where you want to add the substitution.</param>
        /// <param name="personalization">A personalization object to append to the message.</param>
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
                if (Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    Personalizations.Insert(personalizationIndex, p);
                }
                if (Personalizations[personalizationIndex].Substitutions == null)
                {
                    Personalizations[personalizationIndex].Substitutions = new Dictionary<string, string>();
                }
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

        /// <summary>
        /// Add substitutions to the email.
        /// </summary>
        /// <param name="substitutions">A list of Substituions.</param>
        /// <param name="personalizationIndex">Specify the index of the Personalization object where you want to add the substitutions.</param>
        /// <param name="personalization">A personalization object to append to the message.</param>
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
                if (Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    Personalizations.Insert(personalizationIndex, p);
                }
                if (Personalizations[personalizationIndex].Substitutions == null)
                {
                    Personalizations[personalizationIndex].Substitutions = new Dictionary<string, string>();
                }
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

        /// <summary>
        /// Add a custom arguement to the email.
        /// </summary>
        /// <param name="customArgKey">The custom arguement key.</param>
        /// <param name="customArgValue">The custom arguement value.</param>
        /// <param name="personalizationIndex">Specify the index of the Personalization object where you want to add the custom arg.</param>
        /// <param name="personalization">A personalization object to append to the message.</param>
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
                if (Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    Personalizations.Insert(personalizationIndex, p);
                }
                if (Personalizations[personalizationIndex].CustomArgs == null)
                {
                    Personalizations[personalizationIndex].CustomArgs = new Dictionary<string, string>();
                }
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

        /// <summary>
        /// Add custom arguements to the email.
        /// </summary>
        /// <param name="customArgs">A list of CustomArgs.</param>
        /// <param name="personalizationIndex">Specify the index of the Personalization object where you want to add the custom args.</param>
        /// <param name="personalization">A personalization object to append to the message.</param>
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
                if (Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    Personalizations.Insert(personalizationIndex, p);
                }
                if (Personalizations[personalizationIndex].CustomArgs == null)
                {
                    Personalizations[personalizationIndex].CustomArgs = new Dictionary<string, string>();
                }
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

        /// <summary>
        /// Specify the unix timestamp to specify when you want the email to be sent from SendGrid.
        /// </summary>
        /// <param name="sendAt"></param>
        /// <param name="personalizationIndex">Specify the index of the Personalization object where you want to add the send at timestamp.</param>
        /// <param name="personalization">A personalization object to append to the message.</param>
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

        /// <summary>
        /// Set the from email.
        /// </summary>
        /// <param name="email">An email object containing the email address and name of the sender. Unicode encoding is not supported for the from field.</param>
        public void SetFrom(EmailAddress email)
        {
            From = email;
        }

        /// <summary>
        /// Set the reply to email.
        /// </summary>
        /// <param name="email">An email object containing the email address and name of the individual who should receive responses to your email.</param>
        public void SetReplyTo(EmailAddress email)
        {
            ReplyTo = email;
        }

        /// <summary>
        /// Set a global subject line.
        /// </summary>
        /// <param name="subject">The subject of your email. This may be overridden by personalizations[x].subject.</param>
        public void SetGlobalSubject(string subject)
        {
            Subject = subject;
        }

        /// <summary>
        /// Add content to the email.
        /// </summary>
        /// <param name="mimeType">The mime type of the content you are including in your email. For example, text/plain or text/html.</param>
        /// <param name="text">The actual content of the specified mime type that you are including in your email.</param>
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

        /// <summary>
        /// Add contents to the email.
        /// </summary>
        /// <param name="contents">A list of Content.</param>
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

        /// <summary>
        /// Add an attachment to the email.
        /// </summary>
        /// <param name="filename">The filename of the attachment.</param>
        /// <param name="content">The Base64 encoded content of the attachment.</param>
        /// <param name="type">The mime type of the content you are attaching. For example, application/pdf or image/jpeg.</param>
        /// <param name="disposition">The content-disposition of the attachment specifying how you would like the attachment to be displayed. For example, "inline" results in the attached file being displayed automatically within the message while "attachment" results in the attached file requiring some action to be taken before it is displayed (e.g. opening or downloading the file). Defaults to "attachment". Can be either "attachment" or "inline".</param>
        /// <param name="content_id">A unique id that you specify for the attachment. This is used when the disposition is set to "inline" and the attachment is an image, allowing the file to be displayed within the body of your email. Ex: <![CDATA[ <img src="cid:ii_139db99fdb5c3704"></img> ]]></param>
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

        /// <summary>
        /// Add attachments to the email.
        /// </summary>
        /// <param name="attachments">A list of Attachments.</param>
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

        /// <summary>
        /// Add a template id to the email.
        /// </summary>
        /// <param name="templateID">The id of a template that you would like to use. If you use a template that contains content and a subject (either text or html), you do not need to specify those in the respective personalizations or message level parameters.</param>
        public void SetTemplateId(string templateID)
        {
            TemplateId = templateID;
        }

        /// <summary>
        /// Add a section substitution to the email.
        /// </summary>
        /// <param name="key">The section key.</param>
        /// <param name="value">The section replacement value.</param>
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

        /// <summary>
        /// Add sections to the email.
        /// </summary>
        /// <param name="sections">A list of Sections.</param>
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

        /// <summary>
        /// Add a global header to the email.
        /// </summary>
        /// <param name="key">Header key. (e.g. X-Header)</param>
        /// <param name="value">Header value.</param>
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

        /// <summary>
        /// Add global headers to the email.
        /// </summary>
        /// <param name="headers">A list of Headers.</param>
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

        /// <summary>
        /// Add a category to the email.
        /// </summary>
        /// <param name="category">A category name, not to exceed 255 characters. There is a limit of 10 categories per request.</param>
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

        /// <summary>
        /// Add categories to the email.
        /// </summary>
        /// <param name="categories">A list of Categories.</param>
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

        /// <summary>
        /// Add a global custom arguement.
        /// </summary>
        /// <param name="key">The custom arguement key. The value of this key will be overridden by custom args at the personalization level.</param>
        /// <param name="value">The custom argument value.</param>
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

        /// <summary>
        /// Add global custom arguements.
        /// </summary>
        /// <param name="customArgs">A list of CustomArgs.</param>
        public void AddGlobalCustomArgs(Dictionary<string, string> customArgs)
        {
            if (CustomArgs == null)
            {
                CustomArgs = customArgs;
            }
            else
            {
                CustomArgs = (CustomArgs != null)
                    ? CustomArgs.Union(customArgs).ToDictionary(pair => pair.Key, pair => pair.Value) : customArgs;
            }
            return;
        }

        /// <summary>
        /// Set the global send at unix timestamp.
        /// </summary>
        /// <param name="sendAt">A unix timestamp allowing you to specify when you want your email to be sent from SendGrid. This is not necessary if you want the email to be sent at the time of your API request.</param>
        public void SetGlobalSendAt(int sendAt)
        {
            SendAt = sendAt;
        }

        /// <summary>
        /// Set the email's batch id.
        /// </summary>
        /// <param name="batchId">
        /// This ID represents a batch of emails (AKA multiple sends of the same email) to be associated to each other for scheduling. Including a batch_id in your request allows you to include this email in that batch, and also enables you to cancel or pause the delivery of that entire batch. For more information, please read about Cancel Scheduled Sends.
        /// https://sendgrid.com/docs/API_Reference/Web_API_v3/cancel_schedule_send.html
        /// </param>
        public void SetBatchId(string batchId)
        {
            BatchId = batchId;
        }

        /// <summary>
        /// Set advanced suppression management. (ASM)
        /// </summary>
        /// <param name="groupID">The unsubscribe group to associate with this email.</param>
        /// <param name="groupsToDisplay">
        /// An array containing the unsubscribe groups that you would like to be displayed on the unsubscribe preferences page.
        /// https://sendgrid.com/docs/User_Guide/Suppressions/recipient_subscription_preferences.html
        /// </param>
        public void SetAsm(int groupID, List<int> groupsToDisplay)
        {
            Asm = new ASM();
            Asm.GroupId = groupID;
            Asm.GroupsToDisplay = groupsToDisplay;
            return;
        }

        /// <summary>
        /// Set this email's IP Pool.
        /// </summary>
        /// <param name="ipPoolName">The IP Pool that you would like to send this email from.</param>
        public void SetIpPoolName(string ipPoolName)
        {
            IpPoolName = ipPoolName;
        }

        /// <summary>
        /// Set the bcc settings.
        /// The address specified in the mail_settings.bcc object will receive a blind carbon copy (BCC) of the very first personalization defined in the personalizations array.
        /// </summary>
        /// <param name="enable">Indicates if this setting is enabled.</param>
        /// <param name="email">The email address that you would like to receive the BCC.</param>
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

        /// <summary>
        /// Set the bypass list management setting.
        /// Allows you to bypass all unsubscribe groups and suppressions to ensure that the email is delivered to every single recipient. This should only be used in emergencies when it is absolutely necessary that every recipient receives your email. Ex: outage emails, or forgot password emails.
        /// </summary>
        /// <param name="enable">Indicates if this setting is enabled.</param>
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

        /// <summary>
        /// Set the footer setting.
        /// The default footer that you would like appended to the bottom of every email.
        /// </summary>
        /// <param name="enable">Indicates if this setting is enabled.</param>
        /// <param name="html">The HTML content of your footer.</param>
        /// <param name="text">The plain text content of your footer.</param>
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

        /// <summary>
        /// Set the sandbox mode setting.
        /// This allows you to send a test email to ensure that your request body is valid and formatted correctly. For more information, please see our Classroom.
        /// </summary>
        /// <param name="enable">Indicates if this setting is enabled.</param>
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

        /// <summary>
        /// Set the spam check setting.
        /// This allows you to test the content of your email for spam.
        /// </summary>
        /// <param name="enable">Indicates if this setting is enabled.</param>
        /// <param name="threshold">The threshold used to determine if your content qualifies as spam on a scale from 1 to 10, with 10 being most strict, or most likely to be considered as spam.</param>
        /// <param name="postToUrl">An Inbound Parse URL that you would like a copy of your email along with the spam report to be sent to. The post_to_url parameter must start with http:// or https://.</param>
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

        /// <summary>
        /// Set the click tracking setting.
        /// Allows you to track whether a recipient clicked a link in your email.
        /// </summary>
        /// <param name="enable">Indicates if this setting is enabled.</param>
        /// <param name="enableText">Indicates if this setting should be included in the text/plain portion of your email.</param>
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

        /// <summary>
        /// Set the open tracking setting.
        /// Allows you to track whether the email was opened or not, but including a single pixel image in the body of the content. When the pixel is loaded, we can log that the email was opened.
        /// </summary>
        /// <param name="enable">Indicates if this setting is enabled.</param>
        /// <param name="substitutionTag">Allows you to specify a substitution tag that you can insert in the body of your email at a location that you desire. This tag will be replaced by the open tracking pixel.</param>
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

        /// <summary>
        /// Set the subscription tracking setting.
        /// Allows you to insert a subscription management link at the bottom of the text and html bodies of your email. If you would like to specify the location of the link within your email, you may use the substitution_tag.
        /// </summary>
        /// <param name="enable">Indicates if this setting is enabled.</param>
        /// <param name="html">HTML to be appended to the email, with the subscription tracking link. You may control where the link is by using the tag <![CDATA[ <% %> ]]></param>
        /// <param name="text">Text to be appended to the email, with the subscription tracking link. You may control where the link is by using the tag <![CDATA[ <% %> ]]></param>
        /// <param name="substitutionTag">A tag that will be replaced with the unsubscribe URL. for example: [unsubscribe_url]. If this parameter is used, it will override both the textand html parameters. The URL of the link will be placed at the substitution tag’s location, with no additional formatting.</param>
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

        /// <summary>
        /// Set the Google analytics setting.
        /// Allows you to enable tracking provided by Google Analytics.
        /// </summary>
        /// <param name="enable">Indicates if this setting is enabled.</param>
        /// <param name="utmCampaign">The name of the campaign.</param>
        /// <param name="utmContent">Used to differentiate your campaign from advertisements.</param>
        /// <param name="utmMedium">Name of the marketing medium. (e.g. Email)</param>
        /// <param name="utmSource">Name of the referrer source. (e.g. Google, SomeDomain.com, or Marketing Email)</param>
        /// <param name="utmTerm">Used to identify any paid keywords.</param>
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

        /// <summary>
        /// Creates the JSON object required to make a request to SendGrid.
        /// </summary>
        /// <returns>The JSON object required to make a request to SendGrid.</returns>
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
                // MimeType.Text > MimeType.Html > Everything Else
                for (var i = 0; i < Contents.Count; i++)
                {
                    if (Contents[i].Type == MimeType.Html)
                    {
                        var tempContent = new Content();
                        tempContent = Contents[i];
                        Contents.RemoveAt(i);
                        Contents.Insert(0, tempContent);
                    }
                    if (Contents[i].Type == MimeType.Text)
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