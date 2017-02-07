using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// Allows you to bypass all unsubscribe groups and suppressions to ensure that the email is delivered to every single recipient. This should only be used in emergencies when it is absolutely necessary that every recipient receives your email. Ex: outage emails, or forgot password emails.
    /// </summary>
    public class BypassListManagement
    {
        /// <summary>
        /// Indicates if this setting is enabled.
        /// </summary>
        [JsonProperty(PropertyName = "enable")]
        public bool Enable { get; set; }
    }
}