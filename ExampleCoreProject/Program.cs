using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Example
{
    internal class ExampleEmail
    {
        private static void Main()
        {
            Execute().Wait();
        }

        static async Task Execute()
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("team_intrapersonalfaces@devinterfaces.com");
            var subject = "Hello from Twilio SendGrid";

            var msg = new SendGridMessage();
            msg.Subject = subject;
            msg.AddContent(MimeType.Text, "Hello from fakegrid, please work");
            msg.SetFrom(from);

            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Tos = new List<EmailAddress>() {
                        new EmailAddress("bboussayoud@colgate.edu")
                    },
                },
                new Personalization() {
                    Tos = new List<EmailAddress>() {
                        new EmailAddress("bilal.boussayoud@gmail.com")
                    },
                    From = new EmailAddress("team_nofaces@devinterfaces.com")
                },
            };
            var response = await client.SendEmailAsync(msg);

            Console.WriteLine(msg.Serialize());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers.ToString());
            Console.WriteLine("\n\nPress any key to exit.");
            Console.ReadLine();
        }
    }
}