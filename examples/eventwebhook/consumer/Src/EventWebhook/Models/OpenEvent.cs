namespace EventWebhook.Models
{
    public class OpenEvent : Event
    {
        public string UserAgent { get; set; }

        public string IP { get; set; }
    }
}
