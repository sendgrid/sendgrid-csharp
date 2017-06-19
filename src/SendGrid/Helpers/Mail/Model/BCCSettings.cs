// <copyright file="BCCSettings.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Mail
{
    using Newtonsoft.Json;

    /// <summary>
    /// Gets or sets the address specified in the mail_settings.bcc object will receive a blind carbon copy (BCC) of the very first personalization defined in the personalizations array.
    /// </summary>
    [JsonObject(IsReference = false)]
    public class BCCSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether this setting is enabled.
        /// </summary>
        [JsonProperty(PropertyName = "enable")]
        public bool? Enable { get; set; }

        /// <summary>
        /// Gets or sets the email address that you would like to receive the BCC.
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}
