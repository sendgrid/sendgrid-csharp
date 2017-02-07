using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// Allows you to track whether the email was opened or not, but including a single pixel image in the body of the content. When the pixel is loaded, we can log that the email was opened.
    /// </summary>
    public class OpenTracking
    {
        /// <summary>
        /// Indicates if this setting is enabled.
        /// </summary>
        [JsonProperty(PropertyName = "enable")]
        public bool? Enable { get; set; }

        /// <summary>
        /// Allows you to specify a substitution tag that you can insert in the body of your email at a location that you desire. This tag will be replaced by the open tracking pixel.
        /// </summary>
        [JsonProperty(PropertyName = "substitution_tag")]
        public string SubstitutionTag { get; set; }
    }
}