// <copyright file="SendGridMessage.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Mail
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Model;
    using Newtonsoft.Json;

    /// <summary>
    /// Class SendGridMessage builds an object that sends an email through SendGrid.
    /// </summary>
    [JsonObject(IsReference = false)]
    public class SendGridMessage
    {
        /// <summary>
        /// Gets or sets an email object containing the email address and name of the sender. Unicode encoding is not supported for the from field.
        /// </summary>
        [JsonProperty(PropertyName = "from")]
        public EmailAddress From { get; set; }

        /// <summary>
        /// Gets or sets the subject of your email. This may be overridden by personalizations[x].subject.
        /// </summary>
        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets a list of messages and their metadata. Each object within personalizations can be thought of as an envelope - it defines who should receive an individual message and how that message should be handled. For more information, please see our documentation on Personalizations. Parameters in personalizations will override the parameters of the same name from the message level.
        /// https://sendgrid.com/docs/Classroom/Send/v3_Mail_Send/personalizations.html
        /// </summary>
        [JsonProperty(PropertyName = "personalizations", IsReference = false)]
        public List<Personalization> Personalizations { get; set; }

        /// <summary>
        /// Gets or sets a list in which you may specify the content of your email. You can include multiple mime types of content, but you must specify at least one. To include more than one mime type, simply add another object to the array containing the type and value parameters. If included, text/plain and text/html must be the first indices of the array in this order. If you choose to include the text/plain or text/html mime types, they must be the first indices of the content array in the order text/plain, text/html.*Content is NOT mandatory if you using a transactional template and have defined the template_id in the Request
        /// </summary>
        [JsonProperty(PropertyName = "content", IsReference = false)]
        public List<Content> Contents { get; set; }

        /// <summary>
        /// Gets or sets a Content object with a Mime Type of text/plain.
        /// </summary>
        [JsonIgnore]
        public string PlainTextContent { get; set; }

        /// <summary>
        /// Gets or sets a Content object with a Mime Type of text/html.
        /// </summary>
        [JsonIgnore]
        public string HtmlContent { get; set; }

        /// <summary>
        /// Gets or sets a list of objects in which you can specify any attachments you want to include.
        /// </summary>
        [JsonProperty(PropertyName = "attachments", IsReference = false)]
        public List<Attachment> Attachments { get; set; }

        /// <summary>
        /// Gets or sets the id of a template that you would like to use. If you use a template that contains content and a subject (either text or html), you do not need to specify those in the respective personalizations or message level parameters.
        /// </summary>
        [JsonProperty(PropertyName = "template_id")]
        public string TemplateId { get; set; }

        /// <summary>
        /// Gets or sets an object containing key/value pairs of header names and the value to substitute for them. You must ensure these are properly encoded if they contain unicode characters. Must not be any of the following reserved headers: x-sg-id, x-sg-eid, received, dkim-signature, Content-Type, Content-Transfer-Encoding, To, From, Subject, Reply-To, CC, BCC
        /// </summary>
        [JsonProperty(PropertyName = "headers", IsReference = false)]
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Gets or sets an object of key/value pairs that define large blocks of content that can be inserted into your emails using substitution tags.
        /// </summary>
        [JsonProperty(PropertyName = "sections", IsReference = false)]
        public Dictionary<string, string> Sections { get; set; }

        /// <summary>
        /// Gets or sets a list of category names for this message. Each category name may not exceed 255 characters. You cannot have more than 10 categories per request.
        /// </summary>
        [JsonProperty(PropertyName = "categories", IsReference = false)]
        public List<string> Categories { get; set; }

        /// <summary>
        /// Gets or sets values that are specific to the entire send that will be carried along with the email and its activity data. Substitutions will not be made on custom arguments, so any string that is entered into this parameter will be assumed to be the custom argument that you would like to be used. This parameter is overridden by any conflicting personalizations[x].custom_args if that parameter has been defined. If personalizations[x].custom_args has been defined but does not conflict with the values defined within this parameter, the two will be merged. The combined total size of these custom arguments may not exceed 10,000 bytes.
        /// </summary>
        [JsonProperty(PropertyName = "custom_args", IsReference = false)]
        public Dictionary<string, string> CustomArgs { get; set; }

        /// <summary>
        /// Gets or sets a unix timestamp allowing you to specify when you want your email to be sent from SendGrid. This is not necessary if you want the email to be sent at the time of your API request.
        /// </summary>
        [JsonProperty(PropertyName = "send_at")]
        public long? SendAt { get; set; }

        /// <summary>
        /// Gets or sets an object allowing you to specify how to handle unsubscribes.
        /// </summary>
        [JsonProperty(PropertyName = "asm")]
        public ASM Asm { get; set; }

        /// <summary>
        /// Gets or sets an ID that represents a batch of emails (AKA multiple sends of the same email) to be associated to each other for scheduling. Including a batch_id in your request allows you to include this email in that batch, and also enables you to cancel or pause the delivery of that entire batch. For more information, please read about Cancel Scheduled Sends.
        /// https://sendgrid.com/docs/API_Reference/Web_API_v3/cancel_schedule_send.html
        /// </summary>
        [JsonProperty(PropertyName = "batch_id")]
        public string BatchId { get; set; }

        /// <summary>
        /// Gets or sets the IP Pool that you would like to send this email from.
        /// </summary>
        [JsonProperty(PropertyName = "ip_pool_name")]
        public string IpPoolName { get; set; }

        /// <summary>
        /// Gets or sets a collection of different mail settings that you can use to specify how you would like this email to be handled.
        /// </summary>
        [JsonProperty(PropertyName = "mail_settings")]
        public MailSettings MailSettings { get; set; }

        /// <summary>
        /// Gets or sets settings to determine how you would like to track the metrics of how your recipients interact with your email.
        /// </summary>
        [JsonProperty(PropertyName = "tracking_settings")]
        public TrackingSettings TrackingSettings { get; set; }

        /// <summary>
        /// Gets or sets an email object containing the email address and name of the individual who should receive responses to your email.
        /// </summary>
        [JsonProperty(PropertyName = "reply_to")]
        public EmailAddress ReplyTo { get; set; }

        /// <summary>
        /// Add a recipient email.
        /// </summary>
        /// <param name="email">Specify the recipient's email</param>
        /// <param name="name">Specify the recipient's name</param>
        public void AddTo(string email, string name = null)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");

            AddTo(new EmailAddress(email, name));
        }

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
                personalization.Tos = personalization.Tos ?? new List<EmailAddress>();
                personalization.Tos.Add(email);
                if (this.Personalizations == null)
                {
                    this.Personalizations = new List<Personalization>();
                    this.Personalizations.Add(personalization);
                }
                else
                {
                    this.Personalizations.Add(personalization);
                }

                return;
            }

            if (this.Personalizations != null)
            {
                if ((personalizationIndex != 0) && (this.Personalizations.Count() <= personalizationIndex))
                {
                    var p = new Personalization();
                    this.Personalizations.Insert(personalizationIndex, p);
                }

                if (this.Personalizations[personalizationIndex].Tos == null)
                {
                    this.Personalizations[personalizationIndex].Tos = new List<EmailAddress>();
                }

                this.Personalizations[personalizationIndex].Tos.Add(email);
                return;
            }

            this.Personalizations = new List<Personalization>()
            {
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
                if (this.Personalizations == null)
                {
                    this.Personalizations = new List<Personalization>();
                    this.Personalizations.Add(personalization);
                }
                else
                {
                    this.Personalizations.Add(personalization);
                }

                return;
            }

            if (this.Personalizations != null)
            {
                if ((personalizationIndex != 0) && (this.Personalizations.Count() <= personalizationIndex))
                {
                    var p = new Personalization();
                    this.Personalizations.Insert(personalizationIndex, p);
                }

                if (this.Personalizations[personalizationIndex].Tos == null)
                {
                    this.Personalizations[personalizationIndex].Tos = new List<EmailAddress>();
                }

                this.Personalizations[personalizationIndex].Tos.AddRange(emails);
                return;
            }

            this.Personalizations = new List<Personalization>()
            {
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
        /// <param name="email">Specify the recipient's email</param>
        /// <param name="name">Specify the recipient's name</param>
        public void AddCc(string email, string name = null)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");

            AddCc(new EmailAddress(email, name));
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
                personalization.Ccs = personalization.Ccs ?? new List<EmailAddress>();
                personalization.Ccs.Add(email);
                if (this.Personalizations == null)
                {
                    this.Personalizations = new List<Personalization>();
                    this.Personalizations.Add(personalization);
                }
                else
                {
                    this.Personalizations.Add(personalization);
                }

                return;
            }

            if (this.Personalizations != null)
            {
                if (this.Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    this.Personalizations.Insert(personalizationIndex, p);
                }

                if (this.Personalizations[personalizationIndex].Ccs == null)
                {
                    this.Personalizations[personalizationIndex].Ccs = new List<EmailAddress>();
                }

                this.Personalizations[personalizationIndex].Ccs.Add(email);
                return;
            }

            this.Personalizations = new List<Personalization>()
            {
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
                if (this.Personalizations == null)
                {
                    this.Personalizations = new List<Personalization>();
                    this.Personalizations.Add(personalization);
                }
                else
                {
                    this.Personalizations.Add(personalization);
                }

                return;
            }

            if (this.Personalizations != null)
            {
                if (this.Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    this.Personalizations.Insert(personalizationIndex, p);
                }

                if (this.Personalizations[personalizationIndex].Ccs == null)
                {
                    this.Personalizations[personalizationIndex].Ccs = new List<EmailAddress>();
                }

                this.Personalizations[personalizationIndex].Ccs.AddRange(emails);
                return;
            }

            this.Personalizations = new List<Personalization>()
            {
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
        /// <param name="email">Specify the recipient's email</param>
        /// <param name="name">Specify the recipient's name</param>
        public void AddBcc(string email, string name = null)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException("email");

            AddBcc(new EmailAddress(email, name));
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
                personalization.Bccs = personalization.Bccs ?? new List<EmailAddress>();
                personalization.Bccs.Add(email);
                if (this.Personalizations == null)
                {
                    this.Personalizations = new List<Personalization>();
                    this.Personalizations.Add(personalization);
                }
                else
                {
                    this.Personalizations.Add(personalization);
                }

                return;
            }

            if (this.Personalizations != null)
            {
                if (this.Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    this.Personalizations.Insert(personalizationIndex, p);
                }

                if (this.Personalizations[personalizationIndex].Bccs == null)
                {
                    this.Personalizations[personalizationIndex].Bccs = new List<EmailAddress>();
                }

                this.Personalizations[personalizationIndex].Bccs.Add(email);
                return;
            }

            this.Personalizations = new List<Personalization>()
            {
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
                if (this.Personalizations == null)
                {
                    this.Personalizations = new List<Personalization>();
                    this.Personalizations.Add(personalization);
                }
                else
                {
                    this.Personalizations.Add(personalization);
                }

                return;
            }

            if (this.Personalizations != null)
            {
                if (this.Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    this.Personalizations.Insert(personalizationIndex, p);
                }

                if (this.Personalizations[personalizationIndex].Bccs == null)
                {
                    this.Personalizations[personalizationIndex].Bccs = new List<EmailAddress>();
                }

                this.Personalizations[personalizationIndex].Bccs.AddRange(emails);
                return;
            }

            this.Personalizations = new List<Personalization>()
            {
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
                if (this.Personalizations == null)
                {
                    this.Personalizations = new List<Personalization>();
                    this.Personalizations.Add(personalization);
                }
                else
                {
                    this.Personalizations.Add(personalization);
                }

                return;
            }

            if (this.Personalizations != null)
            {
                this.Personalizations[personalizationIndex].Subject = subject;
                return;
            }

            this.Personalizations = new List<Personalization>()
            {
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
                if (this.Personalizations == null)
                {
                    this.Personalizations = new List<Personalization>();
                    this.Personalizations.Add(personalization);
                }
                else
                {
                    this.Personalizations.Add(personalization);
                }

                return;
            }

            if (this.Personalizations != null)
            {
                if (this.Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    this.Personalizations.Insert(personalizationIndex, p);
                }

                if (this.Personalizations[personalizationIndex].Headers == null)
                {
                    this.Personalizations[personalizationIndex].Headers = new Dictionary<string, string>();
                }

                this.Personalizations[personalizationIndex].Headers.Add(headerKey, headerValue);
                return;
            }

            this.Personalizations = new List<Personalization>()
            {
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
                if (this.Personalizations == null)
                {
                    this.Personalizations = new List<Personalization>();
                    this.Personalizations.Add(personalization);
                }
                else
                {
                    this.Personalizations.Add(personalization);
                }

                return;
            }

            if (this.Personalizations != null)
            {
                if (this.Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    this.Personalizations.Insert(personalizationIndex, p);
                }

                if (this.Personalizations[personalizationIndex].Headers == null)
                {
                    this.Personalizations[personalizationIndex].Headers = new Dictionary<string, string>();
                }

                this.Personalizations[personalizationIndex].Headers = (this.Personalizations[personalizationIndex].Headers != null)
                    ? this.Personalizations[personalizationIndex].Headers.Union(headers).ToDictionary(pair => pair.Key, pair => pair.Value) : headers;
                return;
            }

            this.Personalizations = new List<Personalization>()
            {
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
                if (this.Personalizations == null)
                {
                    this.Personalizations = new List<Personalization>();
                    this.Personalizations.Add(personalization);
                }
                else
                {
                    this.Personalizations.Add(personalization);
                }

                return;
            }

            if (this.Personalizations != null)
            {
                if (this.Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    this.Personalizations.Insert(personalizationIndex, p);
                }

                if (this.Personalizations[personalizationIndex].Substitutions == null)
                {
                    this.Personalizations[personalizationIndex].Substitutions = new Dictionary<string, string>();
                }

                this.Personalizations[personalizationIndex].Substitutions.Add(substitutionKey, substitutionValue);
                return;
            }

            this.Personalizations = new List<Personalization>()
            {
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
        /// <param name="substitutions">A list of Substitutions.</param>
        /// <param name="personalizationIndex">Specify the index of the Personalization object where you want to add the substitutions.</param>
        /// <param name="personalization">A personalization object to append to the message.</param>
        public void AddSubstitutions(Dictionary<string, string> substitutions, int personalizationIndex = 0, Personalization personalization = null)
        {
            if (personalization != null)
            {
                personalization.Substitutions = (personalization.Substitutions != null)
                    ? personalization.Substitutions.Union(substitutions).ToDictionary(pair => pair.Key, pair => pair.Value) : substitutions;
                if (this.Personalizations == null)
                {
                    this.Personalizations = new List<Personalization>();
                    this.Personalizations.Add(personalization);
                }
                else
                {
                    this.Personalizations.Add(personalization);
                }

                return;
            }

            if (this.Personalizations != null)
            {
                if (this.Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    this.Personalizations.Insert(personalizationIndex, p);
                }

                if (this.Personalizations[personalizationIndex].Substitutions == null)
                {
                    this.Personalizations[personalizationIndex].Substitutions = new Dictionary<string, string>();
                }

                this.Personalizations[personalizationIndex].Substitutions = (this.Personalizations[personalizationIndex].Substitutions != null)
                    ? this.Personalizations[personalizationIndex].Substitutions.Union(substitutions).ToDictionary(pair => pair.Key, pair => pair.Value) : substitutions;
                return;
            }

            this.Personalizations = new List<Personalization>()
            {
                new Personalization()
                {
                    Substitutions = substitutions
                }
            };
            return;
        }

        /// <summary>
        /// Add a custom argument to the email.
        /// </summary>
        /// <param name="customArgKey">The custom argument key.</param>
        /// <param name="customArgValue">The custom argument value.</param>
        /// <param name="personalizationIndex">Specify the index of the Personalization object where you want to add the custom arg.</param>
        /// <param name="personalization">A personalization object to append to the message.</param>
        public void AddCustomArg(string customArgKey, string customArgValue, int personalizationIndex = 0, Personalization personalization = null)
        {
            if (personalization != null)
            {
                personalization.CustomArgs.Add(customArgKey, customArgValue);
                if (this.Personalizations == null)
                {
                    this.Personalizations = new List<Personalization>();
                    this.Personalizations.Add(personalization);
                }
                else
                {
                    this.Personalizations.Add(personalization);
                }

                return;
            }

            if (this.Personalizations != null)
            {
                if (this.Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    this.Personalizations.Insert(personalizationIndex, p);
                }

                if (this.Personalizations[personalizationIndex].CustomArgs == null)
                {
                    this.Personalizations[personalizationIndex].CustomArgs = new Dictionary<string, string>();
                }

                this.Personalizations[personalizationIndex].CustomArgs.Add(customArgKey, customArgValue);
                return;
            }

            this.Personalizations = new List<Personalization>()
            {
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
        /// Add custom arguments to the email.
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
                if (this.Personalizations == null)
                {
                    this.Personalizations = new List<Personalization>();
                    this.Personalizations.Add(personalization);
                }
                else
                {
                    this.Personalizations.Add(personalization);
                }

                return;
            }

            if (this.Personalizations != null)
            {
                if (this.Personalizations[personalizationIndex] == null)
                {
                    var p = new Personalization();
                    this.Personalizations.Insert(personalizationIndex, p);
                }

                if (this.Personalizations[personalizationIndex].CustomArgs == null)
                {
                    this.Personalizations[personalizationIndex].CustomArgs = new Dictionary<string, string>();
                }

                this.Personalizations[personalizationIndex].CustomArgs = (this.Personalizations[personalizationIndex].CustomArgs != null)
                    ? this.Personalizations[personalizationIndex].CustomArgs.Union(customArgs).ToDictionary(pair => pair.Key, pair => pair.Value) : customArgs;
                return;
            }

            this.Personalizations = new List<Personalization>()
            {
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
        /// <param name="sendAt">Specify the unix timestamp for when you want the email to be sent from SendGrid</param>
        /// <param name="personalizationIndex">Specify the index of the Personalization object where you want to add the send at timestamp.</param>
        /// <param name="personalization">A personalization object to append to the message.</param>
        public void SetSendAt(int sendAt, int personalizationIndex = 0, Personalization personalization = null)
        {
            if (personalization != null)
            {
                personalization.SendAt = sendAt;
                if (this.Personalizations == null)
                {
                    this.Personalizations = new List<Personalization>();
                    this.Personalizations.Add(personalization);
                }
                else
                {
                    this.Personalizations.Add(personalization);
                }

                return;
            }

            if (this.Personalizations != null)
            {
                this.Personalizations[personalizationIndex].SendAt = sendAt;
                return;
            }

            this.Personalizations = new List<Personalization>()
            {
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
        /// <param name="email">Specify the recipient's email</param>
        /// <param name="name">Specify the recipient's name</param>
        public void SetFrom(string email, string name = null)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentNullException("email");
            }

            SetFrom(new EmailAddress(email, name));
        }

        /// <summary>
        /// Set the from email.
        /// </summary>
        /// <param name="email">An email object containing the email address and name of the sender. Unicode encoding is not supported for the from field.</param>
        public void SetFrom(EmailAddress email)
        {
            this.From = email;
        }

        /// <summary>
        /// Set the reply to email.
        /// </summary>
        /// <param name="email">An email object containing the email address and name of the individual who should receive responses to your email.</param>
        public void SetReplyTo(EmailAddress email)
        {
            this.ReplyTo = email;
        }

        /// <summary>
        /// Set a global subject line.
        /// </summary>
        /// <param name="subject">The subject of your email. This may be overridden by personalizations[x].subject.</param>
        public void SetGlobalSubject(string subject)
        {
            this.Subject = subject;
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

            if (this.Contents == null)
            {
                this.Contents = new List<Content>()
                {
                    content
                };
            }
            else
            {
                this.Contents.Add(content);
            }

            return;
        }

        /// <summary>
        /// Add contents to the email.
        /// </summary>
        /// <param name="contents">A list of Content.</param>
        public void AddContents(List<Content> contents)
        {
            if (this.Contents == null)
            {
                this.Contents = new List<Content>();
                this.Contents = contents;
            }
            else
            {
                this.Contents.AddRange(contents);
            }

            return;
        }

        /// <summary>
        /// Add an attachment from a stream to the email. No attachment will be added in the case that the stream cannot be read. Streams of length greater than int.MaxValue are truncated.
        /// </summary>
        /// <param name="filename">The filename the attachment will display in the email.</param>
        /// <param name="contentStream">The stream to use as content of the attachment.</param>
        /// <param name="type">The mime type of the content you are attaching. For example, application/pdf or image/jpeg.</param>
        /// <param name="disposition">The content-disposition of the attachment specifying how you would like the attachment to be displayed. For example, "inline" results in the attached file being displayed automatically within the message while "attachment" results in the attached file requiring some action to be taken before it is displayed (e.g. opening or downloading the file). Defaults to "attachment". Can be either "attachment" or "inline".</param>
        /// <param name="content_id">A unique id that you specify for the attachment. This is used when the disposition is set to "inline" and the attachment is an image, allowing the file to be displayed within the body of your email. Ex: <![CDATA[ <img src="cid:ii_139db99fdb5c3704"></img> ]]></param>
        /// <param name="cancellationToken">A cancellation token which can notify if the task should be canceled.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task AddAttachmentAsync(string filename, Stream contentStream, string type = null, string disposition = null, string content_id = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Stream doesn't want us to read it, can't do anything else here
            if (contentStream == null || !contentStream.CanRead)
            {
                return;
            }

            var contentLength = Convert.ToInt32(contentStream.Length);
            var streamBytes = new byte[contentLength];

            await contentStream.ReadAsync(streamBytes, 0, contentLength, cancellationToken);

            var base64Content = Convert.ToBase64String(streamBytes);

            this.AddAttachment(filename, base64Content, type, disposition, content_id);
        }

        /// <summary>
        /// Add an attachment to the email.
        /// </summary>
        /// <param name="filename">The filename the attachment will display in the email.</param>
        /// <param name="base64Content">The Base64 encoded content of the attachment.</param>
        /// <param name="type">The mime type of the content you are attaching. For example, application/pdf or image/jpeg.</param>
        /// <param name="disposition">The content-disposition of the attachment specifying how you would like the attachment to be displayed. For example, "inline" results in the attached file being displayed automatically within the message while "attachment" results in the attached file requiring some action to be taken before it is displayed (e.g. opening or downloading the file). Defaults to "attachment". Can be either "attachment" or "inline".</param>
        /// <param name="content_id">A unique id that you specify for the attachment. This is used when the disposition is set to "inline" and the attachment is an image, allowing the file to be displayed within the body of your email. Ex: <![CDATA[ <img src="cid:ii_139db99fdb5c3704"></img> ]]></param>
        public void AddAttachment(string filename, string base64Content, string type = null, string disposition = null, string content_id = null)
        {
            if (string.IsNullOrWhiteSpace(filename) || string.IsNullOrWhiteSpace(base64Content))
            {
                return;
            }

            var attachment = new Attachment
            {
                Filename = filename,
                Content = base64Content,
                Type = type,
                Disposition = disposition,
                ContentId = content_id
            };

            this.AddAttachment(attachment);
        }

        /// <summary>
        /// Add an attachment to the email.
        /// </summary>
        /// <param name="attachment">An Attachment.</param>
        public void AddAttachment(Attachment attachment)
        {
            if (this.Attachments == null)
            {
                this.Attachments = new List<Attachment>();
            }

            this.Attachments.Add(attachment);
        }

        /// <summary>
        /// Add attachments to the email.
        /// </summary>
        /// <param name="attachments">A list of Attachments.</param>
        public void AddAttachments(IEnumerable<Attachment> attachments)
        {
            if (this.Attachments == null)
            {
                this.Attachments = new List<Attachment>();
            }

            this.Attachments.AddRange(attachments);
        }

        /// <summary>
        /// Add a template id to the email.
        /// </summary>
        /// <param name="templateID">The id of a template that you would like to use. If you use a template that contains content and a subject (either text or html), you do not need to specify those in the respective personalizations or message level parameters.</param>
        public void SetTemplateId(string templateID)
        {
            this.TemplateId = templateID;
        }

        /// <summary>
        /// Add a section substitution to the email.
        /// </summary>
        /// <param name="key">The section key.</param>
        /// <param name="value">The section replacement value.</param>
        public void AddSection(string key, string value)
        {
            if (this.Sections == null)
            {
                this.Sections = new Dictionary<string, string>()
                {
                    { key, value }
                };
            }
            else
            {
                this.Sections.Add(key, value);
            }

            return;
        }

        /// <summary>
        /// Add sections to the email.
        /// </summary>
        /// <param name="sections">A list of Sections.</param>
        public void AddSections(Dictionary<string, string> sections)
        {
            if (this.Sections == null)
            {
                this.Sections = sections;
            }
            else
            {
                this.Sections = (this.Sections != null)
                    ? this.Sections.Union(sections).ToDictionary(pair => pair.Key, pair => pair.Value) : sections;
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
            if (this.Headers == null)
            {
                this.Headers = new Dictionary<string, string>()
                {
                    { key, value }
                };
            }
            else
            {
                this.Headers.Add(key, value);
            }

            return;
        }

        /// <summary>
        /// Add global headers to the email.
        /// </summary>
        /// <param name="headers">A list of Headers.</param>
        public void AddGlobalHeaders(Dictionary<string, string> headers)
        {
            if (this.Headers == null)
            {
                this.Headers = headers;
            }
            else
            {
                this.Headers = (this.Headers != null)
                    ? this.Headers.Union(headers).ToDictionary(pair => pair.Key, pair => pair.Value) : headers;
            }

            return;
        }

        /// <summary>
        /// Add a category to the email.
        /// </summary>
        /// <param name="category">A category name, not to exceed 255 characters. There is a limit of 10 categories per request.</param>
        public void AddCategory(string category)
        {
            if (this.Categories == null)
            {
                this.Categories = new List<string>()
                {
                    category
                };
            }
            else
            {
                this.Categories.Add(category);
            }

            return;
        }

        /// <summary>
        /// Add categories to the email.
        /// </summary>
        /// <param name="categories">A list of Categories.</param>
        public void AddCategories(List<string> categories)
        {
            if (this.Categories == null)
            {
                this.Categories = new List<string>();
                this.Categories = categories;
            }
            else
            {
                this.Categories.AddRange(categories);
            }

            return;
        }

        /// <summary>
        /// Add a global custom argument.
        /// </summary>
        /// <param name="key">The custom arguments key. The value of this key will be overridden by custom args at the personalization level.</param>
        /// <param name="value">The custom argument value.</param>
        public void AddGlobalCustomArg(string key, string value)
        {
            if (this.CustomArgs == null)
            {
                this.CustomArgs = new Dictionary<string, string>()
                {
                    { key, value }
                };
            }
            else
            {
                this.CustomArgs.Add(key, value);
            }

            return;
        }

        /// <summary>
        /// Add global custom arguments.
        /// </summary>
        /// <param name="customArgs">A list of CustomArgs.</param>
        public void AddGlobalCustomArgs(Dictionary<string, string> customArgs)
        {
            if (this.CustomArgs == null)
            {
                this.CustomArgs = customArgs;
            }
            else
            {
                this.CustomArgs = (this.CustomArgs != null)
                    ? this.CustomArgs.Union(customArgs).ToDictionary(pair => pair.Key, pair => pair.Value) : customArgs;
            }

            return;
        }

        /// <summary>
        /// Set the global send at unix timestamp.
        /// </summary>
        /// <param name="sendAt">A unix timestamp allowing you to specify when you want your email to be sent from SendGrid. This is not necessary if you want the email to be sent at the time of your API request.</param>
        public void SetGlobalSendAt(int sendAt)
        {
            this.SendAt = sendAt;
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
            this.BatchId = batchId;
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
            this.Asm = new ASM();
            this.Asm.GroupId = groupID;
            this.Asm.GroupsToDisplay = groupsToDisplay;
            return;
        }

        /// <summary>
        /// Set this email's IP Pool.
        /// </summary>
        /// <param name="ipPoolName">The IP Pool that you would like to send this email from.</param>
        public void SetIpPoolName(string ipPoolName)
        {
            this.IpPoolName = ipPoolName;
        }

        /// <summary>
        /// Set the bcc settings.
        /// The address specified in the mail_settings.bcc object will receive a blind carbon copy (BCC) of the very first personalization defined in the personalizations array.
        /// </summary>
        /// <param name="enable">Gets or sets a value indicating whether this setting is enabled.</param>
        /// <param name="email">The email address that you would like to receive the BCC.</param>
        public void SetBccSetting(bool enable, string email)
        {
            if (this.MailSettings == null)
            {
                this.MailSettings = new MailSettings();
            }

            this.MailSettings.BccSettings = new BCCSettings()
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
        /// <param name="enable">Gets or sets a value indicating whether this setting is enabled.</param>
        public void SetBypassListManagement(bool enable)
        {
            if (this.MailSettings == null)
            {
                this.MailSettings = new MailSettings();
            }

            this.MailSettings.BypassListManagement = new BypassListManagement()
            {
                Enable = enable
            };
            return;
        }

        /// <summary>
        /// Set the footer setting.
        /// The default footer that you would like appended to the bottom of every email.
        /// </summary>
        /// <param name="enable">Gets or sets a value indicating whether this setting is enabled.</param>
        /// <param name="html">The HTML content of your footer.</param>
        /// <param name="text">The plain text content of your footer.</param>
        public void SetFooterSetting(bool enable, string html = null, string text = null)
        {
            if (this.MailSettings == null)
            {
                this.MailSettings = new MailSettings();
            }

            this.MailSettings.FooterSettings = new FooterSettings()
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
        /// <param name="enable">Gets or sets a value indicating whether this setting is enabled.</param>
        public void SetSandBoxMode(bool enable)
        {
            if (this.MailSettings == null)
            {
                this.MailSettings = new MailSettings();
            }

            this.MailSettings.SandboxMode = new SandboxMode()
            {
                Enable = enable
            };
            return;
        }

        /// <summary>
        /// Set the spam check setting.
        /// This allows you to test the content of your email for spam.
        /// </summary>
        /// <param name="enable">Gets or sets a value indicating whether this setting is enabled.</param>
        /// <param name="threshold">The threshold used to determine if your content qualifies as spam on a scale from 1 to 10, with 10 being most strict, or most likely to be considered as spam.</param>
        /// <param name="postToUrl">An Inbound Parse URL that you would like a copy of your email along with the spam report to be sent to. The post_to_url parameter must start with http:// or https://.</param>
        public void SetSpamCheck(bool enable, int threshold = 1, string postToUrl = null)
        {
            if (this.MailSettings == null)
            {
                this.MailSettings = new MailSettings();
            }

            this.MailSettings.SpamCheck = new SpamCheck()
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
        /// <param name="enable">Gets or sets a value indicating whether this setting is enabled.</param>
        /// <param name="enableText">Indicates if this setting should be included in the text/plain portion of your email.</param>
        public void SetClickTracking(bool enable, bool enableText)
        {
            if (this.TrackingSettings == null)
            {
                this.TrackingSettings = new TrackingSettings();
            }

            this.TrackingSettings.ClickTracking = new ClickTracking()
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
        /// <param name="enable">Gets or sets a value indicating whether this setting is enabled.</param>
        /// <param name="substitutionTag">Allows you to specify a substitution tag that you can insert in the body of your email at a location that you desire. This tag will be replaced by the open tracking pixel.</param>
        public void SetOpenTracking(bool enable, string substitutionTag)
        {
            if (this.TrackingSettings == null)
            {
                this.TrackingSettings = new TrackingSettings();
            }

            this.TrackingSettings.OpenTracking = new OpenTracking()
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
        /// <param name="enable">Gets or sets a value indicating whether this setting is enabled.</param>
        /// <param name="html">HTML to be appended to the email, with the subscription tracking link. You may control where the link is by using the tag <![CDATA[ <% %> ]]></param>
        /// <param name="text">Text to be appended to the email, with the subscription tracking link. You may control where the link is by using the tag <![CDATA[ <% %> ]]></param>
        /// <param name="substitutionTag">A tag that will be replaced with the unsubscribe URL. for example: [unsubscribe_url]. If this parameter is used, it will override both the textand html parameters. The URL of the link will be placed at the substitution tag’s location, with no additional formatting.</param>
        public void SetSubscriptionTracking(bool enable, string html = null, string text = null, string substitutionTag = null)
        {
            if (this.TrackingSettings == null)
            {
                this.TrackingSettings = new TrackingSettings();
            }

            this.TrackingSettings.SubscriptionTracking = new SubscriptionTracking()
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
        /// <param name="enable">Gets or sets a value indicating whether this setting is enabled.</param>
        /// <param name="utmCampaign">The name of the campaign.</param>
        /// <param name="utmContent">Used to differentiate your campaign from advertisements.</param>
        /// <param name="utmMedium">Name of the marketing medium. (e.g. Email)</param>
        /// <param name="utmSource">Name of the referrer source. (e.g. Google, SomeDomain.com, or Marketing Email)</param>
        /// <param name="utmTerm">Used to identify any paid keywords.</param>
        public void SetGoogleAnalytics(bool enable, string utmCampaign = null, string utmContent = null, string utmMedium = null, string utmSource = null, string utmTerm = null)
        {
            if (this.TrackingSettings == null)
            {
                this.TrackingSettings = new TrackingSettings();
            }

            this.TrackingSettings.Ganalytics = new Ganalytics()
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
            if (this.PlainTextContent != null || this.HtmlContent != null)
            {
                this.Contents = new List<Content>();
                if (this.PlainTextContent != null)
                {
                    this.Contents.Add(new PlainTextContent(this.PlainTextContent));
                }

                if (this.HtmlContent != null)
                {
                    this.Contents.Add(new HtmlContent(this.HtmlContent));
                }

                this.PlainTextContent = null;
                this.HtmlContent = null;
            }

            if (this.Contents != null)
            {
                if (this.Contents.Count > 1)
                {
                    // MimeType.Text > MimeType.Html > Everything Else
                    for (var i = 0; i < this.Contents.Count; i++)
                    {
                        if (string.IsNullOrEmpty(this.Contents[i].Type) || string.IsNullOrEmpty(this.Contents[i].Value))
                        {
                            this.Contents.RemoveAt(i);
                            i--;
                            continue;
                        }

                        if (this.Contents[i].Type == MimeType.Html)
                        {
                            var tempContent = new Content();
                            tempContent = this.Contents[i];
                            this.Contents.RemoveAt(i);
                            this.Contents.Insert(0, tempContent);
                        }

                        if (this.Contents[i].Type == MimeType.Text)
                        {
                            var tempContent = new Content();
                            tempContent = this.Contents[i];
                            this.Contents.RemoveAt(i);
                            this.Contents.Insert(0, tempContent);
                        }
                    }
                }
            }

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include,
                StringEscapeHandling = StringEscapeHandling.EscapeHtml
            };

            return JsonConvert.SerializeObject(
                                               this,
                                               Formatting.None,
                                               jsonSerializerSettings);
        }
    }
}
