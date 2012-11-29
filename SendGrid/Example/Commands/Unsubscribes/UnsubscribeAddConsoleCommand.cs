using System;
using System.Linq;
using System.Net;

namespace Example.Commands.Unsubscribes
{
    public class UnsubscribeAddConsoleCommand : IConsoleCommand
    {
        public NetworkCredential Credential { get; set; }

        public String Email { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Name
        {
            get
            {
                return "unsubscribeAdd";
            }
        }

        public string HelpText
        {
            get
            {
                return "add an email unsubscribe";
            }
        }

        public void Run()
        {
            Console.WriteLine("Unsubscribe Add");
            this.Email = ConsoleCommandParser.PromptString("Email:", null);

            this.Execute();
            Console.WriteLine();
        }

        public void Execute()
        {
            SendGridMail.WebApi.WebUnsubscribesApi api = new SendGridMail.WebApi.WebUnsubscribesApi(this.Credential);
            api.AddUnsubscribes(this.Email);
            Console.WriteLine("Done, no errors.");
        }
    }
}