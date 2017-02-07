using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// This allows you to test the content of your email for spam.
    /// </summary>
    public class SpamCheck
    {
        /// <summary>
        /// Indicates if this setting is enabled.
        /// </summary>
        [JsonProperty(PropertyName = "enable")]
        public bool? Enable { get; set; }

        /// <summary>
        /// The threshold used to determine if your content qualifies as spam on a scale from 1 to 10, with 10 being most strict, or most likely to be considered as spam.
        /// </summary>
        [JsonProperty(PropertyName = "threshold")]
        public int? Threshold { get; set; }

        /// <summary>
        /// An Inbound Parse URL that you would like a copy of your email along with the spam report to be sent to. The post_to_url parameter must start with http:// or https://.
        /// </summary>
        [JsonProperty(PropertyName = "post_to_url")]
        public string PostToUrl { get; set; }
    }
}