// <copyright file="TrackingSettings.cs" company="Twilio SendGrid">
// Copyright (c) Twilio SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

#if NETSTANDARD2_0
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// Settings to determine how you would like to track the metrics of how your recipients interact with your email.
    /// </summary>
#if NETSTANDARD2_0
    // Globally defined by setting ReferenceHandler on the JsonSerializerOptions object
#else
    [JsonObject(IsReference = false)]
#endif
    public class TrackingSettings
    {
        /// <summary>
        /// Gets or sets tracking whether a recipient clicked a link in your email.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("click_tracking")]
#else
        [JsonProperty(PropertyName = "click_tracking")]
#endif
        public ClickTracking ClickTracking { get; set; }

        /// <summary>
        /// Gets or sets tracking whether the email was opened or not, but including a single pixel image in the body of the content. When the pixel is loaded, we can log that the email was opened.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("open_tracking")]
#else
        [JsonProperty(PropertyName = "open_tracking")]
#endif
        public OpenTracking OpenTracking { get; set; }

        /// <summary>
        /// Gets or sets a subscription management link at the bottom of the text and html bodies of your email. If you would like to specify the location of the link within your email, you may use the substitution_tag.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("subscription_tracking")]
#else
        [JsonProperty(PropertyName = "subscription_tracking")]
#endif
        public SubscriptionTracking SubscriptionTracking { get; set; }

        /// <summary>
        /// Gets or sets tracking provided by Google Analytics.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("ganalytics")]
#else
        [JsonProperty(PropertyName = "ganalytics")]
#endif
        public Ganalytics Ganalytics { get; set; }
    }
}
