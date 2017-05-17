// <copyright file="MailHelper.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Mail
{
    using System.Collections.Generic;

    /// <summary>
    /// Simplified email sending for common use cases
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// Send a single simple email
        /// </summary>
        /// <param name="from">An email object that may contain the recipient’s name, but must always contain the sender’s email.</param>
        /// <param name="to">An email object that may contain the recipient’s name, but must always contain the recipient’s email.</param>
        /// <param name="subject">The subject of your email. This may be overridden by SetGlobalSubject().</param>
        /// <param name="plainTextContent">The text/plain content of the email body.</param>
        /// <param name="htmlContent">The text/html content of the email body.</param>
        /// <returns>A SendGridMessage object.</returns>
        public static SendGridMessage CreateSingleEmail(
                                                        EmailAddress from,
                                                        EmailAddress to,
                                                        string subject,
                                                        string plainTextContent,
                                                        string htmlContent)
        {
            var msg = new SendGridMessage();
            msg.SetFrom(from);
            msg.SetSubject(subject);
            if (plainTextContent != null && plainTextContent != string.Empty)
            {
                msg.AddContent(MimeType.Text, plainTextContent);
            }

            if (htmlContent != null && htmlContent != string.Empty)
            {
                msg.AddContent(MimeType.Html, htmlContent);
            }

            msg.AddTo(to);
            return msg;
        }

        /// <summary>
        /// Send a single simple email to multiple recipients
        /// </summary>
        /// <param name="from">An email object that may contain the recipient’s name, but must always contain the sender’s email.</param>
        /// <param name="tos">A list of email objects that may contain the recipient’s name, but must always contain the recipient’s email.</param>
        /// <param name="subject">The subject of your email. This may be overridden by SetGlobalSubject().</param>
        /// <param name="plainTextContent">The text/plain content of the email body.</param>
        /// <param name="htmlContent">The text/html content of the email body.</param>
        /// <returns>A SendGridMessage object.</returns>
        public static SendGridMessage CreateSingleEmailToMultipleRecipients(
                                                                            EmailAddress from,
                                                                            List<EmailAddress> tos,
                                                                            string subject,
                                                                            string plainTextContent,
                                                                            string htmlContent)
        {
            var msg = new SendGridMessage();
            msg.SetFrom(from);
            msg.SetGlobalSubject(subject);
            if (plainTextContent != null && plainTextContent != string.Empty)
            {
                msg.AddContent(MimeType.Text, plainTextContent);
            }

            if (htmlContent != null && htmlContent != string.Empty)
            {
                msg.AddContent(MimeType.Html, htmlContent);
            }

            for (var i = 0; i < tos.Count; i++)
            {
                msg.AddTo(tos[i], i);
            }

            return msg;
        }

        /// <summary>
        /// Send multiple emails to multiple recipients.
        /// </summary>
        /// <param name="from">An email object that may contain the recipient’s name, but must always contain the sender’s email.</param>
        /// <param name="tos">A list of email objects that may contain the recipient’s name, but must always contain the recipient’s email.</param>
        /// <param name="subjects">The subject of your email. This may be overridden by SetGlobalSubject().</param>
        /// <param name="plainTextContent">The text/plain content of the email body.</param>
        /// <param name="htmlContent">The text/html content of the email body.</param>
        /// <param name="substitutions">Substitution key/values to customize the content for each email.</param>
        /// <returns>A SendGridMessage object.</returns>
        public static SendGridMessage CreateMultipleEmailsToMultipleRecipients(
                                                                               EmailAddress from,
                                                                               List<EmailAddress> tos,
                                                                               List<string> subjects,
                                                                               string plainTextContent,
                                                                               string htmlContent,
                                                                               List<Dictionary<string, string>> substitutions)
        {
            var msg = new SendGridMessage();
            msg.SetFrom(from);
            if (plainTextContent != null && plainTextContent != string.Empty)
            {
                msg.AddContent(MimeType.Text, plainTextContent);
            }

            if (htmlContent != null && htmlContent != string.Empty)
            {
                msg.AddContent(MimeType.Html, htmlContent);
            }

            for (var i = 0; i < tos.Count; i++)
            {
                msg.AddTo(tos[i], i);
                msg.SetSubject(subjects[i], i);
                msg.AddSubstitutions(substitutions[i], i);
            }

            return msg;
        }
        
        private static readonly Regex = new Regex(@"(?:(?<name>)(?<email>[^\<]*@.*[^\>])|(?<name>[^\<]*)\<(?<email>.*@.*)\>)",
                RegexOptions.ECMAScript);
        public static EmailAddress StringtoEmailAddress(string rfc2822Email)
        {
            const string nameGroup = "name";
            const string emailGroup = "email";
            var match = Regex.Match(rfc2822Email);
            if (!match.Success) return new EmailAddress(rfc2822Email);
            var email = match.Groups[emailGroup].Value.Trim();
            var name = match.Groups[nameGroup].Value.Trim();
            return new EmailAddress(email, name);
        }
    }
}
