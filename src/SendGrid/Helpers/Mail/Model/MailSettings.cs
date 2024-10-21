// <copyright file="MailSettings.cs" company="Twilio SendGrid">
// Copyright (c) Twilio SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

#if NETSTANDARD2_0
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// A collection of different mail settings that you can use to specify how you would like this email to be handled.
    /// </summary>
#if NETSTANDARD2_0
    // Globally defined by setting ReferenceHandler on the JsonSerializerOptions object
#else
    [JsonObject(IsReference = false)]
#endif
    public class MailSettings
    {
        /// <summary>
        /// Gets or sets the address specified in the mail_settings.bcc object will receive a blind carbon copy (BCC) of the very first personalization defined in the personalizations array.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("bcc")]
#else
        [JsonProperty(PropertyName = "bcc")]
#endif
        public BCCSettings BccSettings { get; set; }

        /// <summary>
        /// Gets or sets the bypass of all unsubscribe groups and suppressions to ensure that the email is delivered to every single recipient. This should only be used in emergencies when it is absolutely necessary that every recipient receives your email. Ex: outage emails, or forgot password emails.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("bypass_list_management")]
#else
        [JsonProperty(PropertyName = "bypass_list_management")]
#endif
        public BypassListManagement BypassListManagement { get; set; }

        /// <summary>
        /// Gets or sets the bypass of spam report list to ensure that the email is delivered to recipients. Bounce and unsubscribe lists will still be checked; addresses on these other lists will not receive the message.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("bypass_spam_management")]
#else
        [JsonProperty(PropertyName = "bypass_spam_management")]
#endif
        public BypassSpamManagement BypassSpamManagement { get; set; }

        /// <summary>
        /// Gets or sets the bypass the bounce list to ensure that the email is delivered to recipients. Spam report and unsubscribe lists will still be checked; addresses on these other lists will not receive the message.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("bypass_bounce_management")]
#else
        [JsonProperty(PropertyName = "bypass_bounce_management")]
#endif
        public BypassBounceManagement BypassBounceManagement { get; set; }

        /// <summary>
        /// Gets or sets the bypass the global unsubscribe list to ensure that the email is delivered to recipients. Bounce and spam report lists will still be checked; addresses on these other lists will not receive the message. This filter applies only to global unsubscribes and will not bypass group unsubscribes.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("bypass_unsubscribe_management")]
#else
        [JsonProperty(PropertyName = "bypass_unsubscribe_management")]
#endif
        public BypassUnsubscribeManagement BypassUnsubscribeManagement { get; set; }

        /// <summary>
        /// Gets or sets the default footer that you would like appended to the bottom of every email.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("footer")]
#else
        [JsonProperty(PropertyName = "footer")]
#endif
        public FooterSettings FooterSettings { get; set; }

        /// <summary>
        /// Gets or sets the ability to send a test email to ensure that your request body is valid and formatted correctly. For more information, please see our Classroom.
        /// https://sendgrid.com/docs/Classroom/Send/v3_Mail_Send/sandbox_mode.html.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("sandbox_mode")]
#else
        [JsonProperty(PropertyName = "sandbox_mode")]
#endif
        public SandboxMode SandboxMode { get; set; }

        /// <summary>
        /// Gets or sets the ability to test the content of your email for spam.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("spam_check")]
#else
        [JsonProperty(PropertyName = "spam_check")]
#endif
        public SpamCheck SpamCheck { get; set; }
    }
}
