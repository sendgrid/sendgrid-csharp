using System;
using System.Linq;
using System.Net;

namespace Example.Commands.SpamReports
{
    public class SpamReportDeleteConsoleCommand : IConsoleCommand
    {
        public NetworkCredential Credential { get; set; }

        public String Email { get; set; }

        public string Name
        {
            get
            {
                return "spamReportDelete";
            }
        }

        public string HelpText
        {
            get
            {
                return "delete report spam email addresses";
            }
        }

        public void Run()
        {
            Console.WriteLine("Spam Report Delete");
            this.Email = ConsoleCommandParser.PromptString("Email filter:", null);

            this.Execute();
            Console.WriteLine();
        }

        public void Execute()
        {
            SendGridMail.WebApi.WebSpamReportApi api = new SendGridMail.WebApi.WebSpamReportApi(this.Credential);
            api.DeleteSpamReports(this.Email);
            Console.WriteLine("Done, no errors.");
        }
    }
}