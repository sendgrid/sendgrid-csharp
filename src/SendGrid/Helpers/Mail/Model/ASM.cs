using System.Collections.Generic;
using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    public class ASM
    {
        [JsonProperty(PropertyName = "group_id")]
        public int GroupId { get; set; }

        [JsonProperty(PropertyName = "groups_to_display")]
        public List<int> GroupsToDisplay { get; set; }
    }
}