namespace EventWebhook.Models
{
    public class DeferredEvent : DeliveredEvent
    {
        public int Attempt { get; set; }

    }
}
