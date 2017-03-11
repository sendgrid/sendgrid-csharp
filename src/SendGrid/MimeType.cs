// <copyright file="MimeType.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid
{
    /// <summary>
    /// Helper for the common SendGrid email mime types.
    /// </summary>
    public class MimeType
    {
        /// <summary>
        /// The mime type for HTML content.
        /// </summary>
        public static readonly string Html = "text/html";

        /// <summary>
        /// The mime type for plain text content.
        /// </summary>
        public static readonly string Text = "text/plain";
    }
}
