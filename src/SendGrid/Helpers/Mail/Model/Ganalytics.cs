// <copyright file="Ganalytics.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Mail
{
    using Newtonsoft.Json;

    /// <summary>
    /// Allows you to enable tracking provided by Google Analytics.
    /// </summary>
    [JsonObject(IsReference = false)]
    public class Ganalytics
    {
        /// <summary>
        /// Gets or sets a value indicating whether this setting is enabled.
        /// </summary>
        [JsonProperty(PropertyName = "enable")]
        public bool? Enable { get; set; }

        /// <summary>
        /// Gets or sets the name of the referrer source. (e.g. Google, SomeDomain.com, or Marketing Email)
        /// </summary>
        [JsonProperty(PropertyName = "utm_source")]
        public string UtmSource { get; set; }

        /// <summary>
        /// Gets or sets the name of the marketing medium. (e.g. Email)
        /// </summary>
        [JsonProperty(PropertyName = "utm_medium")]
        public string UtmMedium { get; set; }

        /// <summary>
        /// Gets or sets the identification of any paid keywords.
        /// </summary>
        [JsonProperty(PropertyName = "utm_term")]
        public string UtmTerm { get; set; }

        /// <summary>
        /// Gets or sets the differentiation of your campaign from advertisements.
        /// </summary>
        [JsonProperty(PropertyName = "utm_content")]
        public string UtmContent { get; set; }

        /// <summary>
        /// Gets or sets the name of the campaign.
        /// </summary>
        [JsonProperty(PropertyName = "utm_campaign")]
        public string UtmCampaign { get; set; }
    }
}
