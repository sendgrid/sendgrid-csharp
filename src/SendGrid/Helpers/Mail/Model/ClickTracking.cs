// <copyright file="ClickTracking.cs" company="Twilio SendGrid">
// Copyright (c) Twilio SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// Allows you to track whether a recipient clicked a link in your email.
    /// </summary>
    [JsonObject(IsReference = false)]
    public class ClickTracking
    {
        /// <summary>
        /// Gets or sets a value indicating whether this setting is enabled.
        /// </summary>
        [JsonProperty(PropertyName = "enable")]
        public bool? Enable { get; set; }

        /// <summary>
        /// Gets or sets if this setting should be included in the text/plain portion of your email.
        /// </summary>
        [JsonProperty(PropertyName = "enable_text")]
        public bool? EnableText { get; set; }
    }
}
