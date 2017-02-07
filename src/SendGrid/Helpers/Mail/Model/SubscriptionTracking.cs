using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// Allows you to insert a subscription management link at the bottom of the text and html bodies of your email. If you would like to specify the location of the link within your email, you may use the substitution_tag.
    /// </summary>
    public class SubscriptionTracking
    {
        /// <summary>
        /// Indicates if this setting is enabled.
        /// </summary>
        [JsonProperty(PropertyName = "enable")]
        public bool Enable { get; set; }

        /// <summary>
        /// Text to be appended to the email, with the subscription tracking link. You may control where the link is by using the tag (percent symbol) (percent symbol)
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// HTML to be appended to the email, with the subscription tracking link. You may control where the link is by using the tag (percent symbol) (percent symbol)
        /// </summary>
        [JsonProperty(PropertyName = "html")]
        public string Html { get; set; }

        /// <summary>
        /// A tag that will be replaced with the unsubscribe URL. for example: [unsubscribe_url]. If this parameter is used, it will override both the textand html parameters. The URL of the link will be placed at the substitution tag’s location, with no additional formatting.
        /// </summary>
        [JsonProperty(PropertyName = "substitution_tag")]
        public string SubstitutionTag { get; set; }
    }
}