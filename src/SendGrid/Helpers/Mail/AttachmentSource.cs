// <copyright file="AttachmentSource.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// Represents an attachment source
    /// </summary>
    public class AttachmentSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentSource"/> class.
        /// </summary>
        /// <param name="name">Attachment name</param>
        /// <param name="mimeType">Attachment mime type</param>
        /// <param name="content">Attachment content</param>
        public AttachmentSource(string name, string mimeType, byte[] content)
            : this(name, mimeType, content, content.Length)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentSource"/> class.
        /// </summary>
        /// <param name="name">Attachment name</param>
        /// <param name="mimeType">Attachment mime type</param>
        /// <param name="content">Attachment content</param>
        /// <param name="size">Attachment size in bytes</param>
        public AttachmentSource(string name, string mimeType, byte[] content, long size)
        {
            Name = name;
            MimeType = mimeType;
            Content = content;
            Size = size;
        }

        /// <summary>
        /// Gets attachment file name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets attachment mime type
        /// </summary>
        public string MimeType { get; }

        /// <summary>
        /// Gets attachment content
        /// </summary>
        public byte[] Content { get; }

        /// <summary>
        /// Gets attachment file size in bytes
        /// </summary>
        public long Size { get; }
    }
}
