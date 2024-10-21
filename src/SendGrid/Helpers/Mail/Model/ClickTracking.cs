// <copyright file="ClickTracking.cs" company="Twilio SendGrid">
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
    /// Allows you to track whether a recipient clicked a link in your email.
    /// </summary>
#if NETSTANDARD2_0
    // Globally defined by setting ReferenceHandler on the JsonSerializerOptions object
#else
    [JsonObject(IsReference = false)]
#endif
    public class ClickTracking
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
        /// Gets or sets if this setting should be included in the text/plain portion of your email.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("enable_text")]
#else
        [JsonProperty(PropertyName = "enable_text")]
#endif
        public bool? EnableText { get; set; }
    }
}
