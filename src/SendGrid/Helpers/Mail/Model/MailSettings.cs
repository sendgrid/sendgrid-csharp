// <copyright file="MailSettings.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Mail
{
    using Newtonsoft.Json;

    /// <summary>
    /// A collection of different mail settings that you can use to specify how you would like this email to be handled.
    /// </summary>
    [JsonObject(IsReference = false)]
    public class MailSettings
    {
        /// <summary>
        /// Gets or sets the address specified in the mail_settings.bcc object will receive a blind carbon copy (BCC) of the very first personalization defined in the personalizations array.
        /// </summary>
        [JsonProperty(PropertyName = "bcc")]
        public BCCSettings BccSettings { get; set; }

        /// <summary>
        /// Gets or sets the bypass of all unsubscribe groups and suppressions to ensure that the email is delivered to every single recipient. This should only be used in emergencies when it is absolutely necessary that every recipient receives your email. Ex: outage emails, or forgot password emails.
        /// </summary>
        [JsonProperty(PropertyName = "bypass_list_management")]
        public BypassListManagement BypassListManagement { get; set; }

        /// <summary>
        /// Gets or sets the default footer that you would like appended to the bottom of every email.
        /// </summary>
        [JsonProperty(PropertyName = "footer")]
        public FooterSettings FooterSettings { get; set; }

        /// <summary>
        /// Gets or sets the ability to send a test email to ensure that your request body is valid and formatted correctly. For more information, please see our Classroom.
        /// https://sendgrid.com/docs/Classroom/Send/v3_Mail_Send/sandbox_mode.html
        /// </summary>
        [JsonProperty(PropertyName = "sandbox_mode")]
        public SandboxMode SandboxMode { get; set; }

        /// <summary>
        /// Gets or sets the ability to test the content of your email for spam.
        /// </summary>
        [JsonProperty(PropertyName = "spam_check")]
        public SpamCheck SpamCheck { get; set; }
    }
}
