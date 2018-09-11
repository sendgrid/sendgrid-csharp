// <copyright file="MailHelper.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using SendGrid.Helpers.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// Simplified email sending for common use cases
    /// </summary>
    public class MailHelper
    {
        private const string NameGroup = "name";
        private const string EmailGroup = "email";
        private static readonly Regex Rfc2822Regex = new Regex(
            $@"(?:(?<{NameGroup}>)(?<{EmailGroup}>[^\<]*@.*[^\>])|(?<{NameGroup}>[^\<]*)\<(?<{EmailGroup}>.*@.*)\>)",
            RegexOptions.ECMAScript);

        private static readonly LambdaComparer<EmailAddress> EmailAddressComparer = new LambdaComparer<EmailAddress>(
            (a1, a2) => a1.Email.Equals(a2.Email, StringComparison.OrdinalIgnoreCase));

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
            if (!string.IsNullOrEmpty(plainTextContent))
            {
                msg.AddContent(MimeType.Text, plainTextContent);
            }

            if (!string.IsNullOrEmpty(htmlContent))
            {
                msg.AddContent(MimeType.Html, htmlContent);
            }

            msg.AddTo(to);
            return msg;
        }

        /// <summary>
        /// Send a single dynamic template email
        /// </summary>
        /// <param name="from">An email object that may contain the recipient’s name, but must always contain the sender’s email.</param>
        /// <param name="to">An email object that may contain the recipient’s name, but must always contain the recipient’s email.</param>
        /// <param name="templateId">The ID of the template.</param>
        /// <param name="dynamicTemplateData">The data with which to populate the dynamic template.</param>
        /// <returns>A SendGridMessage object.</returns>
        public static SendGridMessage CreateSingleTemplateEmail(
                                                        EmailAddress from,
                                                        EmailAddress to,
                                                        string templateId,
                                                        object dynamicTemplateData)
        {
            if (string.IsNullOrWhiteSpace(templateId))
            {
                throw new ArgumentException($"{nameof(templateId)} is required when creating a dynamic template email.", nameof(templateId));
            }

            var msg = new SendGridMessage();
            msg.SetFrom(from);
            msg.AddTo(to);
            msg.TemplateId = templateId;

            if (dynamicTemplateData != null)
            {
                msg.SetTemplateData(dynamicTemplateData);
            }

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
            if (!string.IsNullOrEmpty(plainTextContent))
            {
                msg.AddContent(MimeType.Text, plainTextContent);
            }

            if (!string.IsNullOrEmpty(htmlContent))
            {
                msg.AddContent(MimeType.Html, htmlContent);
            }

            var distinctTos = tos.Distinct(EmailAddressComparer).ToList();

            for (var i = 0; i < distinctTos.Count; i++)
            {
                msg.AddTo(distinctTos[i], i);
            }

            return msg;
        }

        /// <summary>
        /// Send a single simple email to multiple recipients
        /// </summary>
        /// <param name="from">An email object that may contain the recipient’s name, but must always contain the sender’s email.</param>
        /// <param name="tos">A list of email objects that may contain the recipient’s name, but must always contain the recipient’s email.</param>
        /// <param name="templateId">The ID of the template.</param>
        /// <param name="dynamicTemplateData">The data with which to populate the dynamic template.</param>
        /// <returns>A SendGridMessage object.</returns>
        public static SendGridMessage CreateSingleTemplateEmailToMultipleRecipients(
                                                                            EmailAddress from,
                                                                            List<EmailAddress> tos,
                                                                            string templateId,
                                                                            object dynamicTemplateData)
        {
            if (string.IsNullOrWhiteSpace(templateId))
            {
                throw new ArgumentException($"{nameof(templateId)} is required when creating a dynamic template email.", nameof(templateId));
            }

            var msg = new SendGridMessage();
            msg.SetFrom(from);
            msg.TemplateId = templateId;

            var setDynamicTemplateDataValues = dynamicTemplateData != null;

            var distinctTos = tos.Distinct(EmailAddressComparer).ToList();

            for (var i = 0; i < distinctTos.Count; i++)
            {
                msg.AddTo(distinctTos[i], i);

                if (setDynamicTemplateDataValues)
                {
                    msg.SetTemplateData(dynamicTemplateData, i);
                }
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
            if (!string.IsNullOrEmpty(plainTextContent))
            {
                msg.AddContent(MimeType.Text, plainTextContent);
            }

            if (!string.IsNullOrEmpty(htmlContent))
            {
                msg.AddContent(MimeType.Html, htmlContent);
            }

            var distinctTos = tos.Distinct(EmailAddressComparer).ToList();

            for (var i = 0; i < distinctTos.Count; i++)
            {
                msg.AddTo(distinctTos[i], i);
                msg.SetSubject(subjects[i], i);
                msg.AddSubstitutions(substitutions[i], i);
            }

            return msg;
        }

        /// <summary>
        /// Send multiple emails to multiple recipients.
        /// </summary>
        /// <param name="from">An email object that may contain the recipient’s name, but must always contain the sender’s email.</param>
        /// <param name="tos">A list of email objects that may contain the recipient’s name, but must always contain the recipient’s email.</param>
        /// <param name="templateId">The ID of the template.</param>
        /// <param name="dynamicTemplateData">The data with which to populate the dynamic template.</param>
        /// <returns>A SendGridMessage object.</returns>
        public static SendGridMessage CreateMultipleTemplateEmailsToMultipleRecipients(
                                                                               EmailAddress from,
                                                                               List<EmailAddress> tos,
                                                                               string templateId,
                                                                               List<object> dynamicTemplateData)
        {
            if (string.IsNullOrWhiteSpace(templateId))
            {
                throw new ArgumentException($"{nameof(templateId)} is required when creating a dynamic template email.", nameof(templateId));
            }

            var msg = new SendGridMessage();
            msg.SetFrom(from);
            msg.TemplateId = templateId;

            var setDynamicTemplateDataValues = dynamicTemplateData != null;

            var distinctTos = tos.Distinct(EmailAddressComparer).ToList();

            for (var i = 0; i < distinctTos.Count; i++)
            {
                msg.AddTo(distinctTos[i], i);

                if (setDynamicTemplateDataValues)
                {
                    msg.SetTemplateData(dynamicTemplateData[i], i);
                }
            }

            return msg;
        }

        /// <summary>
        /// Uncomplex conversion of a <![CDATA["Name <email@email.com>"]]> to EmailAddress
        /// </summary>
        /// <param name="rfc2822Email">"email@email.com" or <![CDATA["Name <email@email.com>"]]> string</param>
        /// <returns>EmailsAddress Object</returns>
        public static EmailAddress StringToEmailAddress(string rfc2822Email)
        {
            var match = Rfc2822Regex.Match(rfc2822Email);
            if (!match.Success)
            {
                return new EmailAddress(rfc2822Email);
            }

            var email = match.Groups[EmailGroup].Value.Trim();
            var name = match.Groups[NameGroup].Value.Trim();
            return new EmailAddress(email, name);
        }

        /// <summary>
        /// Send a single simple email to multiple recipients with option for displaying all the recipients present in "To" section of email
        /// </summary>
        /// <param name="from">An email object that may contain the recipient’s name, but must always contain the sender’s email.</param>
        /// <param name="tos">A list of email objects that may contain the recipient’s name, but must always contain the recipient’s email.</param>
        /// <param name="subject">The subject of your email. This may be overridden by SetGlobalSubject().</param>
        /// <param name="plainTextContent">The text/plain content of the email body.</param>
        /// <param name="htmlContent">The text/html content of the email body.</param>
        /// <param name="showAllRecipients">Displays all the recipients present in the "To" section of email.The default value is false</param>
        /// <returns>A SendGridMessage object.</returns>
        public static SendGridMessage CreateSingleEmailToMultipleRecipients(
                                                                            EmailAddress from,
                                                                            List<EmailAddress> tos,
                                                                            string subject,
                                                                            string plainTextContent,
                                                                            string htmlContent,
                                                                            bool showAllRecipients = false)
        {
            var msg = new SendGridMessage();
            if (showAllRecipients)
            {
                msg.SetFrom(from);
                msg.SetGlobalSubject(subject);
                if (!string.IsNullOrEmpty(plainTextContent))
                {
                    msg.AddContent(MimeType.Text, plainTextContent);
                }

                if (!string.IsNullOrEmpty(htmlContent))
                {
                    msg.AddContent(MimeType.Html, htmlContent);
                }

                msg.AddTos(tos);
            }
            else
            {
                msg = CreateSingleEmailToMultipleRecipients(from, tos, subject, plainTextContent, htmlContent);
            }

            return msg;
        }
    }
}
