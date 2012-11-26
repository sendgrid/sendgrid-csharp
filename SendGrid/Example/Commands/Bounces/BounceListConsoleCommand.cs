using System;
using System.Linq;
using System.Net;
using SendGridMail;

namespace Example.Commands.Bounces
{
    public class BounceListConsoleCommand : IConsoleCommand
    {
        public NetworkCredential Credential { get; set; }

        public Boolean IncludeDate { get; set; }

        public Int32 Days { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Int32 Limit { get; set; }

        public Int32 Offset { get; set; }

        public BounceType Type { get; set; }

        public String Email { get; set; }

        public string Name
        {
            get
            {
                return "bounceList";
            }
        }

        public string HelpText
        {
            get
            {
                return "list bounces";
            }
        }

        public void Run()
        {
            Console.WriteLine("Bounce List");
            this.IncludeDate = ConsoleCommandParser.PromptBoolean("include date (y)?", true);
            if (this.Days <= 0)
            {
                this.StartDate = ConsoleCommandParser.PromptDate("(optional) Starting date:", null);
                this.EndDate = ConsoleCommandParser.PromptDate("(optional) Ending date:", null);
            }
            this.Limit = ConsoleCommandParser.PromptInt32("(optional) Max records to return:", 0);
            this.Offset = 0;
            if (this.Limit > 0)
            {
                this.Offset = ConsoleCommandParser.PromptInt32("(optional) starting record:", 0);
            }
            this.Type = (BounceType)ConsoleCommandParser.PromptInt32("(optional) Type (0=All, 1=Hard, 2=Soft):", 0);
            this.Email = ConsoleCommandParser.PromptString("(optional) Email filter:", null);

            this.Execute();
            Console.WriteLine();
        }

        public void Execute()
        {
            SendGridMail.WebApi.WebBounceApi api = new SendGridMail.WebApi.WebBounceApi(this.Credential);
            var items = api.GetBounces(this.IncludeDate, this.Days, this.StartDate, this.EndDate, this.Limit, this.Offset, this.Type, this.Email);
            if (items.Count == 0)
            {
                Console.WriteLine("No items found.");
            }
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine(String.Format("{0};  {1};  {2}", item.Created, item.Status, item.Email));
                    Console.WriteLine(item.Reason);
                    Console.WriteLine();
                }
                Console.WriteLine(String.Format("{0} items found.", items.Count));
            }
        }
    }
}