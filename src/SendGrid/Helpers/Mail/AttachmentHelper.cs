// <copyright file="AttachmentHelpers.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Mail
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Simplified attachment creation
    /// </summary>
    public static class AttachmentHelper
    {
        private const int MaximumAttachmentSize = 30 * 1024 * 1024;

        /// <summary>
        /// Creates an attachment asynchronously from an attachment source
        /// </summary>
        /// <param name="attachmentSource">An attachment source</param>
        /// <returns>An <see cref="Attachment"/> object</returns>
        public static async Task<Attachment> CreateAttachmentAsync(IAttachmentSource attachmentSource)
        {
            var source = await attachmentSource.GetAttachmentAsync();

            if (source.Size > MaximumAttachmentSize)
            {
                throw new Exception("Attachment exceeds the size limit");
            }

            var content = Convert.ToBase64String(source.Content);

            var attachment = new Attachment
            {
                Filename = source.Name,
                Content = content,
            };

            if (!string.IsNullOrEmpty(source.MimeType))
            {
                attachment.Type = source.MimeType;
            }

            return attachment;
        }
    }
}
