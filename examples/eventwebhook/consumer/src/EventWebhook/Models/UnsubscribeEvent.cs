namespace EventWebhook.Models
{
    public class UnsubscribeEvent : Event
    {
        public UnsubscribeEvent()
        {
            EventType = EventType.Unsubscribe;
        }
    }
}
