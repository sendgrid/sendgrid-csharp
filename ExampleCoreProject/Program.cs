using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;
using SendGrid.Extensions.DependencyInjection;
using SendGrid.Helpers.Mail;

namespace Example
{
    internal class Program
    {
        private static IConfiguration Configuration { get; set; }

        private static async Task Main()
        {
            var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";
            Configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddJsonFile($"appsettings.{env}.json", optional: true)
                    .Build();
            var services = ConfigureServices(new ServiceCollection()).BuildServiceProvider();
            var client = services.GetRequiredService<ISendGridClient>();
            var from = new EmailAddress(Configuration.GetValue("SendGrid:From", "test@example.com"), "Example User");
            var to = new EmailAddress(Configuration.GetValue("SendGrid:To", "test@example.com"), "Example User");
            var msg = new SendGridMessage
            {
                From = from,
                Subject = "Sending with Twilio SendGrid is Fun"
            };
            msg.AddContent(MimeType.Text, "and easy to do anywhere, even with C#");
            msg.AddTo(to);
            if (Configuration.GetValue("SendGrid:SandboxMode", false))
            {
                msg.MailSettings = new MailSettings
                {
                    SandboxMode = new SandboxMode
                    {
                        Enable = true
                    }
                };
            }
            Console.WriteLine($"Sending email with payload: \n{msg.Serialize()}");
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

            Console.WriteLine($"Response: {response.StatusCode}");
            Console.WriteLine(response.Headers);
        }

        private static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddSendGrid(options => { options.ApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY") ?? Configuration["SendGrid:ApiKey"]; });

            return services;
        }
    }
}