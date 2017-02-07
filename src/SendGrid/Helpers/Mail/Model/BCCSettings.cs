using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// The address specified in the mail_settings.bcc object will receive a blind carbon copy (BCC) of the very first personalization defined in the personalizations array.
    /// </summary>
    public class BCCSettings
    {
        /// <summary>
        /// Indicates if this setting is enabled.
        /// </summary>
        [JsonProperty(PropertyName = "enable")]
        public bool? Enable { get; set; }

        /// <summary>
        /// The email address that you would like to receive the BCC.
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}