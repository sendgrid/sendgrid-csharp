<a name="multipleemailsmultiplerecipients"></a>
# Send Multiple Emails to Multiple Recipients

```csharp
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Example
{
    internal class Example
    {
        private static void Main()
        {
            Execute().Wait();
        }

        static async Task Execute()
        {
            var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress("test@example.com", "Example User");
            var tos = new List<EmailAddress>
            {
                new EmailAddress("test1@example.com", "Example User1"),
                new EmailAddress("test2@example.com", "Example User2"),
                new EmailAddress("test3@example.com", "Example User3")
            };
            var subjects = new List<string> { "Test Subject1", "Test Subject2", "Test Subject3" };
            var plainTextContent = "Hello -name-";
            var htmlContent = "Goodbye -name-";
            var substitutions = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>() {{"-name-", "Name1"}},
                new Dictionary<string, string>() {{"-name-", "Name2"}},
                new Dictionary<string, string>() {{"-name-", "Name3"}}
            };

            var msg = MailHelper.CreateMultipleEmailsToMultipleRecipients(from,
                                                                          tos,
                                                                          subjects,
                                                                          plainTextContent,
                                                                          htmlContent,
                                                                          substitutions
                                                                          );
            var response = await client.SendEmailAsync(msg);
        }
    }
}
```
