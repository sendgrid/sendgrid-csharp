namespace EventWebhook.Models
{
    public class SpamReportEvent : Event
    {
        public SpamReportEvent()
        {
            EventType = EventType.SpamReport;
        }
    }
}
