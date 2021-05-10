using System.Text.Json.Serialization;

namespace EventWebhook.Models
{
    public class GroupUnsubscribeEvent : ClickEvent
    {
        [JsonPropertyName("asm_group_id")]
        public int AsmGroupId { get; set; }

        public GroupUnsubscribeEvent()
        {
            EventType = EventType.GroupUnsubscribe;
        }
    }
}
