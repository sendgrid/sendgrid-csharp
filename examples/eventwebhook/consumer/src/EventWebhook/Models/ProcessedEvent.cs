namespace EventWebhook.Models
{
    public class ProcessedEvent : Event
    {
        public Pool Pool { get; set; }

        public ProcessedEvent()
        {
            EventType = EventType.Processed;
        }
    }
}
