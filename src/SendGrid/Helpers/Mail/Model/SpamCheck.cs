// <copyright file="SpamCheck.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Mail
{
    using Newtonsoft.Json;

    /// <summary>
    /// This allows you to test the content of your email for spam.
    /// </summary>
    [JsonObject(IsReference = false)]
    public class SpamCheck
    {
        /// <summary>
        /// Gets or sets a value indicating whether this setting is enabled.
        /// </summary>
        [JsonProperty(PropertyName = "enable")]
        public bool? Enable { get; set; }

        /// <summary>
        /// Gets or sets the threshold used to determine if your content qualifies as spam on a scale from 1 to 10, with 10 being most strict, or most likely to be considered as spam.
        /// </summary>
        [JsonProperty(PropertyName = "threshold")]
        public int? Threshold { get; set; }

        /// <summary>
        /// Gets or sets an Inbound Parse URL that you would like a copy of your email along with the spam report to be sent to. The post_to_url parameter must start with http:// or https://.
        /// </summary>
        [JsonProperty(PropertyName = "post_to_url")]
        public string PostToUrl { get; set; }
    }
}
