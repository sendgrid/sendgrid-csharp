using System.Collections.Generic;
using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// An array of messages and their metadata. Each object within personalizations can be thought of as an envelope - it defines who should receive an individual message and how that message should be handled. For more information, please see our documentation on Personalizations. Parameters in personalizations will override the parameters of the same name from the message level.
    /// https://sendgrid.com/docs/Classroom/Send/v3_Mail_Send/personalizations.html
    /// </summary>
    public class Personalization
    {
        /// <summary>
        /// An array of recipients. Each email object within this array may contain the recipient’s name, but must always contain the recipient’s email.
        /// </summary>
        [JsonProperty(PropertyName = "to")]
        public List<EmailAddress> Tos { get; set; }

        /// <summary>
        /// An array of recipients who will receive a copy of your email. Each email object within this array may contain the recipient’s name, but must always contain the recipient’s email.
        /// </summary>
        [JsonProperty(PropertyName = "cc")]
        public List<EmailAddress> Ccs { get; set; }

        /// <summary>
        /// An array of recipients who will receive a blind carbon copy of your email. Each email object within this array may contain the recipient’s name, but must always contain the recipient’s email.
        /// </summary>
        [JsonProperty(PropertyName = "bcc")]
        public List<EmailAddress> Bccs { get; set; }

        /// <summary>
        /// The subject line of your email.
        /// </summary>
        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        /// <summary>
        /// An object allowing you to specify specific handling instructions for your email.
        /// </summary>
        [JsonProperty(PropertyName = "headers")]
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// An object following the pattern "substitution_tag":"value to substitute". All are assumed to be strings. These substitutions will apply to the content of your email, in addition to the subject and reply-to parameters. 
        /// You may not include more than 100 substitutions per personalization object, and the total collective size of your substitutions may not exceed 10,000 bytes per personalization object.
        /// </summary>
        [JsonProperty(PropertyName = "substitutions")]
        public Dictionary<string, string> Substitutions { get; set; }

        /// <summary>
        /// These are values that are specific to this personalization that will be carried along with the email, activity data, and links. Substitutions will not be made on custom arguments. personalizations[x].custom_args will be merged with message level custom_args, overriding any conflicting keys. The combined total size of the resulting custom arguments, after merging, for each personalization may not exceed 10,000 bytes.
        /// </summary>
        [JsonProperty(PropertyName = "custom_args")]
        public Dictionary<string, string> CustomArgs { get; set; }

        /// <summary>
        /// A unix timestamp allowing you to specify when you want your email to be sent from SendGrid. This is not necessary if you want the email to be sent at the time of your API request.
        /// </summary>
        [JsonProperty(PropertyName = "send_at")]
        public long? SendAt { get; set; }
    }
}