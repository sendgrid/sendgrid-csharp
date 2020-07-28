﻿// <copyright file="SandboxMode.cs" company="Twilio SendGrid">
// Copyright (c) Twilio SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// This allows you to send a test email to ensure that your request body is valid and formatted correctly. For more information, please see our Classroom.
    /// https://sendgrid.com/docs/Classroom/Send/v3_Mail_Send/sandbox_mode.html.
    /// </summary>
    [JsonObject(IsReference = false)]
    public class SandboxMode
    {
        /// <summary>
        /// Gets or sets a value indicating whether this setting is enabled.
        /// </summary>
        [JsonProperty(PropertyName = "enable")]
        public bool? Enable { get; set; }
    }
}
