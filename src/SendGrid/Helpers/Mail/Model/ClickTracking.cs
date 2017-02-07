using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// Allows you to track whether a recipient clicked a link in your email.
    /// </summary>
    public class ClickTracking
    {
        /// <summary>
        /// Indicates if this setting is enabled.
        /// </summary>
        [JsonProperty(PropertyName = "enable")]
        public bool? Enable { get; set; }

        /// <summary>
        /// Indicates if this setting should be included in the text/plain portion of your email.
        /// </summary>
        [JsonProperty(PropertyName = "enable_text")]
        public bool? EnableText { get; set; }
    }
}