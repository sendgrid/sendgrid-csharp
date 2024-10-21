// <copyright file="Ganalytics.cs" company="Twilio SendGrid">
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
    /// Allows you to enable tracking provided by Google Analytics.
    /// </summary>
#if NETSTANDARD2_0
    // Globally defined by setting ReferenceHandler on the JsonSerializerOptions object
#else
    [JsonObject(IsReference = false)]
#endif
    public class Ganalytics
    {
        /// <summary>
        /// Gets or sets a value indicating whether this setting is enabled.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("enable")]
#else
        [JsonProperty(PropertyName = "enable")]
#endif
        public bool? Enable { get; set; }

        /// <summary>
        /// Gets or sets the name of the referrer source (e.g. Google, SomeDomain.com, or Marketing Email).
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("utm_source")]
#else
        [JsonProperty(PropertyName = "utm_source")]
#endif
        public string UtmSource { get; set; }

        /// <summary>
        /// Gets or sets the name of the marketing medium (e.g. Email).
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("utm_medium")]
#else
        [JsonProperty(PropertyName = "utm_medium")]
#endif
        public string UtmMedium { get; set; }

        /// <summary>
        /// Gets or sets the identification of any paid keywords.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("utm_term")]
#else
        [JsonProperty(PropertyName = "utm_term")]
#endif
        public string UtmTerm { get; set; }

        /// <summary>
        /// Gets or sets the differentiation of your campaign from advertisements.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("utm_content")]
#else
        [JsonProperty(PropertyName = "utm_content")]
#endif
        public string UtmContent { get; set; }

        /// <summary>
        /// Gets or sets the name of the campaign.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("utm_campaign")]
#else
        [JsonProperty(PropertyName = "utm_campaign")]
#endif
        public string UtmCampaign { get; set; }
    }
}
