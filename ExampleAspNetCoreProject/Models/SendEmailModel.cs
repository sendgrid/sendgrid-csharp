using System.ComponentModel.DataAnnotations;

namespace ExampleAspNetCoreProject.Models
{
    public class SendEmailModel
    {
        [Required]
        [EmailAddress]
        public string Recipient { get; set; }

        [Required]
        [EmailAddress]
        public string From { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
