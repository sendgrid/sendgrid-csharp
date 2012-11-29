using System;
using System.Linq;
using System.Net;

namespace Example.Commands.InvalidEmails
{
    public class InvalidEmailDeleteConsoleCommand : IConsoleCommand
    {
        public NetworkCredential Credential { get; set; }
        
        public String Email { get; set; }

        public string Name
        {
            get
            {
                return "invalidEmailDelete";
            }
        }

        public string HelpText
        {
            get
            {
                return "delete invalid email addresses";
            }
        }

        public void Run()
        {
            Console.WriteLine("Invalid Email Delete");
            this.Email = ConsoleCommandParser.PromptString("(optional) Email filter:", null);

            this.Execute();
            Console.WriteLine();
        }

        public void Execute()
        {
            SendGridMail.WebApi.WebInvalidEmailApi api = new SendGridMail.WebApi.WebInvalidEmailApi(this.Credential);
            api.DeleteInvalidEmails(this.Email);
            Console.WriteLine("Done, no errors.");
        }
    }
}