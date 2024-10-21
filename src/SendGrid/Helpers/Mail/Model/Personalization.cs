// <copyright file="Personalization.cs" company="Twilio SendGrid">
// Copyright (c) Twilio SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

#if NETSTANDARD2_0
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif
using System.Collections.Generic;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// An array of messages and their metadata. Each object within personalizations can be thought of as an envelope - it defines who should receive an individual message and how that message should be handled. For more information, please see our documentation on Personalizations. Parameters in personalizations will override the parameters of the same name from the message level.
    /// https://sendgrid.com/docs/Classroom/Send/v3_Mail_Send/personalizations.html.
    /// </summary>
#if NETSTANDARD2_0
    // Globally defined by setting ReferenceHandler on the JsonSerializerOptions object
#else
    [JsonObject(IsReference = false)]
#endif
    public class Personalization
    {
        /// <summary>
        /// Gets or sets an array of recipients. Each email object within this array may contain the recipient’s name, but must always contain the recipient’s email.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("to")]
#else
        [JsonProperty(PropertyName = "to", IsReference = false)]
#endif
        [JsonConverter(typeof(RemoveDuplicatesConverter<EmailAddress>))]
        public List<EmailAddress> Tos { get; set; }

        /// <summary>
        /// Gets or sets an array of recipients who will receive a copy of your email. Each email object within this array may contain the recipient’s name, but must always contain the recipient’s email.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("cc")]
#else
        [JsonProperty(PropertyName = "cc", IsReference = false)]
#endif
        [JsonConverter(typeof(RemoveDuplicatesConverter<EmailAddress>))]
        public List<EmailAddress> Ccs { get; set; }

        /// <summary>
        /// Gets or sets an array of recipients who will receive a blind carbon copy of your email. Each email object within this array may contain the recipient’s name, but must always contain the recipient’s email.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("bcc")]
#else
        [JsonProperty(PropertyName = "bcc", IsReference = false)]
#endif
        [JsonConverter(typeof(RemoveDuplicatesConverter<EmailAddress>))]
        public List<EmailAddress> Bccs { get; set; }

        /// <summary>
        /// Gets or sets the from email address. The domain must match the domain of the from email property specified at root level of the request body.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("from")]
#else
        [JsonProperty(PropertyName = "from")]
#endif
        public EmailAddress From { get; set; }

        /// <summary>
        /// Gets or sets the subject line of your email.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("subject")]
#else
        [JsonProperty(PropertyName = "subject")]
#endif
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the object allowing you to specify specific handling instructions for your email.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("headers")]
#else
        [JsonProperty(PropertyName = "headers", IsReference = false)]
#endif
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Gets or sets an object following the pattern "substitution_tag":"value to substitute". All are assumed to be strings. These substitutions will apply to the content of your email, in addition to the subject and reply-to parameters.
        /// You may not include more than 100 substitutions per personalization object, and the total collective size of your substitutions may not exceed 10,000 bytes per personalization object.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("substitutions")]
#else
        [JsonProperty(PropertyName = "substitutions", IsReference = false)]
#endif
        public Dictionary<string, string> Substitutions { get; set; }

        /// <summary>
        /// Gets or sets the values that are specific to this personalization that will be carried along with the email, activity data, and links. Substitutions will not be made on custom arguments. personalizations[x].custom_args will be merged with message level custom_args, overriding any conflicting keys. The combined total size of the resulting custom arguments, after merging, for each personalization may not exceed 10,000 bytes.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("custom_args")]
#else
        [JsonProperty(PropertyName = "custom_args", IsReference = false)]
#endif
        public Dictionary<string, string> CustomArgs { get; set; }

        /// <summary>
        /// Gets or sets a unix timestamp allowing you to specify when you want your email to be sent from Twilio SendGrid. This is not necessary if you want the email to be sent at the time of your API request.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("send_at")]
#else
        [JsonProperty(PropertyName = "send_at")]
#endif
        public long? SendAt { get; set; }

        /// <summary>
        /// Gets or sets the template data object following the pattern "template data key":"template data value". All are assumed to be strings. These key value pairs will apply to the content of your template email, in addition to the subject and reply-to parameters.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("dynamic_template_data")]
#else
        [JsonProperty(PropertyName = "dynamic_template_data", IsReference = false, ItemIsReference = false)]
#endif
        public object TemplateData { get; set; }
    }
}
