using System;
using System.Linq;
using System.Net;

namespace Example.Commands.Blocks
{
    public class BlockDeleteConsoleCommand : IConsoleCommand
    {
        public NetworkCredential Credential { get; set; }
        
        public String Email { get; set; }

        public string Name
        {
            get
            {
                return "blockDelete";
            }
        }

        public string HelpText
        {
            get
            {
                return "delete blocks";
            }
        }

        public void Run()
        {
            Console.WriteLine("Block Delete");
            this.Email = ConsoleCommandParser.PromptString("(optional) Email filter:", null);

            this.Execute();
            Console.WriteLine();
        }

        public void Execute()
        {
            SendGridMail.WebApi.WebBlockApi api = new SendGridMail.WebApi.WebBlockApi(this.Credential);
            api.DeleteBlocks(this.Email);
            Console.WriteLine("Done, no errors.");
        }
    }
}