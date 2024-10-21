// <copyright file="SpamCheck.cs" company="Twilio SendGrid">
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
    /// This allows you to test the content of your email for spam.
    /// </summary>
#if NETSTANDARD2_0
    // Globally defined by setting ReferenceHandler on the JsonSerializerOptions object
#else
    [JsonObject(IsReference = false)]
#endif
    public class SpamCheck
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
        /// Gets or sets the threshold used to determine if your content qualifies as spam on a scale from 1 to 10, with 10 being most strict, or most likely to be considered as spam.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("threshold")]
#else
        [JsonProperty(PropertyName = "threshold")]
#endif
        public int? Threshold { get; set; }

        /// <summary>
        /// Gets or sets an Inbound Parse URL that you would like a copy of your email along with the spam report to be sent to. The post_to_url parameter must start with http:// or https://.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("post_to_url")]
#else
        [JsonProperty(PropertyName = "post_to_url")]
#endif
        public string PostToUrl { get; set; }
    }
}
