﻿// <copyright file="MailMessageExtensions.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

#if NETSTANDARD2_0 || NET452

namespace SendGrid.Helpers.Mail
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Mail;

    public static class MailMessageExtensions
    {
        public static EmailAddress ToSendGridAddress(this MailAddress address)
        {
            return string.IsNullOrWhiteSpace(address.DisplayName) ?
                new EmailAddress(address.Address) :
                new EmailAddress(address.Address, address.DisplayName.Replace(",", "").Replace(";", ""));
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
                var tos = message.To.Select(x => x.ToSendGridAddress()).ToList();
                sendgridMessage.AddTos(tos);
            }

            if (message.CC.Any())
            {
                var cc = message.CC.Select(x => x.ToSendGridAddress()).ToList();
                sendgridMessage.AddCcs(cc);
            }

            if (message.Bcc.Any())
            {
                var bcc = message.Bcc.Select(x => x.ToSendGridAddress()).ToList();
                sendgridMessage.AddBccs(bcc);
            }

            if (!string.IsNullOrWhiteSpace(message.Subject))
            {
                sendgridMessage.Subject = message.Subject;
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
                sendgridMessage.Attachments = new System.Collections.Generic.List<SendGrid.Helpers.Mail.Attachment>();
                sendgridMessage.Attachments.AddRange(message.Attachments.Select(x => ToSendGridAttachment(x)));
            }

            return sendgridMessage;
        }

    }
}

#endif
