
namespace SendGrid.Models
{
    public class MailServiceResponse
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
