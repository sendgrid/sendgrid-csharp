// <copyright file="OpenTracking.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Mail
{
    using Newtonsoft.Json;

    /// <summary>
    /// Allows you to track whether the email was opened or not, but including a single pixel image in the body of the content. When the pixel is loaded, we can log that the email was opened.
    /// </summary>
    [JsonObject(IsReference = false)]
    public class OpenTracking
    {
        /// <summary>
        /// Gets or sets a value indicating whether this setting is enabled.
        /// </summary>
        [JsonProperty(PropertyName = "enable")]
        public bool? Enable { get; set; }

        /// <summary>
        /// Gets or sets the ability to specify a substitution tag that you can insert in the body of your email at a location that you desire. This tag will be replaced by the open tracking pixel.
        /// </summary>
        [JsonProperty(PropertyName = "substitution_tag")]
        public string SubstitutionTag { get; set; }
    }
}
