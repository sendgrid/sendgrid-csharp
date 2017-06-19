// <copyright file="SubscriptionTracking.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Mail
{
    using Newtonsoft.Json;

    /// <summary>
    /// Allows you to insert a subscription management link at the bottom of the text and html bodies of your email. If you would like to specify the location of the link within your email, you may use the substitution_tag.
    /// </summary>
    [JsonObject(IsReference = false)]
    public class SubscriptionTracking
    {
        /// <summary>
        /// Gets or sets a value indicating whether this setting is enabled.
        /// </summary>
        [JsonProperty(PropertyName = "enable")]
        public bool Enable { get; set; }

        /// <summary>
        /// Gets or sets the text to be appended to the email, with the subscription tracking link. You may control where the link is by using the tag (percent symbol) (percent symbol)
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the HTML to be appended to the email, with the subscription tracking link. You may control where the link is by using the tag (percent symbol) (percent symbol)
        /// </summary>
        [JsonProperty(PropertyName = "html")]
        public string Html { get; set; }

        /// <summary>
        /// Gets or sets a tag that will be replaced with the unsubscribe URL. for example: [unsubscribe_url]. If this parameter is used, it will override both the textand html parameters. The URL of the link will be placed at the substitution tag’s location, with no additional formatting.
        /// </summary>
        [JsonProperty(PropertyName = "substitution_tag")]
        public string SubstitutionTag { get; set; }
    }
}
