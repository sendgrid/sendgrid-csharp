// <copyright file="MailMessageExtensions.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
#if NETSTANDARD2_0 || NET452

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// A collection of extension to assist with the conversion of System.Net.Mail.MailMessage to <see cref="SendGridMessage"/>
    /// </summary>
    public static class MailMessageExtensions
    {
        /// <summary>
        /// Converts a System.Net.Mail.MailAddress to a sendgrid address
        /// </summary>
        /// <param name="address">The address to be converted</param>
        /// <returns>The result of the conversion</returns>
        public static EmailAddress ToSendGridAddress(this MailAddress address)
        {
            return string.IsNullOrWhiteSpace(address.DisplayName) ?
                new EmailAddress(address.Address) :
                new EmailAddress(address.Address, address.DisplayName.Replace(",", string.Empty).Replace(";", string.Empty));
        }

        /// <summary>
        /// Copies the attachment from a MailMessage into the <see cref="SendGridMessage"/> object as a base64 string.
        /// </summary>
        /// <param name="attachment">The attachment to be converted</param>
        /// <returns>Returns a <see cref="Attachment"/></returns>
        public static SendGrid.Helpers.Mail.Attachment ToSendGridAttachment(this System.Net.Mail.Attachment attachment)
        {
            using (var stream = new MemoryStream())
            {
                attachment.ContentStream.CopyTo(stream);
                return new SendGrid.Helpers.Mail.Attachment()
                {
                    Disposition = "attachment",
                    Type = attachment.ContentType.MediaType,
                    Filename = attachment.Name,
                    ContentId = attachment.ContentId,
                    Content = Convert.ToBase64String(stream.ToArray())
                };
            }
        }

        /// <summary>
        /// Converts a System.Net.Mail.MailMessage to a SendGrid message.
        /// </summary>
        /// <param name="message">The MailMessage to be converted</param>
        /// <returns>Returns a <see cref="SendGridMessage"/> with the properties from the MailMessage</returns>
        public static SendGridMessage ToSendGridMessage(this MailMessage message)
        {
            var sendgridMessage = new SendGridMessage();

            sendgridMessage.From = ToSendGridAddress(message.From);

            if (message.ReplyToList.Any())
            {
                sendgridMessage.ReplyTo = message.ReplyToList.First().ToSendGridAddress();
            }

            if (message.To.Any())
            {
                var tos = message.To.Select(ToSendGridAddress).ToList();
                sendgridMessage.AddTos(tos);
            }

            if (message.CC.Any())
            {
                var cc = message.CC.Select(ToSendGridAddress).ToList();
                sendgridMessage.AddCcs(cc);
            }

            if (message.Bcc.Any())
            {
                var bcc = message.Bcc.Select(ToSendGridAddress).ToList();
                sendgridMessage.AddBccs(bcc);
            }

            if (!string.IsNullOrWhiteSpace(message.Subject))
            {
                sendgridMessage.Subject = message.Subject;
            }

            if (message.Headers.Count > 0)
            {
                var headers = message.Headers.AllKeys.ToDictionary(x => x, x => message.Headers[x]);
                sendgridMessage.AddHeaders(headers);
            }

            if (!string.IsNullOrWhiteSpace(message.Body))
            {
                var content = message.Body;

                if (message.IsBodyHtml)
                {

                    if (content.StartsWith("<html"))
                    {
                        content = message.Body;
                    }
                    else
                    {
                        content = $"<html><body>{message.Body}</body></html>";
                    }

                    sendgridMessage.AddContent("text/html", content);
                }
                else
                {
                    sendgridMessage.AddContent("text/plain", content);
                }
            }

            if (message.Attachments.Any())
            {
                sendgridMessage.Attachments = new List<Attachment>();
                sendgridMessage.Attachments.AddRange(message.Attachments.Select(ToSendGridAttachment));
            }

            return sendgridMessage;
        }
    }
}
#endif
