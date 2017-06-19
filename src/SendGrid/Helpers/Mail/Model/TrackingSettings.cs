// <copyright file="TrackingSettings.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Mail
{
    using Newtonsoft.Json;

    /// <summary>
    /// Settings to determine how you would like to track the metrics of how your recipients interact with your email.
    /// </summary>
    [JsonObject(IsReference = false)]
    public class TrackingSettings
    {
        /// <summary>
        /// Gets or sets tracking whether a recipient clicked a link in your email.
        /// </summary>
        [JsonProperty(PropertyName = "click_tracking")]
        public ClickTracking ClickTracking { get; set; }

        /// <summary>
        /// Gets or sets tracking whether the email was opened or not, but including a single pixel image in the body of the content. When the pixel is loaded, we can log that the email was opened.
        /// </summary>
        [JsonProperty(PropertyName = "open_tracking")]
        public OpenTracking OpenTracking { get; set; }

        /// <summary>
        /// Gets or sets a subscription management link at the bottom of the text and html bodies of your email. If you would like to specify the location of the link within your email, you may use the substitution_tag.
        /// </summary>
        [JsonProperty(PropertyName = "subscription_tracking")]
        public SubscriptionTracking SubscriptionTracking { get; set; }

        /// <summary>
        /// Gets or sets tracking provided by Google Analytics.
        /// </summary>
        [JsonProperty(PropertyName = "ganalytics")]
        public Ganalytics Ganalytics { get; set; }
    }
}
