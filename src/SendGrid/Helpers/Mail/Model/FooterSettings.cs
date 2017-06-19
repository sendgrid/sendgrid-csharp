// <copyright file="FooterSettings.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Mail
{
    using Newtonsoft.Json;

    /// <summary>
    /// The default footer that you would like appended to the bottom of every email.
    /// </summary>
    [JsonObject(IsReference = false)]
    public class FooterSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether this setting is enabled.
        /// </summary>
        [JsonProperty(PropertyName = "enable")]
        public bool Enable { get; set; }

        /// <summary>
        /// Gets or sets the plain text content of your footer.
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the HTML content of your footer.
        /// </summary>
        [JsonProperty(PropertyName = "html")]
        public string Html { get; set; }
    }
}
