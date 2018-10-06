<a name="attachments"></a>
# Attachments

```csharp
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using System.IO;

namespace Example
{
    internal class Example
    {
        private static void Main()
        {
            ExecuteManualAttachmentAdd().Wait();
            ExecuteStreamAttachmentAdd().Wait();
        }

        static async Task ExecuteManualAttachmentAdd()
        {
            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com");
            var subject = "Subject";
            var to = new EmailAddress("test@example.com");
            var body = "Email Body";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, "");
            var bytes = File.ReadAllBytes("C:\\Users\\username\\file.txt");
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment("file.txt", file);
            var response = await client.SendEmailAsync(msg);
        }

        static async Task ExecuteStreamAttachmentAdd()
        {
            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("test@example.com");
            var subject = "Subject";
            var to = new EmailAddress("test@example.com");
            var body = "Email Body";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, "");

            using (var fileStream = File.OpenRead("C:\\Users\\username\\file.txt"))
            {
                await msg.AddAttachmentAsync("file.txt", fileStream);
                var response = await client.SendEmailAsync(msg);
            }
        }
    }
}
```
