using System.Collections.Generic;
using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// An object allowing you to specify how to handle unsubscribes.
    /// </summary>
    public class ASM
    {
        /// <summary>
        /// The unsubscribe group to associate with this email.
        /// </summary>
        [JsonProperty(PropertyName = "group_id")]
        public int GroupId { get; set; }

        /// <summary>
        /// An array containing the unsubscribe groups that you would like to be displayed on the unsubscribe preferences page.
        /// https://sendgrid.com/docs/User_Guide/Suppressions/recipient_subscription_preferences.html
        /// </summary>
        [JsonProperty(PropertyName = "groups_to_display")]
        public List<int> GroupsToDisplay { get; set; }
    }
}
