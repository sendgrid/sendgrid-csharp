// <copyright file="Personalization.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Mail
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// An array of messages and their metadata. Each object within personalizations can be thought of as an envelope - it defines who should receive an individual message and how that message should be handled. For more information, please see our documentation on Personalizations. Parameters in personalizations will override the parameters of the same name from the message level.
    /// https://sendgrid.com/docs/Classroom/Send/v3_Mail_Send/personalizations.html
    /// </summary>
    [JsonObject(IsReference = false)]
    public class Personalization
    {
        /// <summary>
        /// Gets or sets an array of recipients. Each email object within this array may contain the recipient’s name, but must always contain the recipient’s email.
        /// </summary>
        [JsonProperty(PropertyName = "to", IsReference = false)]
        public List<EmailAddress> Tos { get; set; }

        /// <summary>
        /// Gets or sets an array of recipients who will receive a copy of your email. Each email object within this array may contain the recipient’s name, but must always contain the recipient’s email.
        /// </summary>
        [JsonProperty(PropertyName = "cc", IsReference = false)]
        public List<EmailAddress> Ccs { get; set; }

        /// <summary>
        /// Gets or sets an array of recipients who will receive a blind carbon copy of your email. Each email object within this array may contain the recipient’s name, but must always contain the recipient’s email.
        /// </summary>
        [JsonProperty(PropertyName = "bcc", IsReference = false)]
        public List<EmailAddress> Bccs { get; set; }

        /// <summary>
        /// Gets or sets the subject line of your email.
        /// </summary>
        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the object allowing you to specify specific handling instructions for your email.
        /// </summary>
        [JsonProperty(PropertyName = "headers", IsReference = false)]
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Gets or sets an object following the pattern "substitution_tag":"value to substitute". All are assumed to be strings. These substitutions will apply to the content of your email, in addition to the subject and reply-to parameters.
        /// You may not include more than 100 substitutions per personalization object, and the total collective size of your substitutions may not exceed 10,000 bytes per personalization object.
        /// </summary>
        [JsonProperty(PropertyName = "substitutions", IsReference = false)]
        public Dictionary<string, string> Substitutions { get; set; }

        /// <summary>
        /// Gets or sets the values that are specific to this personalization that will be carried along with the email, activity data, and links. Substitutions will not be made on custom arguments. personalizations[x].custom_args will be merged with message level custom_args, overriding any conflicting keys. The combined total size of the resulting custom arguments, after merging, for each personalization may not exceed 10,000 bytes.
        /// </summary>
        [JsonProperty(PropertyName = "custom_args", IsReference = false)]
        public Dictionary<string, string> CustomArgs { get; set; }

        /// <summary>
        /// Gets or sets a unix timestamp allowing you to specify when you want your email to be sent from SendGrid. This is not necessary if you want the email to be sent at the time of your API request.
        /// </summary>
        [JsonProperty(PropertyName = "send_at")]
        public long? SendAt { get; set; }
    }
}
