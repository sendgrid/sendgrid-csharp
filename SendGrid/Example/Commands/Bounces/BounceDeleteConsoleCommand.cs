using System;
using System.Linq;
using System.Net;
using SendGridMail;

namespace Example.Commands.Bounces
{
    public class BounceDeleteConsoleCommand : IConsoleCommand
    {
        public NetworkCredential Credential { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public BounceType Type { get; set; }

        public String Email { get; set; }

        public string Name
        {
            get
            {
                return "bounceDelete";
            }
        }

        public string HelpText
        {
            get
            {
                return "delete bounces";
            }
        }

        public void Run()
        {
            Console.WriteLine("Bounce Delete");
            this.StartDate = ConsoleCommandParser.PromptDate("(optional) Starting date:", null);
            this.EndDate = ConsoleCommandParser.PromptDate("(optional) Ending date:", null);
            this.Type = (BounceType)ConsoleCommandParser.PromptInt32("(optional) Type (0=All, 1=Hard, 2=Soft):", 0);
            this.Email = ConsoleCommandParser.PromptString("(optional) Email filter:", null);

            this.Execute();
            Console.WriteLine();
        }

        public void Execute()
        {
            SendGridMail.WebApi.WebBounceApi api = new SendGridMail.WebApi.WebBounceApi(this.Credential);
            api.DeleteBounces(this.StartDate, this.EndDate, this.Type, this.Email);
            Console.WriteLine("Done, no errors.");
        }
    }
}