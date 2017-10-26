// <copyright file="FileAttachmentSource.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Mail
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a local file attachment source
    /// </summary>
    public class FileAttachmentSource : IAttachmentSource
    {
        private readonly string path;
        private readonly string mimeType;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileAttachmentSource"/> class.
        /// </summary>
        /// <param name="path">File path</param>
        /// <remarks>File mime type will be auto-detected based on file extension</remarks>
        public FileAttachmentSource(string path)
            : this(path, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileAttachmentSource"/> class.
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="mimeType">File mime type</param>
        public FileAttachmentSource(string path, string mimeType)
        {
            this.path = path;
            this.mimeType = mimeType;
        }

        /// <summary>
        /// Create <see cref="AttachmentSource" /> object from source
        /// </summary>
        /// <returns>An <see cref="AttachmentSource" /> object</returns>
        public async Task<AttachmentSource> GetAttachmentAsync()
        {
            var fileName = Path.GetFileName(this.path);
            byte[] buffer;

            using (var sourceStream = File.Open(fileName, FileMode.Open))
            {
                var streamLength = (int)sourceStream.Length;
                buffer = new byte[streamLength];
                await sourceStream.ReadAsync(buffer, 0, streamLength);
            }

            return new AttachmentSource(fileName, this.mimeType, buffer);
        }
    }
}
