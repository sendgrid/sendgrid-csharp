namespace EventWebhook.Models
{
    public class GroupResubscribeEvent : GroupUnsubscribeEvent
    {
        public GroupResubscribeEvent()
        {
            EventType = EventType.GroupResubscribe;
        }
    }
}
