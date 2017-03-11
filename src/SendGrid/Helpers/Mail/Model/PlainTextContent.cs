// <copyright file="PlainTextContent.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Mail.Model
{
    /// <summary>
    /// Helper class for plain text mime types
    /// </summary>
    public class PlainTextContent : Content
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlainTextContent"/> class.
        /// </summary>
        /// <param name="value">The actual content of the specified mime type that you are including in your email.</param>
        public PlainTextContent(string value)
        {
            this.Type = MimeType.Text;
            this.Value = value;
        }
    }
}
