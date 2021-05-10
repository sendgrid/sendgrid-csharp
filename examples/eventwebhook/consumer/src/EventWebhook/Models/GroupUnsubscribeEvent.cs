using Newtonsoft.Json;

namespace EventWebhook.Models
{
    public class GroupUnsubscribeEvent : ClickEvent
    {
        [JsonProperty("asm_group_id")]
        public int AsmGroupId { get; set; }
    }
}
