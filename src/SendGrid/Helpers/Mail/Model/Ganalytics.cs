using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// Allows you to enable tracking provided by Google Analytics.
    /// </summary>
    public class Ganalytics
    {
        /// <summary>
        /// Indicates if this setting is enabled.
        /// </summary>
        [JsonProperty(PropertyName = "enable")]
        public bool? Enable { get; set; }

        /// <summary>
        /// Name of the referrer source. (e.g. Google, SomeDomain.com, or Marketing Email)
        /// </summary>
        [JsonProperty(PropertyName = "utm_source")]
        public string UtmSource { get; set; }

        /// <summary>
        /// Name of the marketing medium. (e.g. Email)
        /// </summary>
        [JsonProperty(PropertyName = "utm_medium")]
        public string UtmMedium { get; set; }

        /// <summary>
        /// Used to identify any paid keywords.
        /// </summary>
        [JsonProperty(PropertyName = "utm_term")]
        public string UtmTerm { get; set; }

        /// <summary>
        /// Used to differentiate your campaign from advertisements.
        /// </summary>
        [JsonProperty(PropertyName = "utm_content")]
        public string UtmContent { get; set; }

        /// <summary>
        /// The name of the campaign.
        /// </summary>
        [JsonProperty(PropertyName = "utm_campaign")]
        public string UtmCampaign { get; set; }
    }
}