// <copyright file="BypassListManagement.cs" company="Twilio SendGrid">
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
    /// Allows you to bypass all unsubscribe groups and suppressions to ensure that the email is delivered to every single recipient. This should only be used in emergencies when it is absolutely necessary that every recipient receives your email. Ex: outage emails, or forgot password emails.
    /// </summary>
#if NETSTANDARD2_0
    // Globally defined by setting ReferenceHandler on the JsonSerializerOptions object
#else
    [JsonObject(IsReference = false)]
#endif
    public class BypassListManagement
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
    }
}
