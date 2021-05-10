namespace EventWebhook.Models
{
    public class DeliveredEvent : Event
    {
        public string Response { get; set; }

        public DeliveredEvent()
        {
            EventType = EventType.Delivered;
        }
    }
}
