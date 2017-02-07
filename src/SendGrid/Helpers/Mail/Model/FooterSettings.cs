using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// The default footer that you would like appended to the bottom of every email.
    /// </summary>
    public class FooterSettings
    {
        /// <summary>
        /// Indicates if this setting is enabled.
        /// </summary>
        [JsonProperty(PropertyName = "enable")]
        public bool Enable { get; set; }

        /// <summary>
        /// The plain text content of your footer.
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// The HTML content of your footer.
        /// </summary>
        [JsonProperty(PropertyName = "html")]
        public string Html { get; set; }
    }
}