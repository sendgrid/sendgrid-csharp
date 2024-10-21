// <copyright file="ASM.cs" company="Twilio SendGrid">
// Copyright (c) Twilio SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

#if NETSTANDARD2_0
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif
using System.Collections.Generic;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// An object allowing you to specify how to handle unsubscribes.
    /// </summary>
#if NETSTANDARD2_0
    // Globally defined by setting ReferenceHandler on the JsonSerializerOptions object
#else
    [JsonObject(IsReference = false)]
#endif
    public class ASM
    {
        /// <summary>
        /// Gets or sets the unsubscribe group to associate with this email.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("group_id")]
#else
        [JsonProperty(PropertyName = "group_id")]
#endif
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets an array containing the unsubscribe groups that you would like to be displayed on the unsubscribe preferences page.
        /// https://sendgrid.com/docs/User_Guide/Suppressions/recipient_subscription_preferences.html.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("groups_to_display")]
#else
        [JsonProperty(PropertyName = "groups_to_display", IsReference = false)]
#endif
        public List<int> GroupsToDisplay { get; set; }
    }
}
