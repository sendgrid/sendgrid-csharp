// <copyright file="ASM.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid.Helpers.Mail
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// An object allowing you to specify how to handle unsubscribes.
    /// </summary>
    [JsonObject(IsReference = false)]
    public class ASM
    {
        /// <summary>
        /// Gets or sets the unsubscribe group to associate with this email.
        /// </summary>
        [JsonProperty(PropertyName = "group_id")]
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets an array containing the unsubscribe groups that you would like to be displayed on the unsubscribe preferences page.
        /// https://sendgrid.com/docs/User_Guide/Suppressions/recipient_subscription_preferences.html
        /// </summary>
        [JsonProperty(PropertyName = "groups_to_display", IsReference = false)]
        public List<int> GroupsToDisplay { get; set; }
    }
}
