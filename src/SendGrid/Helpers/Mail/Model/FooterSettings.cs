// <copyright file="FooterSettings.cs" company="Twilio SendGrid">
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
    /// The default footer that you would like appended to the bottom of every email.
    /// </summary>
#if NETSTANDARD2_0
    // Globally defined by setting ReferenceHandler on the JsonSerializerOptions object
#else
    [JsonObject(IsReference = false)]
#endif
    public class FooterSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether this setting is enabled.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("enable")]
#else
        [JsonProperty(PropertyName = "enable")]
#endif
        public bool Enable { get; set; }

        /// <summary>
        /// Gets or sets the plain text content of your footer.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("text")]
#else
        [JsonProperty(PropertyName = "text")]
#endif
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the HTML content of your footer.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("html")]
#else
        [JsonProperty(PropertyName = "html")]
#endif
        public string Html { get; set; }
    }
}
