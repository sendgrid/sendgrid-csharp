using System;
using System.Linq;
using System.Net;

namespace Example.Commands.Unsubscribes
{
    public class UnsubscribeDeleteConsoleCommand : IConsoleCommand
    {
        public NetworkCredential Credential { get; set; }

        public String Email { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Name
        {
            get
            {
                return "unsubscribeDelete";
            }
        }

        public string HelpText
        {
            get
            {
                return "delete email unsubscribes";
            }
        }

        public void Run()
        {
            Console.WriteLine("Unsubscribe Delete");
            this.StartDate = ConsoleCommandParser.PromptDate("(optional) Starting date:", null);
            this.EndDate = ConsoleCommandParser.PromptDate("(optional) Ending date:", null);
            this.Email = ConsoleCommandParser.PromptString("Email:", null);

            this.Execute();
            Console.WriteLine();
        }

        public void Execute()
        {
            SendGridMail.WebApi.WebUnsubscribesApi api = new SendGridMail.WebApi.WebUnsubscribesApi(this.Credential);
            api.DeleteUnsubscribes(this.StartDate, this.EndDate, this.Email);
            Console.WriteLine("Done, no errors.");
        }
    }
}
