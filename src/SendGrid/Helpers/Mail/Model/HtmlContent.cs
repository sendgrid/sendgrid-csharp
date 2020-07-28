﻿// <copyright file="HtmlContent.cs" company="Twilio SendGrid">
// Copyright (c) Twilio SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail.Model
{
    /// <summary>
    /// Helper class for plain html mime types.
    /// </summary>
    [JsonObject(IsReference = false)]
    public class HtmlContent : Content
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlContent"/> class.
        /// </summary>
        /// <param name="value">The actual content of the specified mime type that you are including in your email.</param>
        public HtmlContent(string value)
        {
            this.Type = MimeType.Html;
            this.Value = value;
        }
    }
}
